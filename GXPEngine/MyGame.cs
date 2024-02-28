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
        //Console.WriteLine(ControllerManager.GetRightRecordValue());
    }

    static void Main()
    {
        //controllerManager = new ControllerManager();
        /*
        SerialPort port = new SerialPort();
        port.PortName = "COM3";
        port.BaudRate = 9600;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
        while (true)
        {
            string line = port.ReadLine(); // read separated values
                                           //string line = port.ReadExisting(); // when using characters
            if (line != "")
            {
                Console.WriteLine("Read from port: " + line);

            }

            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo key = Console.ReadKey();
                port.Write(key.KeyChar.ToString());  // writing a string to Arduino
            }
        }*/

        new MyGame().Start();
    }
}