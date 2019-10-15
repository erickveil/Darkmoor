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
    /// </summary>
    class Population
    {
        public Ancestry BaseAncestry;
        public int Members;
        public int AcMod = 0;
        public int ToHitMod = 0;
        public int BonusAttacks = 0;
        public int HitDiceBonus = 0;

        private Dice _dice;

        public Population(Ancestry blueprint, Dice dice)
        {
            BaseAncestry = blueprint;
            _dice = dice;
            Members = 
                dice.Roll(blueprint.MinAppearing, blueprint.MaxAppearing);
        }

    }
}
