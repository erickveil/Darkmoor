using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class BestiaryMonster
    {
        public string name;
        public string size;


        private List<string> typeList = new List<string>();
        public List<string> TypeList { get => typeList; set => typeList = value; }

        private int ac;
        public int ArmorClass { get => ac; set => ac = value; }
    }
}
