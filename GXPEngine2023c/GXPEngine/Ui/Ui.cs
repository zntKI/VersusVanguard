using System;
using GXPEngine;
using GXPEngine.Core;

class Ui : GameObject
{

    // menuTile array
    Sprite titleImage = new Sprite("uiAssets/Title_proto.png");
    Sprite backgroundImage = new Sprite("uiAssets/bg_proto.png");
    Sprite[] backgroundTiles = new Sprite[2];
    MenuTile[] menuTiles = new MenuTile[3];
    int tilesToRender = 3;
    int[] renderedTiles;
    int currentTile = 0;
    int[] bottomCenter = new int[2]   { 525, 380 };
    int[] bottomLeftUp = new int[2]   { 150, 360 };
    int[] bottomRightUp = new int[2]  { 1050, 360 };
    int[] bottomLeftBack = new int[2] { 425, 310 };
    int[] bottomRightBack = new int[2]{ 825, 310 };

    

    // dynamically load menuTiles

    public Ui()
    {
        // NOTE : This is a placeholder for the menuTiles untill the json configs are implemented
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();

        // title image
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();

        // background image
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();

        // background sound
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();

    }

    void Update()
    {
        // logic for swapping between menuTiles
        if ( renderedTiles == null )
        {
            Render();
        }
        
        UpdateCurrentTile();
    }

    void UpdateCurrentTile()
    {
        if ( Input.GetKeyDown(Key.LEFT) )
        {
            currentTile = currentTile == 0 ? renderedTiles.Length-1 : currentTile - 1;
        } else if ( Input.GetKeyDown(Key.RIGHT) )
        {
            currentTile = currentTile == renderedTiles.Length-1 ? 0 : currentTile + 1;
        }
    }

    void Render()
    {
        // render background and title
        backgroundTiles[0] = new Sprite("uiAssets/SongTile_proto.png");
        backgroundTiles[1] = new Sprite("uiAssets/SongTile_proto.png");
        parent.AddChild( backgroundImage );
        parent.AddChild( titleImage );
        parent.AddChild( backgroundTiles[0] );
        parent.AddChild( backgroundTiles[1] );

        // set amount of tiles to render
        renderedTiles = new int[tilesToRender];

        // render currentTile first then other 2 tiles
        renderedTiles[0] = currentTile;
        renderedTiles[1] = currentTile == 0 ? renderedTiles.Length-1 : currentTile - 1;
        renderedTiles[2] = currentTile == renderedTiles.Length-1 ? 0 : currentTile + 1;

        //set position of menuTiles
        SetTilePositions();

        // render menuTiles
        foreach ( int tile in renderedTiles )
        {
            parent.AddChild( menuTiles[tile] );
        }
    }

    void SetTilePositions()
    {
        // set position of menuTiles
        menuTiles[0].SetXY( bottomCenter[0], bottomCenter[1] );
        menuTiles[2].SetXY( bottomRightUp[0], bottomRightUp[1] );
        menuTiles[1].SetXY( bottomLeftUp[0], bottomLeftUp[1] );
        backgroundTiles[0].SetXY( bottomLeftBack[0], bottomLeftBack[1] );
        backgroundTiles[1].SetXY( bottomRightBack[0], bottomRightBack[1] );

        menuTiles[0].SetScaleXY( 0.8f, 0.8f );
        menuTiles[1].SetScaleXY( 0.5f, 0.5f );
        menuTiles[2].SetScaleXY( 0.5f, 0.5f );
        backgroundTiles[0].SetScaleXY( 0.3f, 0.3f );
        backgroundTiles[1].SetScaleXY( 0.3f, 0.3f );
    }

}
