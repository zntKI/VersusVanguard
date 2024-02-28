using System;
using GXPEngine;
using GXPEngine.Core;
using System.IO.Ports;

public class ControllerManager
{
    private SerialPort port;

    private int currentRecordRightValue;
    private int currentRecordLeftValue;
    private int currentButtonValue;

    public ControllerManager(string portName, int baudRate=9600)
	{
        port = new SerialPort();
        port.PortName = portName;
        port.BaudRate = baudRate;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
    }

    public void ReadInput()
    {
        string line = port.ReadLine();

        if (line == "")
            return;

        string[] inputParts = line.Split(',');

        if (inputParts.Length != 3)
            return;


        /*currentRecordRightValue = */int.TryParse(inputParts[0].Substring(inputParts[0].IndexOf(':') + 1), out currentRecordRightValue);
        /*currentRecordLeftValue = */int.TryParse(inputParts[1].Substring(inputParts[1].IndexOf(':') + 1), out currentRecordLeftValue);
        /*currentButtonValue = */int.TryParse(inputParts[2].Substring(inputParts[2].IndexOf(':') + 1), out currentButtonValue);

        Console.WriteLine($"{inputParts[0]} {currentRecordRightValue}");

        //try
        //{
        //}
        //catch (Exception)
        //{
        //    throw new Exception("The provided input from the Arduino is not in the right format");
        //}
    }
}
