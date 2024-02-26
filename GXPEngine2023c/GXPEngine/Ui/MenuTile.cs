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
    string levelAssets = "../../assets/levelAssets/";
    string levelName;
    string artLocation;
    string soundLocation;
    // bool   audioPreview = false;

    public Level level;


    public MenuTile( String levelName ) : base("../../assets/uiAssets/SongTile_proto.png")
    {
        this.levelName = levelName;
        this.levelAssets += levelName;
        this.artLocation = levelAssets + "/art.png";
        this.soundLocation = levelAssets + "/sound.wav";

        if (Game.main == null) {
				throw new Exception ("Sprites cannot be created before creating a Game instance.");
			}

			name = artLocation;
			initializeFromTexture(Texture2D.GetInstance( artLocation , false ));
    }

    public MenuTile() : base("../../assets/uiAssets/SongTile_proto.png")
    {
        // Empty
    }

    void Update()
    {
        // Empty
    }

    public void LoadLevel()
    {
        level = new Level(60);
        level.SetLevelAssets(levelAssets, soundLocation);
        parent.AddChild(level);
    }

    void LoadLevelConfig()
    {
        // Empty
    }

}

