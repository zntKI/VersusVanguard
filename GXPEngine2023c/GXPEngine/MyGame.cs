
using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;

public class MyGame : Game {  // Resolution is 1366 x 768
	public MyGame() : base(1366, 768, false, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		//Ui ui = new Ui();
		//AddChild(ui);

		AddChild(new Level(60));
    }

	void Update() {
		// Empty
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}