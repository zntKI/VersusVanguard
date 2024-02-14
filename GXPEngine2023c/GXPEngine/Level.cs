using GXPEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Level : GameObject
{
    private List<Sound> melody;

    private int bpm;
	private int timeBetweenBeatsMS;

    private int randomTimeSpawnTileMS;
    private int counterTimeSpawnTileMS;

	public Level(int bpm)
	{
		this.bpm = bpm;
        timeBetweenBeatsMS = (60 / bpm) * 1000;
        randomTimeSpawnTileMS = Utils.Random(1, timeBetweenBeatsMS);

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
            //Spawn tile (automate this later)
            DirectionTile dirTile = new DirectionTile("dirTileLeftExample.png", 5f, .05f, "", true);
            dirTile.SetXY(game.width / 2 + dirTile.width, 0);
            AddChild(dirTile);

            counterTimeSpawnTileMS = 0;
            randomTimeSpawnTileMS = Utils.Random(1, timeBetweenBeatsMS);
        }
    }
}