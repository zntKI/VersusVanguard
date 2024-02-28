using System;
using System.Collections.Generic;
using System.Xml;
using GXPEngine;

namespace GXPEngine
{
    class FileReader
    {

        String filename;
        List<String> modes = new List<string> { "json", "xml", "txt" };
        String mode;
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement rootNode;
        List<MenuTile> menuTiles = new List<MenuTile>();
        

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
        
        private void ReadFile()
        {
            if (mode == "json") ReadJson();
            if (mode == "xml")  ReadXml();
            if (mode == "txt")  ReadTxt();
        }

        public List<MenuTile> GenerateMenuTiles()
        {
            ReadFile();
            return menuTiles;
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
            rootNode = xmlDoc.DocumentElement;
            foreach (XmlNode node in rootNode.ChildNodes)
            {
                menuTiles.Add(new MenuTile(node.Name));
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    Console.WriteLine(childNode.InnerText);
                }
            }
        }
    }
}