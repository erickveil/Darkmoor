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

        public DataSaver()
        {
            ProgramData = new HexDataIndex();
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
            ProgramData.InitChildren();
            Console.WriteLine("Data Loaded");
        }
    }
}
