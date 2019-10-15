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
        string Name = "Unnamed";
        string Type = "Cave";
        Civilization HomeCiv;
    }
}
