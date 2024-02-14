using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game {
	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{   
		//Delete this later
		//DirectionTile dirTile = new DirectionTile("dirTileLeftExample.png", 5f, .05f, "", true);
		//dirTile.SetXY(width / 2 + dirTile.width, 0);
		//AddChild(dirTile);
	}

	// For every game object, Update is called every frame, by the engine:
	void Update() {
		// Empty
	}

	static void Main()                          // Main() is the first method that's called when the program is run
	{
		new MyGame().Start();                   // Create a "MyGame" and start it
	}
}