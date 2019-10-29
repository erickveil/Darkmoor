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
        RandomTable<Ancestry> _tier1;
        RandomTable<Ancestry> _tier2;
        RandomTable<Ancestry> _tier3;
        RandomTable<Ancestry> _tier4;

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
            _tier1 = new RandomTable<Ancestry>();
            _tier2 = new RandomTable<Ancestry>();
            _tier3 = new RandomTable<Ancestry>();
            _tier4 = new RandomTable<Ancestry>();
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
            foreach (var ancestry in ancestryList)
            {
                _ancestryTable.AddItem(ancestry);
            }
            var ancestryListT1 = loader.ExportAncestryListForTier(1);
            foreach (var ancestry in ancestryListT1)
            {
                _tier1.AddItem(ancestry);
            }
            var ancestryListT2 = loader.ExportAncestryListForTier(2);
            foreach (var ancestry in ancestryListT2)
            {
                _tier2.AddItem(ancestry);
            }
            var ancestryListT3 = loader.ExportAncestryListForTier(3);
            foreach (var ancestry in ancestryListT3)
            {
                _tier3.AddItem(ancestry);
            }
            var ancestryListT4 = loader.ExportAncestryListForTier(4);
            foreach (var ancestry in ancestryListT4)
            {
                _tier4.AddItem(ancestry);
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
        /// Gets a random ancestry from the table of all ancestries.
        /// </summary>
        /// <returns></returns>
        public Ancestry GetRandomAncestry()
        {
            return _ancestryTable.GetResult();
        }

        /// <summary>
        /// Gets a random ancestry whos CR falls withing the tier for a party
        /// of 4.
        /// </summary>
        /// <param name="tier"></param>
        /// <returns></returns>
        public Ancestry GetRandomAncestry(int tier)
        {
            if (tier == 1) { return _tier1.GetResult(); }
            if (tier == 2) { return _tier2.GetResult(); }
            if (tier == 3) { return _tier3.GetResult(); }
            return _tier4.GetResult();
        }


    }
}
