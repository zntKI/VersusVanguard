using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Level : GameObject
{
    //The different notes that can be played
    private List<Sound> melody;

    private Vector2 leftDiscCoor = new Vector2(444, 640);
    private Vector2 rightDiscCoor = new Vector2(920, 640);

    private int bpm;
	private int timeBetweenBeatsMS;

    //what is the lowest amount of time after which a tile could be spawned
    //(could be later used for difficulty adjustments and also linked to the speed with which the tile will be moving)
    private int spawnTimeMin;

    private int randomTimeSpawnTileMS;
    private int counterTimeSpawnTileMS;


	public Level(int bpm)
	{
		this.bpm = bpm;
        timeBetweenBeatsMS = (60 / bpm) * 1000;
        spawnTimeMin = (int)(timeBetweenBeatsMS * 0.8);//Possible difficulty adjustments
        randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);

        AddChild(new Sprite("Background_sketch.png", false, false));//Figure out better solution for background

        counterTimeSpawnTileMS = 0;
    }

	private void Update()
    {
        //Spawn the tile with the random sound from the list based on bpm
        ManageTileSpawning();
    }

    private void ManageTileSpawning()
    {
        counterTimeSpawnTileMS += Time.deltaTime;

        if (counterTimeSpawnTileMS >= randomTimeSpawnTileMS)
        {
            //Spawn tile

            bool shouldTileMoveLeft = Utils.Random(1, 3) == 1;

            Tile tileToSpawn;
            int tileToSpawnNum = Utils.Random(1, 4);//Dictates which tile to spawn
            switch (tileToSpawnNum)
            {
                case 1:
                    {
                        //TODO: Fix this later:
                        int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                        string filename = dirNum == 1 ? "dirTileLeftExample" : "dirTileRightExample";
                        tileToSpawn = new DirectionTile($"{filename}.png", dirNum == 1, 5f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");
                        break;
                    }
                case 2:
                    {
                        //TODO: Fix this later:
                        int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                        string filename = dirNum == 1 ? "strokeTileLeftExample" : "strokeTileRightExample";
                        tileToSpawn = new StrokeTile($"{filename}.png", dirNum == 1, 5f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "", 0f/*TODO: Fix that later*/);
                        break;
                    }
                case 3:
                    tileToSpawn = new Tile("denyTileExample.png", 5f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");
                    break;
                default:
                    throw new InvalidOperationException("Wrong number for spawning tiles");
            }

            AddChild(tileToSpawn);

            counterTimeSpawnTileMS = 0;
            randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);
        }
    }
}