using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectionTile : Tile
{
    private const float inputAmountMin = .5f;
    private bool isLeft;

    public DirectionTile(string filename, float speed, float scaleIncrement, string soundPath, bool isLeft) : base(filename, speed, scaleIncrement, soundPath)
    {
        this.isLeft = isLeft;
    }

    private void Update()
    {
        Move(false);
    }
}
