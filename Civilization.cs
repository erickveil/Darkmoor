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
        public Civilization()
        {
            _dice = Dice.Instance;
        }

        /// <summary>
        /// Generates a random population and uses that as the basis of this civ.
        /// </summary>
        public void InitializeAsRandomCiv()
        {
            var nameGen = new RandomName();
            CulturalIdentity = nameGen.CreateWord();

            var founders = new Population();
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
            History.addRecord(record, isLogged: false);
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
            History.addRecord(record, isLogged: true);
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

        public string GetPluralName()
        {
            return RandomName.Pluralize(GetFullName());
        }

        public void DissolvePopulation()
        {
            Patricians.Members = 0;
            string record = "The " + RandomName.Pluralize(GetFullName()) 
                + " have disbanded.";
            History.addRecord(record);
        }

        public void IncreasePopulation()
        {
            int percentIncrease = _dice.Roll(1, 10) + 5;
            double increaseMultiplier = percentIncrease * 0.01;
            
            int numNewPatricians = 
                (int)Math.Round(Patricians.Members * increaseMultiplier);

            Patricians.Members += numNewPatricians;
            string record = "The " + GetPluralName()
                + " census shows a population increase of " + numNewPatricians
                + " (" + percentIncrease
                + "%) among the patricians. The new population count is "
                + Patricians.Members;
            History.addRecord(record);
        }
    }
}
