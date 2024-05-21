using System;
using GXPEngine;
using System.Drawing;
using GXPEngine.Core;
using System.IO.Ports;
using System.IO;

public class MyGame : Game
{  // Resolution is 1366 x 768

    public ControllerManager controllerManager;

    public MyGame() : base(1366, 768, false, false)     // Create a window that's 800x600 and NOT fullscreen
    {
        controllerManager = new ControllerManager("COM3");
        OnBeforeStep += controllerManager.Step;

        Ui ui = new Ui();
        AddChild(ui);

        //AddChild(new Level(60));
    }

    void Update()
    {
    }

    static void Main()
    {
        new MyGame().Start();
    }
}