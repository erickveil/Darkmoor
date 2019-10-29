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
        public List<string> TypeList 
        { 
            get => typeList; 
            set => typeList = value; 
        }

        private int ac;
        public int ArmorClass { get => ac; set => ac = value; }

        private bool _isCopy = false;
        public bool IsCopy { get => _isCopy; set => _isCopy = value; }

        private string _challengeRating;
        public string ChallengeRating { get => _challengeRating; set => _challengeRating = value; }

        public int GetXpValue()
        {
            return GetXpValue(_challengeRating);
        }

        public static int GetXpValue(string cr)
        {
            switch(cr)
            {
                // tier 1
                case "0": return 10;
                case "1/8": return 25;
                case "1/4": return 50;
                case "1/2": return 100;
                case "1": return 200;
                case "2": return 450;
                case "3": return 700;
                case "4": return 1100;
                // tier 2
                case "5": return 1800;
                case "6": return 2300;
                case "7": return 2900;
                case "8": return 3900;
                case "9": return 5000;
                case "10": return 5900;
                // tier 3
                case "11": return 7200;
                case "12": return 8400;
                case "13": return 10000;
                case "14": return 11500;
                case "15": return 13000;
                case "16": return 15000;
                // tier 4
                case "17": return 18000;
                case "18": return 20000;
                case "19": return 22000;
                case "20": return 25000;
                // epic
                case "21": return 33000;
                case "22": return 41000;
                case "23": return 50000;
                case "24": return 62000;
                case "25": return 75000;
                case "26": return 90000;
                case "27": return 105000;
                case "28": return 120000;
                case "29": return 135000;
                case "30": return 155000;
                default: return 0;
            }
        }

        public static int GetTier(int xpValue)
        {
            if (xpValue <= 1800) { return 1; }
            if (xpValue <= 7200) { return 2; }
            if (xpValue <= 18000) { return 3; }
            return 4;
        }

    }
}
