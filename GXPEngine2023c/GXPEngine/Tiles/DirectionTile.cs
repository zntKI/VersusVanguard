using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class DirectionTile : Tile
{
    public DirectionTile(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
    }
}
