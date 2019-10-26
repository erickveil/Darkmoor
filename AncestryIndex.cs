using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Darkmoor
{
    /// <summary>
    /// Loads, holds, and provides anctries.
    /// </summary>
    class AncestryIndex
    {
        RandomTable<Ancestry> _ancestryTable;

        Dice _dice;

        public AncestryIndex()
        {
            _dice = Dice.Instance;
            _ancestryTable = new RandomTable<Ancestry>();
        }

        public void LoadConstantAncestries()
        {
            var nameList = new List<string> { 
                "human", "elf", "dwarf", "hobgoblin", "dark elf", "orc" 
            };

            foreach(var name in nameList)
            {
                var ancestryObj = new Ancestry();
                ancestryObj.Name = name;
                ancestryObj.MinAppearing = 180;
                ancestryObj.MaxAppearing = 220;
                ancestryObj.BaseAc = 10;
                ancestryObj.BaseToHit = 0;
                ancestryObj.BaseNumAttacks = 1;
                ancestryObj.HitDice = 1;
                ancestryObj.MoraleBonus = 0;
                _ancestryTable.AddItem(ancestryObj);
            }
        }

        public void LoadAllSources()
        {
            LoadCsvAncestries();
            LoadJsonAncestries();
        }

        public void LoadCsvAncestries()
        {
            try
            {
                string fileContents = File.ReadAllText(@"ancestries.csv");
                string[] recordList = fileContents.Split('\n');
                bool isFirst = true;
                foreach (var record in recordList)
                {
                    // Skip header line
                    if (isFirst)
                    {
                        isFirst = false;
                        continue;
                    }
                    // Skip empty lines
                    if (record == "") { continue; }

                    string[] splitRecord = record.Split(',');
                    var entryList = new List<string>(splitRecord);
                    var ancestry = new Ancestry();

                    _fillStringEntry(entryList, 0, out ancestry.Name);

                    if (entryList.Count() <= 7)
                    {
                        Console.WriteLine("WARN: "
                            + ancestry.Name + " entry only has " 
                            + entryList.Count() + " members: " + record);
                    }

                    _fillIntEntry(entryList, 1, out ancestry.MinAppearing, ancestry.Name + " MinAppearing");
                    _fillIntEntry(entryList, 2, out ancestry.MaxAppearing, ancestry.Name + " MaxAppearing");
                    _fillIntEntry(entryList, 3, out ancestry.BaseAc, ancestry.Name + " BaseAc");
                    _fillIntEntry(entryList, 4, out ancestry.BaseToHit, ancestry.Name + " BaseToHit");
                    _fillIntEntry(entryList, 5, out ancestry.BaseNumAttacks, ancestry.Name + " NumAttacks");
                    _fillIntEntry(entryList, 6, out ancestry.HitDice, ancestry.Name + " HitDice");
                    _fillIntEntry(entryList, 7, out ancestry.MoraleBonus, ancestry.Name + " MoraleBonus");

                    _ancestryTable.AddItem(ancestry);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("\n-----\nERROR: Failed to load Ancestry file: " 
                    + ex.Message + "\n-----\n");
            }

        }

        public void LoadJsonAncestries()
        {
            // load files
            Bestiary bestiary_mm;
            string mmFilename = @"bestiary-mm.json";
            string jsonData = File.ReadAllText(mmFilename);

            // easy items to deserialize
            bestiary_mm = JsonConvert.DeserializeObject<Bestiary>(jsonData);

            // items that have questionable types
            JObject rootObj = JObject.Parse(jsonData);
            JArray monsterList = (JArray)rootObj["monster"];
            for (int i = 0; i < monsterList.Count; ++i)
            {
                // ======================
                // Type
                JToken typeToken = monsterList[i]["type"];
                var typeList = new List<string>();
                if (typeToken.GetType() == typeof(JValue))
                {
                    typeList.Add((string)typeToken);
                }
                else if (typeToken.GetType() == typeof(JObject))
                {
                    // Add the type string
                    var typeObj = (JObject)typeToken;
                    typeList.Add((string)typeObj["type"]);

                    // Add the tags as type strings
                    var tagsJsonList = (JArray)typeObj["tags"];

                    // not all type objects have a tags list
                    var tagList = tagsJsonList?.ToObject<List<string>>();
                    if (!(tagList is null))
                    {
                        typeList = typeList.Concat(tagList).ToList();
                    }

                    // There are other possible entries in the object 
                    // (such as "swarmSize") but we don't care.
                }
                else
                {
                    Console.WriteLine("Unrecognized data type for 'type' " +
                        "entry: " + typeToken.GetType());
                }
                bestiary_mm.monster[i].TypeList = typeList;

                // =========================
                // ac
                JArray acJsonEntry = (JArray)monsterList[i]["ac"];
                JToken acJsonElement = acJsonEntry[0];
                Type eleType = acJsonElement.GetType();
                if (eleType == typeof(JValue))
                {
                    bestiary_mm.monster[i].ArmorClass = (int)acJsonElement;
                }
                else if (eleType == typeof(JObject))
                {
                    bestiary_mm.monster[i].ArmorClass = (int)acJsonElement["ac"];
                }
                else
                {
                    Console.WriteLine("Unrecognized data type for 'ac' " +
                        "entry: " + eleType);
                }
            }

            Console.WriteLine("Bestiary Loaded");

            // Convert bestiary into Ancestry list items
        }

        

        private void _fillStringEntry(List<string> entryList, int index, 
            out string target)
        {
            target = "";
            if (entryList.Count() <= index) { return; }
            target = entryList[index];
        }

        private void _fillIntEntry(List<string> entryList, int index,
            out int target, string targetId = "")
        {
            target = 0;
            if (entryList.Count() <= index) { return; }
            bool ok = Int32.TryParse(entryList[index], out target);
            if (!ok) 
            {
                Console.WriteLine("WARN: " + targetId + " (val="
                    + entryList[index] + ") could not be converted to int.");
            }
        }

        public Ancestry GetRandomAncestry()
        {
            return _ancestryTable.GetResult();
        }


    }
}
