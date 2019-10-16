using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// HexData is the information about a map hex.
    /// Hexes are considered to be 12 miles wide from face to face, have 6 sides,
    /// and contain 132 one-mile hexes within them.
    /// Hexes also contain 0-5 Lairs, each located within a specific one-mile 
    /// hex.
    /// </summary>
    class HexData
    {
        public string Name;
        public int XCoord = 0;
        public int YCoord = 0;
        public List<Lair> LairList = new List<Lair>();
        public HistoryLog History = new HistoryLog();

        Dice _dice;

        public HexData(Dice dice)
        {
            _dice = dice;
        }

        public void InitializeAsRandomHex(int x, int y)
        {
            var nameGen = new RandomName(_dice);
            Name = nameGen.CreateWord();

            XCoord = x;
            YCoord = y;

            int numLairs = _dice.Roll(1, 6) - 1;
            for (int i = 0; i < numLairs; ++i)
            {
                var lair = new Lair(_dice);
                lair.InitializeAsRandomLair();
                LairList.Add(lair);
            }

            string record = Name + " (" + x + ", " + y + ") has been discovered.";
            if (LairList.Count > 0)
            {
                record += " The following lairs are found within: ";
                foreach(var lair in LairList)
                {
                    record += lair.Name + " " + lair.Type + ", ";
                }
            }
            History.addRecord(record);
        }
    }
}
