﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace Darkmoor
{
    /// <summary>
    /// Loads "bestiaries" which are special JSON formatted entries of 
    /// monsters. These JSON files are not included in this repository
    /// because I do not own the license for their distribution.
    /// </summary>
    class BestiaryJsonLoader
    {
        public List<Bestiary> BestiaryList = new List<Bestiary>();

        /// <summary>
        /// Bestiaries are spread over multiple files.
        /// </summary>
        public void LoadAllBestiaries()
        {
            var fileList = new List<string> { "bestiary-mm.json"};

            foreach (string file in fileList)
            {
                try
                {
                    LoadJsonMonsters(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("WARNING: Failed to load bestiary file " 
                        + file + ". Reason: " + ex.Message);
                }
            }
            Console.WriteLine("All bestiaries loaded.");
        }

        /// <summary>
        /// Export all loaded bestiaries as a list of Ancestry objects for use
        /// by this program.
        /// </summary>
        /// <returns></returns>
        public List<Ancestry> ExportAsAncestryList()
        {
            var ancestryList = new List<Ancestry>();
            var allowedTypeList = new List<string> 
            { 
                "aberration",
                "dragon",
                "elemental",
                "fey",
                "giant",
                "monstrosity",
                "humanoid" 
            };

            foreach (var bestiary in BestiaryList)
            {
                foreach (var monster in bestiary.monster)
                {
                    foreach (var validType in allowedTypeList)
                    {
                        if (!monster.TypeList.Contains(validType)) 
                        { 
                            continue; 
                        }
                        var ancestry = AsAncestry(monster);
                        ancestryList.Add(ancestry);
                    }

                }

            }

            return ancestryList;
        }

        /// <summary>
        /// Converts a single bestiary monster into an Ancestry object
        /// </summary>
        /// <param name="monster"></param>
        /// <returns></returns>
        public Ancestry AsAncestry(BestiaryMonster monster)
        {
            var ancestry = new Ancestry();

            // Todo: convert ancestries
            ancestry.Name = monster.name;
            ancestry.BaseAc = monster.ArmorClass;
            ancestry.BaseToHit = _calcBaseToHit(monster.action);
            ancestry.BaseNumAttacks = _calcNumAttacks(monster.action);
            ancestry.HitDice = _parseHitDice(monster.hp);
            ancestry.MoraleBonus = (int)((monster.Wis - 10) * 0.5);

            float baseAppearing = 200 / ancestry.HitDice;
            float mod = baseAppearing * 0.15f;
            float fMin = baseAppearing - mod;
            float fMax = baseAppearing + mod;
            ancestry.MinAppearing = (int)fMin;
            ancestry.MaxAppearing = (int)fMax;

            return ancestry;
        }

        /// <summary>
        /// Average hp times 2 is max hit points.
        /// Divided by 8 (or multiplied by 0.125) gets number of d8s that are
        /// rolled.
        /// Counts CON modifiers as HD, so high CON monsters appear to have
        /// more HD.
        /// </summary>
        /// <param name="hpObj"></param>
        /// <returns></returns>
        private int _parseHitDice(BestiaryMonsterHp hpObj)
        {
            int hd = (int)(((hpObj.average - 1)) * 0.125);
            if (hd < 1) { hd = 1; }
            return hd;
        }

        private int _calcNumAttacks(List<BestiaryMonsterAction> actionList)
        {
            int numAtt = 1;

            foreach (var action in actionList)
            {
                if (!action.name.Contains("Multiattack")) { continue; }
                var entryList = action.ActionEntries;
                foreach (var entry in entryList)
                {
                    int attFound = 0;
                    if (entry.Contains("two")) { attFound = 2; }
                    if (entry.Contains("three")) { attFound = 3; }
                    if (entry.Contains("four")) { attFound = 4; }
                    if (entry.Contains("five")) { attFound = 5; }
                    if (attFound > numAtt) { numAtt = attFound; }
                }
            }
            // Console.WriteLine("Num Att: " + numAtt);
            return numAtt;
        }

        private int _calcBaseToHit(List<BestiaryMonsterAction> actionList)
        {
            int toHit = 0;
            foreach (var action in actionList)
            {
                var entryList = action.ActionEntries;
                foreach (var entry in entryList)
                {
                    if (!entry.Contains("{@hit")) { continue; }
                    int attPos = entry.IndexOf("{@hit");
                    int valPos = attPos + 6;
                    string toHitStr = entry.Substring(valPos, 1);
                    bool success = Int32.TryParse(toHitStr, out int hitMod);
                    //Console.WriteLine("found " + toHitStr + " success: " + success);
                    if (!success) { continue; }
                    if (hitMod > toHit) { toHit = hitMod; }

                }

            }
            return toHit;
        }

        /// <summary>
        /// Loads a single bestiary
        /// </summary>
        /// <param name="filename"></param>
        public void LoadJsonMonsters(string filename)
        {
            // load files
            Bestiary bestiary;
            string jsonData = File.ReadAllText(filename);

            // easy items to deserialize
            bestiary = JsonConvert.DeserializeObject<Bestiary>(jsonData);

            // items that have questionable types
            JObject rootObj = JObject.Parse(jsonData);
            JArray monsterList = (JArray)rootObj["monster"];
            for (int i = 0; i < monsterList.Count; ++i)
            {
                _loadTypeData(monsterList, i, bestiary);
                _loadAcData(monsterList, i, bestiary);
                _loadActionData(monsterList, i, bestiary);
            }

            BestiaryList.Add(bestiary);

        } // loadJsonMonsters

        private void _loadTypeData(JArray monsterList, int monsterIndex, 
            Bestiary bestiary)
        {
                JToken typeToken = monsterList[monsterIndex]["type"];
                var typeList = new List<string>();
                if (typeToken.GetType() == typeof(JValue))
                {
                    typeList.Add((string)typeToken);
                }
                else if (typeToken.GetType() == typeof(JObject))
                {
                    typeList = _parseTypeObjext(typeToken, typeList);
                }
                else
                {
                    Console.WriteLine("Unrecognized data type for 'type' " +
                        "entry: " + typeToken.GetType());
                }
                bestiary.monster[monsterIndex].TypeList = typeList;
        }

        private List<string> _parseTypeObjext(JToken typeToken, 
            List<string> typeList)
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
            return typeList;
        }

        private void _loadAcData(JArray monsterList, int monsterIndex, 
            Bestiary bestiary)
        {
            JArray acJsonEntry = (JArray)monsterList[monsterIndex]["ac"];
            JToken acJsonElement = acJsonEntry[0];
            Type eleType = acJsonElement.GetType();
            if (eleType == typeof(JValue))
            {
                bestiary.monster[monsterIndex].ArmorClass = (int)acJsonElement;
            }
            else if (eleType == typeof(JObject))
            {
                bestiary.monster[monsterIndex].ArmorClass = 
                    (int)acJsonElement["ac"];
            }
            else
            {
                Console.WriteLine("Unrecognized data type for 'ac' " +
                    "entry: " + eleType);
            }
        }

        private void _loadActionData(JArray monsterList, int monsterIndex, 
            Bestiary bestiary)
        {
            JArray actionList = (JArray)monsterList[monsterIndex]["action"];
            if (!(actionList is null))
            {
                _parseActionList(actionList, bestiary, monsterIndex);
            }
        }

        private void _parseActionList(JArray actionList, Bestiary bestiary, 
            int monsterIndex)
        {
            for (int n = 0; n < actionList.Count; ++n)
            {
                var actionJObj = (JObject)actionList[n];
                var monsterActionObj = new BestiaryMonsterAction();
                monsterActionObj.name = (string)actionJObj["name"];
                var actionEntryJList = (JArray)actionJObj["entries"];
                _parseActionEntries(actionEntryJList, monsterActionObj);
                bestiary.monster[monsterIndex].action[n] = monsterActionObj;
            }
        }

        private void _parseActionEntries(JArray actionEntryJList, 
            BestiaryMonsterAction monsterActionObj)
        {
            foreach (var actionEntryJToken in actionEntryJList)
            {
                Type tokenType = actionEntryJToken.GetType();
                if (tokenType == typeof(JValue))
                {
                    monsterActionObj.ActionEntries.Add(
                        (string)actionEntryJToken);
                }
                else if (tokenType == typeof(JObject))
                {
                    var actionEntryJObj = (JObject)actionEntryJToken;
                    var actionEntryItemJList = 
                        (JArray)actionEntryJObj["items"];
                    _parseActionSubItems(actionEntryItemJList, 
                        monsterActionObj);
                }
                else
                {
                    Console.WriteLine("Unrecognized action entry type: " 
                        + tokenType);
                }
            }
        }

        private void _parseActionSubItems(JArray actionEntryItemJList, 
            BestiaryMonsterAction monsterActionObj)
        {
            foreach (var actionItemEntry in actionEntryItemJList)
            {
                var actionItemJObj = (JObject)actionItemEntry;
                monsterActionObj.ActionEntries.Add(
                    (string)actionItemJObj["entry"]);
            }
        }


    } // class BestiaryJsonLoader
} // namespace Darkmoor