using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Civilizations are made up of populations of Ancestries.
    /// The populations are unified under a Name, though their Population.Rank
    /// may vary.
    /// Each Lair is occupied by a single civlilization.
    /// A civilization may occupy several Lairs.
    /// </summary>
    class Civilization
    {
        public string Name = "Unnamed";
        public List<Population> CitizenList = new List<Population>();
        public HistoryLog History = new HistoryLog();

        private Dice _dice;

        public Civilization(Dice dice)
        {
            _dice = dice;
        }

        public void InitializeAsRandomCiv()
        {
            var nameGen = new RandomName(_dice);
            Name = nameGen.CreateWord();

            var founders = new Population(_dice);
            founders.InitializeAsRandomPop();
            CitizenList.Add(founders);

            string record = "The " + Name + " " + founders.BaseAncestry.Name
                + " Civilization has begun, with a starting population of "
                + founders.Members;
            History.addRecord(record);
        }

        public void JoinOtherCivilization(string otherCivName)
        {
            string record = Name 
                + " " + CitizenList[0].BaseAncestry.Name
                + "s have been assimilated into the " + otherCivName 
                + " Civilization.";
            Name = otherCivName;
            History.addRecord(record);
        }
        
        public void JoinOurCivilization(string otherCivName)
        {
            string record = Name 
                + " " + CitizenList[0].BaseAncestry.Name
                + "s have assimilated the " + otherCivName 
                + " Civilization.";
            Name = otherCivName;
            History.addRecord(record, false);
        }
    }
}
