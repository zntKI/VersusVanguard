using System;
using GXPEngine;
using GXPEngine.Core;

class Ui : GameObject
{

    // menuTile array
    Sprite titleImage;
    Sprite backgroundImage;
    Sprite[] backgroundTiles = new Sprite[2];
    MenuTile[] menuTiles = new MenuTile[3];
    int tilesToRender;
    int[] renderedTiles;
    int currentTile = 0;
    int[] bottomCenter = new int[2]   { 683, 380 };  // NOTE: this + or - 341.5 from center
    int[] bottomLeftUp = new int[2]   { 342, 360 };  // NOTE: this + or - 170.75 from sides
    int[] bottomRightUp = new int[2]  { 1024, 360 };
    int[] bottomLeftBack = new int[2] { 512, 310 };
    int[] bottomRightBack = new int[2]{ 854, 310 };
    int[] hidden = new int[2]         { -100, -100 };

    

    // dynamically load menuTiles

    public Ui()
    {
        // NOTE : This is a placeholder for the menuTiles untill the json configs are implemented
        titleImage = new Sprite("uiAssets/Title_proto.png");
        backgroundImage = new Sprite("uiAssets/bg_proto.png");
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
        menuTiles[0] = new MenuTile( "level1" );
        menuTiles[1] = new MenuTile( "level2" );
        menuTiles[2] = new MenuTile( "level3" );

        tilesToRender = menuTiles.Length;
    }

    void UpdateCurrentTile()            //NOTE: refactor this when controller is added
    {
        if ( Input.GetKeyDown(Key.LEFT) )
        {
            currentTile = currentTile == 0 ? renderedTiles.Length-1 : currentTile - 1;
        } else if ( Input.GetKeyDown(Key.RIGHT) )
        {
            currentTile = currentTile == renderedTiles.Length-1 ? 0 : currentTile + 1;
        } else if ( Input.GetKeyDown(Key.SPACE) )
        {
            // Maybe use to enable audio preview or debug mode ?
            // Console.WriteLine("");
            
        } else if ( Input.GetKeyDown(Key.ENTER) )
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
