using System;
using GXPEngine;

/*
    Should have track image, title, and dificulty
    Difficulty should/could have a small png ( higher the difficulty, the higher the png count )
*/

class MenuTile : Sprite
{

    public MenuTile() : base("uiAssets/SongTile_proto.png")
    {
        // use Level config to get level info
    }

    void Update()
    {
        // Empty
    }
}

