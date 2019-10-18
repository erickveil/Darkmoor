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
            HexData hex = new HexData(_dice, this);
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

        /// <summary>
        /// Determines the number of edges of the hex that fall outside the 
        /// existing map.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public int NumberOutsideEdges(HexData hex)
        {
            int numOuterEdges = 0;
            for (int i = 0; i < 6; ++i)
            {
                int index = i + 1;
                var neighborCoords = hex.FindNeighborByIndex(index);
                var neighbor = GetHexByCoordinates(neighborCoords.Item1, 
                    neighborCoords.Item2);
                if (neighbor == null) { ++numOuterEdges; }
            }
            return numOuterEdges;
        }
    }
}
