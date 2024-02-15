using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tile : Sprite
{
    private const float sidewaysMoveAmount = .8f;
    private bool shouldMoveLeft;

    private float speed;
    private float scaleIncrement; //TODO: could be calculated automatically by knowing the screen height and the max size it can reach

    private string soundPath;

    public Tile(string filename, float speed, float scaleIncrement, bool shouldMoveLeft, string soundPath) : base(filename, false, false)
    {
        SetOrigin(width / 2, height / 2);

        this.speed = speed;
        this.scaleIncrement = scaleIncrement;
        this.shouldMoveLeft = shouldMoveLeft;

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
}