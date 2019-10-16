using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Populations are derived from Ancestry objexts.
    /// A Population is informative about an Ancestry. 
    /// The Ancestry is the blueprint of a Population.
    /// Civilizatons are made up of populations.
    /// </summary>
    class Population
    {
        public Ancestry BaseAncestry;
        public int Members;
        public int AcMod = 0;
        public int ToHitMod = 0;
        public int BonusAttacks = 0;
        public int HitDiceBonus = 0;
        public string Rank = "Patrician";
        public HistoryLog History = new HistoryLog();

        private Dice _dice;

        public Population(Dice dice)
        {
            _dice = dice;
        }

        public void InitializeAsRandomPop()
        {
            var ancestryGen = new AncestryIndex(_dice);
            ancestryGen.LoadConstantAncestries();
            BaseAncestry = ancestryGen.GetRandomAncestry();
            Members = _dice.RandomNumber(BaseAncestry.MinAppearing, 
                BaseAncestry.MaxAppearing);
            string record = Members + " " + BaseAncestry.Name 
                + "s have entered the world.";
            History.addRecord(record);
        }

    }
}
