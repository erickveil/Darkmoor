using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class RandomName
    {
        Dice _die;

        public RandomName(Dice die)
        {
            _die = die;
        }

        /// <summary>
        /// Creates some hard consonants that are easy to land on
        /// </summary>
        /// <returns></returns>
        public string CreateConsonant()
        {
            var consonantList = new List<string> { 
                "b", "c", "d", "f", "g", "j", "k", "l", "m", "n", "p", 
                "r", "s", "t", "v", "z", "ch", "sh", "th" };
            int roll = _die.Roll(1, consonantList.Count) - 1;
            return consonantList[roll];
        }

        /// <summary>
        /// Creates some air between consonants
        /// </summary>
        /// <returns></returns>
        public string CreateVowel()
        {
            var vowelList = new List<string>
            {
                "a", "e", "i", "o", "u", "ai", "ea", "ee",  
                "ie", "oa", "oi", "ou" };
            int roll = _die.Roll(1, vowelList.Count) - 1;
            return vowelList[roll];
        }

        public string WordEnd()
        {
            var vowelList = new List<string>
            {
                "a", "e", "i", "o" 
            };
            int roll = _die.Roll(1, vowelList.Count) - 1;
            return vowelList[roll];

        }

        /// <summary>
        /// Assembles out of consonant-vowel-consonent random mixes
        /// </summary>
        /// <returns></returns>
        public string CreateSyllable()
        {
            string syllable = CreateConsonant();
            syllable += CreateVowel();
            syllable += CreateConsonant();
            return syllable;
        }

        /// <summary>
        /// Assembles 1-3 syllables into a word
        /// </summary>
        /// <returns></returns>
        public string CreateWord(bool isCapitalized = true)
        {
            int syllables = _die.Roll(1, 2);
            bool isFeminine = _die.Roll(1, 6) <= 3;
            uint syllableCount = Convert.ToUInt32(syllables);
            if (isFeminine) { return CreateWord(syllableCount) + WordEnd(); }
            return CreateWord(syllableCount, isCapitalized);
        }

        /// <summary>
        /// Creates a word with as many syllables as you want.
        /// </summary>
        /// <param name="syllableCount"></param>
        /// <returns></returns>
        public string CreateWord(uint syllableCount, bool isCapitalized = true)
        {
            string word = "";
            for (uint i = 0; i < syllableCount; ++i)
            {
                word += CreateSyllable();
            }

            if (isCapitalized)
            {
                word = word.First().ToString().ToUpper()
                    + String.Join("", word.Skip(1));
            }

            return word;
        }
    }
}
