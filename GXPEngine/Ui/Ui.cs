using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using GXPEngine;
using GXPEngine.Core;

class Ui : GameObject
{

    // menuTile array
    Sprite titleImage;
    Sprite backgroundImage;
    Sprite[] backgroundTiles = new Sprite[2];
    MenuTile[] menuTiles = new MenuTile[3];
    EasyDraw scoreDisplayer;
    string scoreText = "ScorE Board:";
    int tilesToRender;
    int[] renderedTiles;
    int currentTile = 0;
	FileReader fileReader;		
    int[] bottomCenter = new int[2]   { 683, 380 };  // NOTE: this + or - 341.5 from center
    int[] bottomLeftUp = new int[2]   { 342, 360 };  // NOTE: this + or - 170.75 from sides
    int[] bottomRightUp = new int[2]  { 1024, 360 };
    int[] bottomLeftBack = new int[2] { 512, 310 };
    int[] bottomRightBack = new int[2]{ 854, 310 };
    int[] hidden = new int[2]         { -100, -100 };

    private Tuple<bool, bool> hasChangedTile = new Tuple<bool, bool>(false, false);

    // dynamically load menuTiles

    public Ui( string config = "levelConfig.xml" )
    {
        fileReader = new FileReader( config );
        // NOTE : This is a placeholder for the menuTiles untill the json configs are implemented
        titleImage = new Sprite("uiAssets/Title.png");
        backgroundImage = new Sprite("uiAssets/background.png");
        LoadMenuTiles();
    }

    void Update()
    {
        // if nothing rendered then render once
        if ( renderedTiles == null) {
            Render();
        }

        UpdateCurrentTile();
    }

    void LoadMenuTiles()
    {
        /*  // NOTE : Uncomment this to debug the menuTiles
            menuTiles[0] = new MenuTile("level1"); 
            menuTiles[1] = new MenuTile("level2"); 
            menuTiles[2] = new MenuTile("level3"); 
        */


        List<Dictionary<string,string>> ganeratedMenuTiles = fileReader.GenerateMenuTiles();
        int menuTileIndex = 0;
        foreach (Dictionary<string,string> tile in ganeratedMenuTiles)
        {
            menuTiles[menuTileIndex] = new MenuTile( tile );
            menuTileIndex++;
        }


        tilesToRender = menuTiles.Length;
    }

    void UpdateCurrentTile()            //NOTE: refactor this when controller is added
    {
        if ((ControllerManager.GetLeftRecordValue() == 0 && hasChangedTile.Item2) || (ControllerManager.GetRightRecordValue() == 0 && !hasChangedTile.Item2))
        {
            hasChangedTile = new Tuple<bool, bool>(false, false);
        }

        if ( (ControllerManager.GetLeftRecordValue() == -1 || ControllerManager.GetRightRecordValue() == -1) &&
            !hasChangedTile.Item1/*Input.GetKeyDown(Key.LEFT)*/ )
        {
            currentTile = currentTile == 0 ? renderedTiles.Length-1 : currentTile - 1;
            hasChangedTile = new Tuple<bool, bool>(true, ControllerManager.GetLeftRecordValue() == -1 ? true : false);
        } else if ( (ControllerManager.GetLeftRecordValue() == 1 || ControllerManager.GetRightRecordValue() == 1) &&
            !hasChangedTile.Item1/*Input.GetKeyDown(Key.RIGHT)*/ )
        {
            currentTile = currentTile == renderedTiles.Length-1 ? 0 : currentTile + 1;
            hasChangedTile = new Tuple<bool, bool>(true, ControllerManager.GetLeftRecordValue() == 1 ? true : false);
        } else if ( Input.GetKeyDown(Key.SPACE) )
        {
            // Maybe use to enable audio preview or debug mode ?
            // Console.WriteLine("");
            
        } else if (ControllerManager.GetButtonValue() == 0/*Input.GetKeyDown(Key.ENTER)*/ )
        {
            Console.WriteLine( "Loading level" );
            menuTiles[currentTile].LoadLevel();
            UnLoadUi();
            menuTiles[currentTile].level.LoadLevel();
            return;
        } 

        // if currentTile is not rendered then render
        if ( currentTile != renderedTiles[0] ) Render();
    }

    void UnloadMenuTiles()
    {
        foreach ( GameObject child in parent.GetChildren() )
        {
            if ( child is Ui) continue;
            parent.RemoveChild( child );
        }
    }

    void UnLoadUi()
    {
        foreach ( GameObject child in parent.GetChildren() )
        {
            if ( child is Level ) continue;
            child.LateDestroy();
        }
    }

    void Render()
    {

        UnloadMenuTiles();

        // render background and title
        backgroundTiles[0] = new Sprite("uiAssets/SongTile_proto.png");
        backgroundTiles[1] = new Sprite("uiAssets/SongTile_proto.png");
        parent.AddChild( backgroundImage );
        parent.AddChild( titleImage );
        parent.AddChild( backgroundTiles[0] );
        parent.AddChild( backgroundTiles[1] );

        // set amount of tiles to render
        renderedTiles = new int[tilesToRender];

        // sort array with currentTile first second tile after, previous tile third and the rest after
        for ( int i = 0; i < renderedTiles.Length; i++ )
        {
            if ( i == 0 ) { 
                renderedTiles[i] = currentTile;
            } else if ( i == 2 ) {
                renderedTiles[i] = (currentTile+2)%3; 
            } else if ( i == 1 ) {
                renderedTiles[i] = (currentTile+1)%3; 
            }
        }

        SetTilePositions();

        // render menuTiles
        foreach ( int tile in renderedTiles )
        {
            parent.AddChild( menuTiles[tile] );
        }

        RenderScores();
    }

    void RenderScores()
    {
        // render scores
        string[] scores = menuTiles[currentTile].levelScores;
        string finalScore = "";

        scoreDisplayer = new EasyDraw(600, 100, false);
        scoreDisplayer.TextFont( Utils.LoadFont("ElectroShackle-Yrvy.ttf", 15) );
        scoreDisplayer.SetXY(0, 20);
        scoreDisplayer.Fill(Color.White);

        foreach ( string score in scores )
        {
            // Console.WriteLine( score );
            finalScore = finalScore + score + "\n";
        }

        scoreDisplayer.Text(this.scoreText+"\n"+finalScore, true);
        parent.AddChild(scoreDisplayer);
    }

    void SetTilePositions()
    {
        foreach ( int tile in renderedTiles )
        {
            if ( tile == currentTile) 
            {
                menuTiles[tile].SetScaleXY( 0.8f, 0.8f );
                menuTiles[tile].SetXY( bottomCenter[0]-menuTiles[tile].width/2 , bottomCenter[1] );

            } else if ( tile == (currentTile+2)%3)
            {
                menuTiles[tile].SetScaleXY( 0.5f, 0.5f );
                menuTiles[tile].SetXY( bottomLeftUp[0]-menuTiles[tile].width/2, bottomLeftUp[1] );

            } else if ( tile == (currentTile+1)%3)
            {
                menuTiles[tile].SetScaleXY( 0.5f, 0.5f );
                menuTiles[tile].SetXY( bottomRightUp[0]-menuTiles[tile].width/2, bottomRightUp[1] );

            } 
        }
        
        // set scale of backgroundTiles
        backgroundTiles[0].SetScaleXY( 0.3f, 0.3f );
        backgroundTiles[1].SetScaleXY( 0.3f, 0.3f );
        
        backgroundTiles[0].SetXY( bottomLeftBack[0]-backgroundTiles[0].width/2, bottomLeftBack[1] );
        backgroundTiles[1].SetXY( bottomRightBack[0]-backgroundTiles[1].width/2, bottomRightBack[1] );
    }

}
