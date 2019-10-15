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
    }
}
