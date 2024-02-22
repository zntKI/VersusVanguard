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
    private float tempStrokeXOffset = 4f;
    private float tempStrokeYOffset = -11f;

    public StrokeTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath, float strokeLength) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;
        
        stroke = new Stroke(this.assets + $"/strokeExample.png", shouldMoveLeft, strokeLength);
        AddChild(stroke);

        stroke.SetXY(shouldMoveLeft ? tempStrokeXOffset : -tempStrokeXOffset, tempStrokeYOffset);
    }

    private void Update()
    {
        if (stroke == null)
            stroke = FindObjectOfType<Stroke>();

        Move();
    }

    public override int CheckPosition(int reactionDistance, Vector2 leftRecordCoor, Vector2 rightRecordCoor)
    {
        throw new NotImplementedException("Temporarily Stroke tiles don't exist in the game"); //Implement this after the first play testing session has passed
    }
}