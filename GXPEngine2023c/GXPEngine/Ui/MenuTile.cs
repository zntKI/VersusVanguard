using System;
using GXPEngine;
using GXPEngine.Core;

/*
    Should have track image, title, and dificulty
    Difficulty should/could have a small png ( higher the difficulty, the higher the png count )
    write method to mount level assets to level class ( in constructor perhaps ?)
*/

class MenuTile : Sprite
{

    public MenuTile( String filename, bool keepInCache=false, bool addCollider=true ) : base("../../assets/uiAssets/SongTile_proto.png")
    {
        // use Level config to get level info
        if (Game.main == null) {
				throw new Exception ("Sprites cannot be created before creating a Game instance.");
			}
			name = filename;
			initializeFromTexture(Texture2D.GetInstance(filename, keepInCache));
    }

    public MenuTile() : base("../../assets/uiAssets/SongTile_proto.png")
    {
        // Empty
    }

    void Update()
    {
        // Empty
    }

    void loadLevel()
    {
        // Empty
    }

    void LoadLevelConfig()
    {
        // Empty
    }

}

