using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectionTile : Tile
{
    private const float inputAmountMin = .5f;
    private bool isLeft;

    public DirectionTile(string filename, bool isLeft, float speed, Vector2 leftDiscCoor, Vector2 rightDiscCoor, bool shouldMoveLeft, string soundPath) : base(filename, speed, leftDiscCoor, rightDiscCoor, shouldMoveLeft, soundPath)
    {
        this.isLeft = isLeft;
    }

    private void Update()
    {
        Move();
    }
}
