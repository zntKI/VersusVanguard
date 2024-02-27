using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectionTile : Tile
{
    private const float inputAmountMin = .5f;
    private bool isLeft;

    public DirectionTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
    }

    private void Update()
    {
        Move();
    }

    public override int CheckPosition(int reactionDistance, Vector2 leftRecordCoor, Vector2 rightRecordCoor)
    {
        int distanceFromRecordCenter = (int)Mathf.Abs((shouldMoveLeft ? leftRecordCoor.y : rightRecordCoor.y) - this.y);

        bool conditionLeftLane = shouldMoveLeft && ((isLeft && Input.GetKeyDown(Key.A)) || (!isLeft && Input.GetKeyDown(Key.D)));
        bool conditionRightLane = !shouldMoveLeft && ((isLeft && Input.GetKeyDown(Key.J)) || (!isLeft && Input.GetKeyDown(Key.L)));
        if ((conditionLeftLane || conditionRightLane)
            && distanceFromRecordCenter <= reactionDistance)
        {
            Destroy();
            return reactionDistance - distanceFromRecordCenter;
        }
        return 0;
    }
}
