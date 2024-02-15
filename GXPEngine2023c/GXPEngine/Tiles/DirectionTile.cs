using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectionTile : Tile
{
    private const float inputAmountMin = .5f;
    private bool isLeft;

    public DirectionTile(string filename, bool isLeft, float speed, float scaleIncrement, bool shouldMoveLeft, string soundPath) : base(filename, speed, scaleIncrement, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
    }

    private void Update()
    {
        Move();
    }
}
