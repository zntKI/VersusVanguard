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
    private AnimationSprite crowd;

    private Vector2 leftDiscCoor = new Vector2(422, 640);
    private Vector2 rightDiscCoor = new Vector2(936, 640);

    private int bpm;
    private int timeBetweenBeatsMS;

    //what is the lowest amount of time after which a tile could be spawned
    //(could be later used for difficulty adjustments and also linked to the speed with which the tile will be moving)
    private int spawnTimeMin;

    private int randomTimeSpawnTileMS;
    private int counterTimeSpawnTileMS;

    //Variables for wait time after a stroke tile has been spawned
    private int leftLaneWaitTimeMS;
    private int rightLaneWaitTimeMS;
    private int leftLaneWaitTimeMSCounter;
    private int rightLaneWaitTimeMSCounter;

    private int reactionDistance = 50;

    private int score;
    private EasyDraw scoreDisplayer;//Temporary way to display score (think of a better way after the playtesting session)

    public bool levelLoaded = false;

    private int bgTopChildIndex;

    public Level(int bpm)
    {
        this.bpm = bpm;
        timeBetweenBeatsMS = (60 / bpm) * 1000;
        spawnTimeMin = (int)(timeBetweenBeatsMS * 0.8);//Possible difficulty adjustments
        randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);

        AddChild(new Sprite("levelTilesAssets/Background.png", false, false));//Figure out better solution for background

        crowd = new AnimationSprite("levelTilesAssets/crowd.png", 3, 4, -1, false, false);
        crowd.SetOrigin(crowd.width / 2, crowd.height / 2);
        AddChild(crowd);
        crowd.SetXY(game.width / 2, game.height / 2 + 50);
        crowd.SetCycle(0, 12, 2);

        AddChild(new Sprite("levelTilesAssets/table.png", false, false));

        Sprite bgTop = new Sprite("levelTilesAssets/Background_top.png", false, false);
        AddChild(bgTop);
        bgTopChildIndex = bgTop.Index;

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
        if (levelLoaded == true)
        {
            return;
        }

        crowd.Animate();

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
        if (leftLaneWaitTimeMS != 0)
            leftLaneWaitTimeMSCounter += Time.deltaTime;
        if (rightLaneWaitTimeMS != 0)
            rightLaneWaitTimeMSCounter += Time.deltaTime;


        if (counterTimeSpawnTileMS >= randomTimeSpawnTileMS)
        {
            //Spawn tile

            bool shouldTileMoveLeft = Utils.Random(1, 3) == 1;

            //Skips tile spawning if the specified amount of time has not yet passed since the last spawn of a stroke tile in the given lane
            if ((shouldTileMoveLeft && leftLaneWaitTimeMS != 0 && leftLaneWaitTimeMSCounter < leftLaneWaitTimeMS) ||
                (!shouldTileMoveLeft && rightLaneWaitTimeMS != 0 && rightLaneWaitTimeMSCounter < rightLaneWaitTimeMS))
                return;

            Tile tileToSpawn = Spawn(shouldTileMoveLeft);
            AddChild(tileToSpawn);
            this.SetChildIndex(tileToSpawn, bgTopChildIndex);

            counterTimeSpawnTileMS = 0;
            randomTimeSpawnTileMS = Utils.Random(spawnTimeMin, timeBetweenBeatsMS);
        }
    }

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
                    string filename = shouldTileMoveLeft ? (dirNum == 1 ? "recordLeftLeft" : "recordLeftRight") : (dirNum == 1 ? "recordRightLeft" : "recordRightRight");
                    tileToSpawn = new DirectionTile($"levelTilesAssets/{filename}.png", dirNum == 1, 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");

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
                    string filename = shouldTileMoveLeft ? (dirNum == 1 ? "recordStrokeLeftLeft" : "recordStrokeLeftRight") : (dirNum == 1 ? "recordStrokeRightLeft" : "recordStrokeRightRight");
                    tileToSpawn = new StrokeTile($"levelTilesAssets/{filename}.png", dirNum == 1, 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "", 5f/*Fix this later(make it not hardcoded)*/);

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
                {
                    string filename = shouldTileMoveLeft ? "strokeDenyLeft" : "strokeDenyRight";
                    tileToSpawn = new Tile($"levelTilesAssets/{filename}.png", 4f, leftDiscCoor, rightDiscCoor, shouldTileMoveLeft, "");

                    //TODO: Figure out a better solution
                    if (shouldTileMoveLeft)
                        leftLaneWaitTimeMS = 0;
                    else
                        rightLaneWaitTimeMS = 0;

                    break;
                }
            default:
                throw new InvalidOperationException("Wrong number for spawning tiles");
        }

        return tileToSpawn;
    }

    private void CheckForInput()
    {
        var tilesInScene = this.GetChildren().Where(obj => obj is Tile);

        foreach (var tile in tilesInScene)
        {
            //Check if the current tile in the reaction zone
            Tile currentTile = (Tile)tile;
            int scoreIncrement = currentTile.CheckPosition(reactionDistance, leftDiscCoor, rightDiscCoor);

            ReactionParticle reactionParticle;
            if (scoreIncrement >= 40)
            {
                reactionParticle = new ReactionParticle("levelTilesAssets/effectPerfect.png", 200);


                reactionParticle.SetScale(0.5f, 1f).
                    SetVelocity(0, Utils.Random(-0.1f, 0f));

                reactionParticle.SetXY(Utils.Random(currentTile.x - currentTile.width / 2, currentTile.x + currentTile.width / 2),
                    currentTile.y - currentTile.height / 2 - 40);
                reactionParticle.rotation = Utils.Random(-25, 26);
                AddChild(reactionParticle);
            }
            else if (scoreIncrement >= 20)
            {
                reactionParticle = new ReactionParticle("levelTilesAssets/effectGreat.png", 200);

                reactionParticle.SetScale(0.5f, 1f).
                    SetVelocity(0, Utils.Random(-0.1f, 0f));

                reactionParticle.SetXY(Utils.Random(currentTile.x - currentTile.width / 2, currentTile.x + currentTile.width / 2),
                    currentTile.y - currentTile.height / 2 - 40);
                reactionParticle.rotation = Utils.Random(-25, 26);
                AddChild(reactionParticle);
            }
            else if (scoreIncrement > 0)
            {
                reactionParticle = new ReactionParticle("levelTilesAssets/effectNice.png", 200);

                reactionParticle.SetScale(0.5f, 1f).
                    SetVelocity(0, Utils.Random(-0.1f, 0f));

                reactionParticle.SetXY(Utils.Random(currentTile.x - currentTile.width / 2, currentTile.x + currentTile.width / 2),
                    currentTile.y - currentTile.height / 2 - 40);
                reactionParticle.rotation = Utils.Random(-25, 26);
                AddChild(reactionParticle);
            }


            score += scoreIncrement;
        }
    }
}