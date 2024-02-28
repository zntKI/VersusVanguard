using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tile : Sprite
{
    private Vector2 spawnPointIfGoingLeft = new Vector2(630, 217);
    private Vector2 spawnPointIfGoingRight = new Vector2(731, 217);

    protected bool shouldMoveLeft;

    protected float speed;
    protected float sidewaysMoveAmount;
    protected float scaleIncrement;

    private string soundPath;

    /*Think about where the records coordinates belong (either in Level or Tile class)*/
    public Tile(string filename, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath) : base(filename, false, false)
    {
        SetOrigin(width / 2, height / 2);

        float startScale = scale / 2;
        SetScaleXY(startScale);

        this.speed = speed;
        this.shouldMoveLeft = shouldMoveLeft;
        SetXY(shouldMoveLeft ? spawnPointIfGoingLeft.x : spawnPointIfGoingRight.x, spawnPointIfGoingLeft.y);

        float distanceToMoveX = shouldMoveLeft ? (this.x - leftDiscCoor.x) : (rightDiscCoor.x - this.x);
        float distanceToMoveY = shouldMoveLeft ? (leftDiscCoor.y - this.y) : (rightDiscCoor.y - this.y);

        float framesToMove = distanceToMoveY / speed;
        sidewaysMoveAmount = distanceToMoveX / framesToMove;

        scaleIncrement = startScale / framesToMove;

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

        if ((Input.GetKeyDown(Key.G) || ControllerManager.GetButtonValue() == 0) && distanceFromRecordCenter <= reactionDistance)
        {
            Destroy();
            return reactionDistance - distanceFromRecordCenter;
        }
        return 0;
    }
}