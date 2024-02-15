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

    public StrokeTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath, float strokeLength) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;
    }

    private void Update()
    {
        Move();
    }
}