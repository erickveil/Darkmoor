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
        public string CulturalIdentity = "Unnamed";
        public Population Patricians;
        public HistoryLog History = new HistoryLog();
        public int LeaderCompetency = 0;

        private readonly Dice _dice;

        /// <summary>
        /// Constructor sets the dice
        /// </summary>
        /// <param name="dice"></param>
        public Civilization(Dice dice)
        {
            _dice = dice;
        }

        /// <summary>
        /// Generates a random population and uses that as the basis of this civ.
        /// </summary>
        public void InitializeAsRandomCiv()
        {
            var nameGen = new RandomName(_dice);
            CulturalIdentity = nameGen.CreateWord();

            var founders = new Population(_dice);
            founders.InitializeAsRandomPop();
            Patricians = founders;

            LeaderCompetency = (_dice.Roll(2, 6) / 2);

            string record = "The " + GetFullName() 
                + " Civilization has emerged, with a starting population of "
                + founders.Members;
            History.addRecord(record);
        }

        /// <summary>
        /// Merge this civ into another one. 
        /// This civ will lose its identity and take on the identity of the 
        /// civ they join.
        /// </summary>
        /// <param name="otherCivName"></param>
        public void JoinOtherCivilization(string otherCivName)
        {
            string record = RandomName.Pluralize(GetFullName())
                + " have been assimilated into the " + otherCivName 
                + " Civilization.";
            CulturalIdentity = otherCivName;
            History.addRecord(record);
        }
        
        /// <summary>
        /// Another civilization joins this one.
        /// Mostly just records the merger with this civ's history.
        /// </summary>
        /// <param name="otherCivName"></param>
        public void JoinOurCivilization(string otherCivName)
        {
            string record = RandomName.Pluralize(GetFullName())
                + " have assimilated the " + otherCivName 
                + " Civilization.";
            History.addRecord(record, false);
        }

        /// <summary>
        /// The full name of the civ is made up of the civ's identity
        /// and the name of its ancestry.
        /// </summary>
        /// <returns></returns>
        public string GetFullName()
        {
            return CulturalIdentity + " " + Patricians.BaseAncestry.Name;
        }

    }
}
