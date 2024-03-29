using System;
using System.Collections.Generic;
using GXPEngine;
using GXPEngine.Core;

class MenuTile : Sprite
{
    string levelName;
    FileReader scoresReader;
    Dictionary<string, string> levelConfig;
    public string[] levelScores;
    // bool   audioPreview = false;

    public Level level;


    public MenuTile( Dictionary<string, string> levelConfig ) : base("uiAssets/SongTile_proto.png")
    {
        this.levelConfig = levelConfig;
        this.levelName = levelConfig["levelName"];
        this.scoresReader = new FileReader( this.levelConfig["Scores"], "txt" );
        this.levelScores = scoresReader.UpdateScores();

        if (Game.main == null) {
				throw new Exception ("Sprites cannot be created before creating a Game instance.");
			}

			initializeFromTexture(Texture2D.GetInstance( levelConfig["Thumbnail"] , false ));
    }

    public MenuTile() : base("uiAssets/SongTile_proto.png")
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
        level.LoadLevelConfig( levelConfig );
        level.SetLevelAssets();
        parent.AddChild(level);
    }
}

