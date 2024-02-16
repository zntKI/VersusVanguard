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

    

    // dynamically load menuTiles

    public Ui()
    {
        // NOTE : This is a placeholder for the menuTiles untill the json configs are implemented
        titleImage = new Sprite(this.assets+"/uiAssets/Title_proto.png");
        backgroundImage = new Sprite(this.assets+"/uiAssets/bg_proto.png");

        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();
        tilesToRender = menuTiles.Length;

        //debugging colors
        menuTiles[0].SetColor( 0, 255, 0 );
        menuTiles[1].SetColor( 255, 0, 0 );
        menuTiles[2].SetColor( 0, 0, 255 );

        // title image
        // menuTiles[0].songimage;
        // menuTiles[1].songimage;
        // menuTiles[2].songimage;

        // background image
        // menuTiles[0].backgroundImage;
        // menuTiles[1].backgroundImage;
        // menuTiles[2].backgroundImage;

        // background sound
        // menuTiles[0].sound;
        // menuTiles[1].sound;
        // menuTiles[2].sound;

    }

    void Update()
    {
        // if nothing rendered then render once
        if ( renderedTiles == null) {
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
        } else if ( Input.GetKeyDown(Key.SPACE) )
        {
            foreach (int tile in renderedTiles)
            {
                Console.WriteLine( "Tile: " + tile );
            }
            Console.WriteLine( "\n");
            
        } else if ( Input.GetKeyDown(Key.ENTER) )
        {
            Console.WriteLine( currentTile );
            // menuTiles[currentTile].LoadLevel();
        } 

        // if currentTile is not rendered then render
        if ( currentTile != renderedTiles[0] ) Render();
    }

    void Render()
    {
        foreach ( GameObject child in parent.GetChildren() )
        {
            if ( child is Ui) continue;
            parent.RemoveChild( child );
        }

        // render background and title
        backgroundTiles[0] = new Sprite(this.assets+"/uiAssets/SongTile_proto.png");
        backgroundTiles[1] = new Sprite(this.assets+"/uiAssets/SongTile_proto.png");
        parent.AddChild( backgroundImage );
        parent.AddChild( titleImage );
        parent.AddChild( backgroundTiles[0] );
        parent.AddChild( backgroundTiles[1] );

        // set amount of tiles to render
        renderedTiles = new int[tilesToRender];

        // render currentTile first then other 2 tiles
        renderedTiles[0] = currentTile;
        renderedTiles[1] = currentTile == renderedTiles.Length-1 ? 0 : currentTile + 1;
        renderedTiles[2] = currentTile == 0 ? renderedTiles.Length-1 : currentTile - 1;

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
