using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stroke : Sprite
{
    private const float strokeRotation = 15.8f;

    public int strokeEndY;

    private bool isDetached;

    private float sidewaysMoveAmount;
    private float speed;

    public Stroke(string filename, bool shouldTileMoveLeft, float length) : base(filename, false, false)
    {
        SetOrigin(width / 2, height);
        SetScaleXY(scaleX, scaleY * length);
        rotation = shouldTileMoveLeft ? strokeRotation : -strokeRotation;
    }

    private void Update()
    {
        if (isDetached)
            Move();
    }

    private void Move()
    {
        x += sidewaysMoveAmount;
        y += speed;
    }

    public void Detach(float sidewaysMoveAmount, float speed, int tileStoppedY)
    { 
        isDetached = true;

        this.sidewaysMoveAmount = sidewaysMoveAmount;
        this.speed = speed;

        strokeEndY = (int)Mathf.Abs(Mathf.Cos(rotation) * height);
    }
}