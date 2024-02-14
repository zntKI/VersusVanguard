using System;
using GXPEngine;

class Ui : GameObject
{

    // menuTile array
    MenuTile[] menuTiles = new MenuTile[3];
    

    // dynamically load menuTiles

    public Ui()
    {
        // NOTE : This is a placeholder for the menuTiles untill the json configs are implemented
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();
        menuTiles[3] = new MenuTile();

        // title image
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();
        menuTiles[3] = new MenuTile();

        // background image
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();
        menuTiles[3] = new MenuTile();

        // background sound
        menuTiles[0] = new MenuTile();
        menuTiles[1] = new MenuTile();
        menuTiles[2] = new MenuTile();
        menuTiles[3] = new MenuTile();

    }

    void Update()
    {
        // logic for swapping between menuTiles
    }
}
