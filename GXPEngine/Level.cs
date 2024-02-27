using GXPEngine;
using GXPEngine.Core;
using System;
using System.Collections.Generic;
using System.Drawing;
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

<<<<<<< HEAD
    //Variables for wait time after a stroke tile has been spawned
    private int leftLaneWaitTimeMS;
    private int rightLaneWaitTimeMS;
    private int leftLaneWaitTimeMSCounter;
    private int rightLaneWaitTimeMSCounter;

=======
>>>>>>> test
    private int reactionDistance = 50;

    private int score;
    private EasyDraw scoreDisplayer;//Temporary way to display score (think of a better way after the playtesting session)

    public bool levelLoaded = false;

    public Level(int bpm)
    {
        this.bpm = bpm;
        timeBetweenBeatsMS = (60 / bpm) * 1000;
        spawnTimeMin = (int)(timeBetweenBeatsMS * 0.8);//Possible difficulty adjustments
        randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);

        AddChild(new Sprite(this.assets + "/Background_sketch.png", false, false));//Figure out better solution for background
        
        //Temporary way to display score (think of a better way after the playtesting session)
        scoreDisplayer = new EasyDraw(200, 70, false);
        scoreDisplayer.TextSize(20);
        scoreDisplayer.TextAlign(CenterMode.Center, CenterMode.Center);
        scoreDisplayer.Fill(Color.Black);
        scoreDisplayer.Text($"Score: {score}", true);
        scoreDisplayer.SetXY(0, 0);
        AddChild(scoreDisplayer);

        counterTimeSpawnTileMS = 0;
    }

    private void Update()
    {
<<<<<<< HEAD
        if ( levelLoaded == true )
=======
        if ( levelLoaded == false )
>>>>>>> test
        {
            return;
        }

        //Spawn the tile with the random sound from the list based on bpm
        ManageTileSpawning();
        CheckForInput();

        scoreDisplayer.Text($"Score: {score}", true);//Temporary way to display score (think of a better way after the playtesting session)
    }

    public void LoadLevel()
    {
        //Load level assets
        //LoadLevelConfig();
        levelLoaded = true;
    }

    private void ManageTileSpawning()
    {
        counterTimeSpawnTileMS += Time.deltaTime;

<<<<<<< HEAD
        if (leftLaneWaitTimeMS != 0)
            leftLaneWaitTimeMSCounter += Time.deltaTime;
        if (rightLaneWaitTimeMS != 0)
            rightLaneWaitTimeMSCounter += Time.deltaTime;


=======
>>>>>>> test
        if (counterTimeSpawnTileMS >= randomTimeSpawnTileMS)
        {
            //Spawn tile

            bool shouldTileMoveLeft = Utils.Random(1, 3) == 1;

<<<<<<< HEAD
            //Skips tile spawning if the specified amount of time has not yet passed since the last spawn of a stroke tile in the given lane
            if ((shouldTileMoveLeft && leftLaneWaitTimeMS != 0 && leftLaneWaitTimeMSCounter < leftLaneWaitTimeMS) ||
                (!shouldTileMoveLeft && rightLaneWaitTimeMS != 0 && rightLaneWaitTimeMSCounter < rightLaneWaitTimeMS))
                return;

            Tile tileToSpawn = Spawn(shouldTileMoveLeft);
=======
            Tile tileToSpawn;
            int tileToSpawnNum = Utils.Random(1, 3);//Dictates which tile to spawn
            switch (tileToSpawnNum)
            {
                case 1:
                    {
                        //TODO: Fix this later:
                        int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                        string filename = dirNum == 1 ? "dirTileLeftExample" : "dirTileRightExample";
                        tileToSpawn = new DirectionTile(this.assets + $"/{filename}.png", dirNum == 1, 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");
                        break;
                    }
                //case 2: //Uncomment this after the first play testing session has passed
                //    {
                //        //TODO: Fix this later:
                //        int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                //        string filename = dirNum == 1 ? "strokeTileLeftExample" : "strokeTileRightExample";
                //        tileToSpawn = new StrokeTile($"{filename}.png", dirNum == 1, 5f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "", 0f/*TODO: Fix that later*/);
                //        break;
                //    }
                case 2:
                    tileToSpawn = new Tile(this.assets + "/denyTileExample.png", 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");
                    break;
                default:
                    throw new InvalidOperationException("Wrong number for spawning tiles");
            }

>>>>>>> test
            AddChild(tileToSpawn);

            counterTimeSpawnTileMS = 0;
            randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);
        }
    }

<<<<<<< HEAD
    private Tile Spawn(bool shouldTileMoveLeft)
    {
        Tile tileToSpawn;
        int tileToSpawnNum = Utils.Random(1, 4);//Dictates which tile to spawn
        switch (tileToSpawnNum)
        {
            case 1:
                {
                    //TODO: Fix this later:
                    int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                    string filename = dirNum == 1 ? "dirTileLeftExample" : "dirTileRightExample";
                    tileToSpawn = new DirectionTile(this.assets + $"/{filename}.png", dirNum == 1, 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");

                    //TODO: Figure out a better solution
                    if (shouldTileMoveLeft)
                        leftLaneWaitTimeMS = 0;
                    else
                        rightLaneWaitTimeMS = 0;
                    break;
                }
            case 2:
                {
                    //TODO: Fix this later:
                    int dirNum = Utils.Random(1, 3);//Dictates tile's direction
                    string filename = dirNum == 1 ? "strokeTileLeftExample" : "strokeTileRightExample";
                    tileToSpawn = new StrokeTile($"{filename}.png", dirNum == 1, 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "", 5f/*Fix this later(make it not hardcoded)*/);

                    if (shouldTileMoveLeft)
                    {
                        leftLaneWaitTimeMS = 5000;/*Link it to the stroke length; Fix this later(make it not hardcoded)*/
                        leftLaneWaitTimeMSCounter = 0;
                    }
                    else
                    {
                        rightLaneWaitTimeMS = 5000;/*Link it to the stroke length; Fix this later(make it not hardcoded)*/
                        rightLaneWaitTimeMSCounter = 0;
                    }

                    break;
                }
            case 3:
                tileToSpawn = new Tile(this.assets + "/denyTileExample.png", 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");

                //TODO: Figure out a better solution
                if (shouldTileMoveLeft)
                    leftLaneWaitTimeMS = 0;
                else
                    rightLaneWaitTimeMS = 0;

                break;
            default:
                throw new InvalidOperationException("Wrong number for spawning tiles");
        }

        return tileToSpawn;
    }

    private void CheckForInput()
    {
=======
    private void CheckForInput()
    {
        if (!Input.AnyKey() && !Input.AnyKeyDown())
            return;

>>>>>>> test
        var tilesInScene = this.GetChildren().Where(obj => obj is Tile);

        foreach (var tile in tilesInScene)
        {
            //Check if the current tile in the reaction zone
            score += ((Tile)tile).CheckPosition(reactionDistance, leftDiscCoor, rightDiscCoor);
        }
<<<<<<< HEAD
=======

        Console.WriteLine(score);
>>>>>>> test
    }
}