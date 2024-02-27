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


        private string[] ReadTxt()
        {
            string[] lines = System.IO.File.ReadAllLines(filename);
            return lines;
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
            XmlNodeList nodeList = xmlDoc.DocumentElement.SelectNodes("LevelConfig/Level1");
            foreach (XmlNode node in nodeList)
            {
                Console.WriteLine( node );
            }
        }
    }
}