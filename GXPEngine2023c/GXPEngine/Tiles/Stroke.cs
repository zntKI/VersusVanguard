using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Stroke : Sprite
{
    private const float strokeRotation = 15.8f;

    public Stroke(string filename, bool shouldTileMoveLeft, float length) : base(filename, false, false)
    {
        SetOrigin(width / 2, height);
        SetScaleXY(scaleX, scaleY * length);
        rotation = shouldTileMoveLeft ? strokeRotation : -strokeRotation;
    }
}