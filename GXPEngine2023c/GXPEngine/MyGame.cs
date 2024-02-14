using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game {
	public MyGame() : base(800, 600, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		//Delete this later
		Level level = new Level(60);
		AddChild(level);
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