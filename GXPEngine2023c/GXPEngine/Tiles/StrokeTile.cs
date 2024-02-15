using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StrokeTile : Tile
{
    private bool isLeft;
    private float strokeLength;

    public StrokeTile(string filename, bool isLeft, float speed, float scaleIncrement, bool shouldMoveLeft, string soundPath, float strokeLength) : base(filename, speed, scaleIncrement, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;
    }

    private void Update()
    {
        Move();
    }
}