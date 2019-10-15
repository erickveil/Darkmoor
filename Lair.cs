using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// The home of a civilization.
    /// Only one Civilization can live in a lair at a time.
    /// If another one comes to live here, they must defeat and drive out the
    /// existing occupants, assimilate them, or be assimilated in order to 
    /// stay.
    /// </summary>
    class Lair
    {
        public string Name = "Unnamed";
        public string Type = "Cave";
        public Civilization HomeCiv;
        public int MileHexIndex = 0;

        private Dice _dice;

        public Lair(Dice dice)
        {
            _dice = dice;
        }

        public void setAsRandomLair()
        {
            // Name
            var nameGen = new RandomName(_dice);
            Name = nameGen.CreateWord();

            // Type
            var lairTypeList = new List<string>()
            {
                "Cave", "Dungeon", "Castle", "Keep", "Village", "Town", "City",
                "Hamlet"
            };
            int lairIndex = _dice.RandomNumber(0, lairTypeList.Count - 1);
            Type = lairTypeList[lairIndex];

            // Civ
            var civ = new Civilization(_dice);
            civ.SetAsRandomCiv();
            HomeCiv = civ;

            // Location in hex
            MileHexIndex = _dice.RandomNumber(1, 132);
        }
    }
}
