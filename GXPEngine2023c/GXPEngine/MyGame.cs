
using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game {
	public MyGame() : base(1366, 768, false)     // Resolution is 1366 x 768
	{
		//Delete this later
		Level level = new Level(60);
		AddChild(level);
    }

	void Update() {
		// Empty
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}