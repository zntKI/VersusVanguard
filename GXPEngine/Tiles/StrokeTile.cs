using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StrokeTile : Tile
{
    private bool isLeft;
    private float strokeLength;

    private Stroke stroke;
    private float strokeXOffset = 45f;  //Temp variables(remove when having actual assets)
    private float strokeYOffset = -65f;//Temp variables(remove when having actual assets)

    private Tuple<bool, int> shouldStopMoving = new Tuple<bool, int>(false, 0);
    private int currentTileStoppedY;

    private bool hasAlreadyStopped;

    public StrokeTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath, float strokeLength) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;

        stroke = new Stroke("levelTilesAssets/strokeStraight.png", shouldMoveLeft, strokeLength);
        AddChild(stroke);

        stroke.SetXY(shouldMoveLeft ? strokeXOffset : -strokeXOffset, strokeYOffset);

        hasAlreadyStopped = false;
    }

    private void Update()
    {
        if (stroke == null)
            stroke = FindObjectOfType<Stroke>();

        if (!shouldStopMoving.Item1)
            Move();
        else if (stroke.y - stroke.StrokeEndY >= currentTileStoppedY)
        {
            stroke.Destroy();
            Destroy();
        }
    }

    public override int CheckPosition(int reactionDistance, Vector2 leftRecordCoor, Vector2 rightRecordCoor)
    {
        int distanceFromRecordCenter = (int)Mathf.Abs((shouldMoveLeft ? leftRecordCoor.y : rightRecordCoor.y) - this.y);

        //Checks if the player has stopped 'spinning' the record
        if (distanceFromRecordCenter <= reactionDistance && shouldStopMoving.Item1 &&
            (shouldMoveLeft ? ControllerManager.GetLeftRecordValue() != shouldStopMoving.Item2 : ControllerManager.GetRightRecordValue() != shouldStopMoving.Item2) /*!Input.GetKey(shouldStopMoving.Item2)*/)
        {
            shouldStopMoving = new Tuple<bool, int>(false, 0);
            hasAlreadyStopped = true;
        }

        bool conditionLeftLane = shouldMoveLeft && ((isLeft && (Input.GetKey(Key.A) || ControllerManager.GetLeftRecordValue() == -1)) || (!isLeft && (Input.GetKey(Key.D) || ControllerManager.GetLeftRecordValue() == 1)));
        bool conditionRightLane = !shouldMoveLeft && ((isLeft && (Input.GetKey(Key.J) || ControllerManager.GetRightRecordValue() == -1)) || (!isLeft && (Input.GetKey(Key.L) || ControllerManager.GetRightRecordValue() == 1)));
        if ((conditionLeftLane || conditionRightLane)
            && distanceFromRecordCenter <= reactionDistance)
        {
            if (!shouldStopMoving.Item1 && !hasAlreadyStopped)
            {
                //int keyCode = conditionLeftLane ? (isLeft ? Key.A : Key.D) : (isLeft ? Key.J : Key.L);//Change that once we have the real controller
                int keyCode = isLeft ? -1 : 1;
                shouldStopMoving = new Tuple<bool, int>(true, keyCode);
                currentTileStoppedY = (int)this.y;

                if (stroke.parent == this)//Makes sure it detaches the stroke once
                    DetachStroke();
            }

            if (!hasAlreadyStopped)
                return Time.time % 8 == 0 ? reactionDistance - distanceFromRecordCenter : 0;
        }
        return 0;
    }

    //Detach stroke from tile and assign it as a child of level instead
    private void DetachStroke()
    {
        float strokeX = stroke.x;
        float strokeY = stroke.y;
        float strokeScaleX = stroke.scaleX;
        float strokeScaleY = stroke.scaleY;
        stroke.parent = this.parent;
        this.parent.SetChildIndex(stroke, this.Index);
        stroke.SetXY(this.x + strokeX, this.y + strokeY);
        stroke.SetScaleXY(this.scaleX * strokeScaleX, this.scaleY * strokeScaleY);
        stroke.Detach(shouldMoveLeft ? -sidewaysMoveAmount : sidewaysMoveAmount, speed, (int)y);
    }
}