
using System;
using GXPEngine;
using System.Drawing;

public class MyGame : Game {
	public MyGame() : base(1366, 768, false)     // Resolution is 1366 x 768
	{
		Ui ui = new Ui();
		AddChild(ui);
    }

	void Update() {
		// Empty
	}

	static void Main()                          
	{
		new MyGame().Start();                  
	}
}