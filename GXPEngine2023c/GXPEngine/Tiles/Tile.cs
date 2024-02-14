using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Tile : Sprite
{
    private const float sidewaysMoveAmount = .8f;

    private float speed;
    private float scaleIncrement;

    private string soundPath;

    public Tile(string filename, float speed, float scaleIncrement, string soundPath) : base(filename, false, false)
    {
        SetOrigin(width / 2, height / 2);

        this.speed = speed;
        this.scaleIncrement = scaleIncrement;
        this.soundPath = soundPath;
    }

    private void Update()
    {
        Move(true);
    }

    public virtual void Move(bool shouldMoveLeft)
    {
        x += shouldMoveLeft ? -sidewaysMoveAmount : sidewaysMoveAmount;
        y += speed;
        scale += scaleIncrement;
    }
}