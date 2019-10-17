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
        private readonly Dice _dice;

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
                    HexList.Add(CreateRandomHex(x, y));
                }
            }
        }

        public HexData CreateRandomHex(int x, int y)
        {
            HexData hex = new HexData(_dice);
            hex.InitializeAsRandomHex(x, y);
            return hex;
        }

        public HexData GetHexByCoordinates(int x, int y)
        {
            foreach(var hex in HexList)
            {
                if (hex.XCoord == x && hex.YCoord == y) { return hex; }
            }
            return null;
        }

        /// <summary>
        /// after battles, we remove the civilizations that have no members
        /// left.
        /// </summary>
        public void CleanOutRuins()
        {
            foreach (var hex in HexList)
            {
                foreach (var lair in hex.LairList)
                {
                    if (lair.IsRuins()) { continue; }
                    lair.AbandonIfEmpty();
                }

            }
        }
    }
}
