using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;

public class MyGame : Game {
	public MyGame() : base(1366, 768, false, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		//Delete this later
		Level level = new Level(60, "60 BPM Four to the Floor.mp3");
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