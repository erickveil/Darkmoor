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
        public List<BestiaryMonsterAction> action = 
            new List<BestiaryMonsterAction>();
        public BestiaryMonsterHp hp;
        public int Str;
        public int Dex;
        public int Con;
        public int Int;
        public int Wis;
        public int Cha;



        private List<string> typeList = new List<string>();
        public List<string> TypeList { get => typeList; set => typeList = value; }

        private int ac;
        public int ArmorClass { get => ac; set => ac = value; }
    }
}
