using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// A collection of Hexes
    /// </summary>
    class HexDataIndex
    {
        private Dice _dice;

        public List<HexData> HexList = new List<HexData>();

        public HexDataIndex(Dice dice)
        {
            _dice = dice;
        }

        public void GenerateWorld(int width, int height)
        {
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    HexList.Add(createRandomHex(x, y));
                }
            }
        }

        public HexData createRandomHex(int x, int y)
        {
            HexData hex = new HexData();

            var nameGen = new RandomName(_dice);
            hex.Name = nameGen.CreateWord();

            hex.XCoord = x;
            hex.YCoord = y;

            string logStr = hex.Name + " created at location " + x + ", " + y;
            Console.WriteLine(logStr);

            int numLairs = _dice.Roll(1, 6) - 1;
            for (int i = 0; i < numLairs; ++i)
            {
                var lair = new Lair(_dice);
                lair.setAsRandomLair();
                hex.LairList.Add(lair);

                logStr = hex.Name + " (" + hex.XCoord + ", "
                    + hex.YCoord + ") has a new Lair: " + lair.Name + " "
                    + lair.Type + ", occupied by the " + lair.HomeCiv.Name 
                    + " " + lair.HomeCiv.CitizenList[0].BaseAncestry.Name 
                    + "s in hex " + lair.MileHexIndex;
                Console.WriteLine(logStr);
            }

            return hex;
        }
    }
}
