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
        bool _isLoaded = false;

        private static AncestryIndex _instance = null;

        /// <summary>
        /// Index of all possible ancestries.
        /// This reference is a singleton, because we only need one instance
        /// and once loaded, it never changes.
        /// 
        /// It provides the blueprints for all populations.
        /// </summary>
        private AncestryIndex()
        {
            _dice = Dice.Instance;
            _ancestryTable = new RandomTable<Ancestry>();
        }

        /// <summary>
        /// Get an instance of this singelton.
        /// </summary>
        public static AncestryIndex Instance
        {
            get
            {
                if (_instance is null) { _instance = new AncestryIndex(); }
                return _instance;
            }
        }

        /// <summary>
        /// Old method used in the initial testing. Now ancestries are loaded
        /// from files so you can control what is included in your world.
        /// </summary>
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

        /// <summary>
        /// Loads all ancestries from various files.
        /// </summary>
        public void LoadAllSources()
        {
            if (_isLoaded) { return; }
            _isLoaded = true;

            LoadCsvAncestries();
            LoadJsonAncestries();
        }

        /// <summary>
        /// CSV ancestries are easy to create by users who want to add their 
        /// own or control available ancestries via whitelist.
        /// </summary>
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

                    _fillIntEntry(entryList, 1, out ancestry.MinAppearing, 
                        ancestry.Name + " MinAppearing");
                    _fillIntEntry(entryList, 2, out ancestry.MaxAppearing, 
                        ancestry.Name + " MaxAppearing");
                    _fillIntEntry(entryList, 3, out ancestry.BaseAc, 
                        ancestry.Name + " BaseAc");
                    _fillIntEntry(entryList, 4, out ancestry.BaseToHit, 
                        ancestry.Name + " BaseToHit");
                    _fillIntEntry(entryList, 5, out ancestry.BaseNumAttacks, 
                        ancestry.Name + " NumAttacks");
                    _fillIntEntry(entryList, 6, out ancestry.HitDice, 
                        ancestry.Name + " HitDice");
                    _fillIntEntry(entryList, 7, out ancestry.MoraleBonus, 
                        ancestry.Name + " MoraleBonus");

                    _ancestryTable.AddItem(ancestry);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Skipping ancestries.csv");
            }

        }

        /// <summary>
        /// A specially formatted JSON file of D&D creatures can be provided.
        /// These JSON files are not included with this repo, as they contain
        /// monsters that I don't have the license for.
        /// </summary>
        public void LoadJsonAncestries()
        {
            var loader = new BestiaryJsonLoader();
            loader.LoadAllBestiaries();
            List<Ancestry> ancestryList = loader.ExportAsAncestryList();
            // TODO: add ancestryList to ancestries
            foreach (var ancestry in ancestryList)
            {
                _ancestryTable.AddItem(ancestry);
            }
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

        /// <summary>
        /// Gets a random ancestry from the table.
        /// </summary>
        /// <returns></returns>
        public Ancestry GetRandomAncestry()
        {
            return _ancestryTable.GetResult();
        }


    }
}
