using System;
using System.Collections.Generic;
using System.Xml;
using GXPEngine;

namespace GXPEngine
{
    public class FileReader
    {

        String filename;
        List<String> modes = new List<string> { "json", "xml", "txt" };
        String mode;
        XmlDocument xmlDoc = new XmlDocument();
        

        public FileReader( string filename, string mode = "xml")
        {
            this.filename = filename;

            if (!modes.Contains(mode))
            {
                throw new Exception("Invalid mode");
            } else 
            {
                this.mode = mode;
            }  
        }
        
        public void ReadFile()
        {
            if (mode == "json") ReadJson();
            if (mode == "xml")  ReadXml();
            if (mode == "txt")  ReadTxt();
        }


        private void ReadTxt()
        {
            Console.WriteLine("Reading txt file: " + filename);
            string[] lines = System.IO.File.ReadAllLines(filename);

            foreach (string line in lines)
            {
                Console.WriteLine(line);
            }
        }

        private void ReadJson()
        {
        //     string json = System.IO.File.ReadAllText(filename);
        //     dynamic data = Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        //     foreach (var item in data)
        //     {
        //         string assetsLocation = item.assetsLocation;
        //         string songLocation = item.songLocation;
        //         int bpm = item.bpm;
        //         Level level = new Level(bpm);
        //     }
        }

        private void ReadXml()
        {
            xmlDoc.Load(filename);
            Console.WriteLine( xmlDoc.DocumentElement.SelectSingleNode("LevelConfig/Level1").ChildNodes );
        }
    }
}