using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// The blueprint for members of civilizations
    /// Values should be loaded from a file or defined with constants.
    /// Or user created via interface, perhaps.
    /// 
    /// To track actual populations of this ancestry in the world, 
    /// use a seperate class, called Population.
    /// This Ancestry class initalizes a Population.
    /// 
    /// CSV parsing: 
    /// https://stackoverflow.com/questions/2081418/parsing-csv-files-in-c-with-header
    /// </summary>
    class Ancestry
    {
        public string Name;
        public int MinAppearing;
        public int MaxAppearing;
        public int BaseAc;
        public int BaseToHit;
        public int BaseNumAttacks;
        public int HitDice;
        public int MoraleBonus;
    }
}
