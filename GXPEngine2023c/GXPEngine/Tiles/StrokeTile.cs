using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StrokeTile : Tile
{
    public StrokeTile(string filename, bool keepInCache = false, bool addCollider = false) : base(filename, keepInCache, addCollider)
    {
    }
}