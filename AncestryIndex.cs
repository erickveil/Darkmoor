using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Loads, holds, and provides anctries.
    /// </summary>
    class AncestryIndex
    {
        RandomTable<Ancestry> _ancestryTable;

        Dice _dice;

        public AncestryIndex(Dice dice)
        {
            _dice = dice;
            _ancestryTable = new RandomTable<Ancestry>(dice);
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
                _ancestryTable.AddItem(ancestryObj);
            }
        }

        public Ancestry GetRandomAncestry()
        {
            return _ancestryTable.GetResult();
        }


    }
}
