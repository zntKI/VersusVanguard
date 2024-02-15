using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Level : GameObject
{
    //The different notes that can be played
    private List<Sound> melody;

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
        spawnTimeMin = timeBetweenBeatsMS / 2;
        randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);

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
            int tileToSpawnNum = Utils.Random(1, 4);
            switch (tileToSpawnNum)
            {
                case 1:
                    {
                        //TODO: Fix this later:
                        int dirNum = Utils.Random(1, 3);
                        string filename = dirNum == 1 ? "dirTileLeftExample" : "dirTileRightExample";
                        tileToSpawn = new DirectionTile($"{filename}.png", dirNum == 1, 5f, .05f, shouldTileMoveLeft, "");
                        break;
                    }
                case 2:
                    {
                        //TODO: Fix this later:
                        int dirNum = Utils.Random(1, 3);
                        string filename = dirNum == 1 ? "strokeTileLeftExample" : "strokeTileRightExample";
                        tileToSpawn = new StrokeTile($"{filename}.png", dirNum == 1, 5f, .05f, shouldTileMoveLeft, "", 0f/*TODO: Fix that later*/);
                        break;
                    }
                case 3:
                    tileToSpawn = new Tile("denyTileExample.png", 5f, .05f, shouldTileMoveLeft, "");
                    break;
                default:
                    throw new InvalidOperationException("Wrong number for spawning tiles");
            }

            tileToSpawn.SetXY(shouldTileMoveLeft ? game.width / 2 - tileToSpawn.width : game.width / 2 + tileToSpawn.width, 0);
            AddChild(tileToSpawn);

            counterTimeSpawnTileMS = 0;
            randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);
        }
    }
}