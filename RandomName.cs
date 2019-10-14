using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class RandomName
    {
        public static string CreateConsonant()
        {
            var consonantList = new List<string> { 
                "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "q", 
                "qu", "r", "s", "t", "v", "w", "x", "y", "z", "ch", "sh", "th", 
                "kh", "" };
            int roll = Dice.Roll(1, consonantList.Count) - 1;
            return consonantList[roll];
        }

        public static string CreateVowel()
        {
            var vowelList = new List<string>
            {
                "a", "e", "i", "o", "u", "y", "ae", "ie", "ai", "ea", "ee", "ei", 
                "ey", "ie", "oa", "oi", "ou" };
            int roll = Dice.Roll(1, vowelList.Count) - 1;
            return vowelList[roll];
        }

        public static string CreateSyllable()
        {
            string syllable = CreateConsonant();
            syllable += CreateVowel();
            syllable += CreateConsonant();
            return syllable;
        }

        public static string CreateWord()
        {
            int syllables = Dice.Roll(1, 4);
            string word = "";
            for (int i = 0; i < syllables; ++i)
            {
                word += CreateSyllable();
            }
            return word;
        }
    }
}
