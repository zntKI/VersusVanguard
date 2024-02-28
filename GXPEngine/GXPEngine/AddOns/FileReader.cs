using System;
using GXPEngine;

/*
    - Make a hashmap list for the menu tiles
    - Get the level name and use it for the menu tile parameter
    - Assign the rest of the hashmap to the menu tile
*/

namespace GXPEngine
{
    class FileReader
    {

        String filename;
        List<String> modes = new List<string> { "json", "xml", "txt" };
        String mode;
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement rootNode;
        List<Dictionary<string,string>> menuTiles = new List<Dictionary<string,string>>();
        

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

        public List<Dictionary<string,string>> GenerateMenuTiles()
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
                Dictionary<string,string> menuTile = new Dictionary<string,string>();
                menuTile.Add("levelName", node.Name);

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    menuTile.Add(childNode.Name, childNode.InnerText);
                }
                menuTiles.Add(menuTile);
            }
        }
    }
}

