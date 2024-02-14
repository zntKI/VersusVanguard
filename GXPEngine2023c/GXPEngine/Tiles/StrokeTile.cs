using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StrokeTile : Tile
{
    private bool isLeft;
    private float strokeLength;

    public StrokeTile(string filename, float speed, float scaleIncrement, string soundPath, bool isLeft, float strokeLength) : base(filename, speed, scaleIncrement, soundPath)
    {
        this.isLeft = isLeft;
        this.strokeLength = strokeLength;
    }

    private void Update()
    {
        Move(false);
    }
}