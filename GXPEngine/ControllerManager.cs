using System;
using GXPEngine;
using GXPEngine.Core;
using System.IO.Ports;

public class ControllerManager
{
    private SerialPort port;

    private static int currentRecordRightValue;
    private static int currentRecordLeftValue;
    private static int currentButtonValue;

    public ControllerManager(string portName, int baudRate=9600)
	{
        port = new SerialPort();
        port.PortName = portName;
        port.BaudRate = baudRate;
        port.RtsEnable = true;
        port.DtrEnable = true;
        port.Open();
    }

    public void Step()
    {
        string line = port.ReadLine();

        if (line == "")
            return;

        string[] inputParts = line.Split(',');

        if (inputParts.Length != 3)
            return;

        int.TryParse(inputParts[0].Substring(inputParts[0].IndexOf(':') + 1), out currentRecordRightValue);
        int.TryParse(inputParts[1].Substring(inputParts[1].IndexOf(':') + 1), out currentRecordLeftValue);
        int.TryParse(inputParts[2].Substring(inputParts[2].IndexOf(':') + 1), out currentButtonValue);

        //Console.WriteLine($"{currentRecordRightValue} {currentRecordLeftValue} {currentButtonValue}");
    }

    public static int GetRightRecordValue() => currentRecordRightValue;
    public static int GetLeftRecordValue() => currentRecordLeftValue;
    public static int GetButtonValue() => currentButtonValue;
}