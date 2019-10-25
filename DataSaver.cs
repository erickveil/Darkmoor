using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace Darkmoor
{

    class DataSaver
    {
        public HexDataIndex ProgramData;
        public string saveFileName = @"worldData.json";
        private Dice _dice;

        public DataSaver(Dice dice)
        {

            _dice = dice;
            ProgramData = new HexDataIndex(_dice);
        }

        public void Save()
        {
            GameTime timeObj = ProgramData.TimeObj;
            string jsonData = JsonConvert.SerializeObject(ProgramData, Formatting.Indented);
            //Console.WriteLine(jsonData);
            File.WriteAllText(saveFileName, jsonData);
            Console.WriteLine("Data Saved");
        }

        public void Load()
        {
            ProgramData.ClearAllData();
            string jsonData = File.ReadAllText(saveFileName);
            ProgramData = JsonConvert.DeserializeObject<HexDataIndex>(jsonData);
            Console.WriteLine("Data Loaded");
        }
    }
}
