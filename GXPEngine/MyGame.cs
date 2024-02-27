
using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;
using System.IO;

public class MyGame : Game {  // Resolution is 1366 x 768
	public MyGame() : base(1366, 768, false, false)     // Create a window that's 800x600 and NOT fullscreen
	{
		FileReader fileReader = new FileReader(this.assets + "/levelAssets/level1/levelConfig.json");		
		fileReader.ReadJson();
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