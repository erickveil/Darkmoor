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
    }
}
