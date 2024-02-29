using System;
using System.Collections.Generic;
using System.Xml;
using GXPEngine;
using GXPEngine.Core;

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
        string[] scores;
        

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

        private void WriteFile()
        {
            // if (mode == "json") WriteJson();
            // if (mode == "xml")  WriteXml();
            if (mode == "txt")  WriteTxt();
        }

        private void ReadTxt() 
        {
            scores = System.IO.File.ReadAllLines(filename);
        }

        private void WriteTxt()
        {
            Console.WriteLine("Writing txt file: " + filename);
            System.IO.File.WriteAllLines(filename, scores);
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
        
        public List<Dictionary<string,string>> GenerateMenuTiles()
        {
            ReadFile();
            return menuTiles;
        }

        public string[] UpdateScores()
        {
            ReadFile();
            return scores;
        }

        public void UpdateScores( int newScore )
        {
            ReadFile();
            int scoreIndex = 0;
            foreach (string score in scores)
            {
                if (newScore > int.Parse(score))
                {
                    scores[scoreIndex] = newScore.ToString();
                    break;
                }
                scoreIndex++;
            }
            WriteFile();
        }
    }
}

