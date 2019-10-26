using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class BestiaryMonsterAction
    {
        public string name;

        private List<string> _actionEntries = new List<string>();
        public List<string> ActionEntries { get => _actionEntries; set => _actionEntries = value; }
    }
}
