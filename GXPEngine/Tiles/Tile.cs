using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tile : Sprite
{
    protected bool shouldMoveLeft;

    private float speed;
    private float sidewaysMoveAmount;
    private float scaleIncrement;

    private string soundPath;

    /*Think about where the records coordinates belong (either in Level or Tile class)*/
    public Tile(string filename, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath) : base(filename, false, false)
    {
        SetOrigin(width / 2, height / 2);
        SetScaleXY(scale * 2);//Remove that after having the final assets

        this.speed = speed;
        this.shouldMoveLeft = shouldMoveLeft;
        SetXY(shouldMoveLeft ? game.width / 2 - width : game.width / 2 + width, 0);//Fix that after having established the final spawn pos of the records

        float distanceToMoveX = shouldMoveLeft ? (this.x - leftDiscCoor.x) : (rightDiscCoor.x - this.x);
        float distanceToMoveY = shouldMoveLeft ? (leftDiscCoor.y - this.y) : (rightDiscCoor.y - this.y);

        float framesToMove = distanceToMoveY / speed;
        sidewaysMoveAmount = distanceToMoveX / framesToMove;

        float endScaleAmount = (177 * scale) / width;
        scaleIncrement = (endScaleAmount - scale) / framesToMove;

        this.soundPath = soundPath;
    }

    private void Update()
    {
        Move();
    }

    public virtual void Move()
    {
        x += shouldMoveLeft ? -sidewaysMoveAmount : sidewaysMoveAmount;
        y += speed;
        if (y > game.height)
            Destroy();

        scale += scaleIncrement;
    }

    public virtual int CheckPosition(int reactionDistance, Vector2 leftRecordCoor, Vector2 rightRecordCoor)
    {
        int distanceFromRecordCenter = (int)Mathf.Abs((shouldMoveLeft ? leftRecordCoor.y : rightRecordCoor.y) - this.y);

        if (Input.GetKeyDown(Key.G) && distanceFromRecordCenter <= reactionDistance)
        {
            Destroy();
            return reactionDistance - distanceFromRecordCenter;
        }
        return 0;
    }
}