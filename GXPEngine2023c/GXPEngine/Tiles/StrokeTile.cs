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
    private float strokeXOffset = 4f;  //Temp variables(remove when having actual assets)
    private float strokeYOffset = -11f;//Temp variables(remove when having actual assets)

    private Tuple<bool, int> shouldStopMoving = new Tuple<bool, int>(false, 0);

    public StrokeTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath, float strokeLength) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;

        stroke = new Stroke(this.assets + $"/strokeExample.png", shouldMoveLeft, strokeLength);
        AddChild(stroke);

        stroke.SetXY(shouldMoveLeft ? strokeXOffset : -strokeXOffset, strokeYOffset);
    }

    private void Update()
    {
        if (stroke == null)
            stroke = FindObjectOfType<Stroke>();

        if (!shouldStopMoving.Item1)
            Move();
    }

    public override int CheckPosition(int reactionDistance, Vector2 leftRecordCoor, Vector2 rightRecordCoor)
    {
        int distanceFromRecordCenter = (int)Mathf.Abs((shouldMoveLeft ? leftRecordCoor.y : rightRecordCoor.y) - this.y);

        if (distanceFromRecordCenter <= reactionDistance && shouldStopMoving.Item1 && !Input.GetKey(shouldStopMoving.Item2))
            shouldStopMoving = new Tuple<bool, int>(false, 0);

        bool conditionLeftLane = shouldMoveLeft && ((isLeft && Input.GetKey(Key.A)) || (!isLeft && Input.GetKey(Key.D)));
        bool conditionRightLane = !shouldMoveLeft && ((isLeft && Input.GetKey(Key.J)) || (!isLeft && Input.GetKey(Key.L)));
        if ((conditionLeftLane || conditionRightLane)
            && distanceFromRecordCenter <= reactionDistance)
        {
            int keyCode = conditionLeftLane ? (isLeft ? Key.A : Key.D) : (isLeft ? Key.J : Key.L);
            shouldStopMoving = new Tuple<bool, int>(true, keyCode);

            return reactionDistance - distanceFromRecordCenter;
        }
        return 0;
    }
}