using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// Civilizations are made up of populations of Ancestries.
    /// The populations are unified under a Name, though their Population.Rank
    /// may vary.
    /// </summary>
    class Civilization
    {
        public string Name = "Unnamed";
        public List<Population> CitizenList = new List<Population>();

        private Dice _dice;

        public Civilization(Dice dice)
        {
            _dice = dice;
        }

        public void SetAsRandomCiv()
        {
            var nameGen = new RandomName(_dice);
            Name = nameGen.CreateWord();

            var popGen = new AncestryIndex(_dice);
            popGen.LoadConstantAncestries();
            Population pop = popGen.CreateRandomPopulation();
            CitizenList.Add(pop);
        }
    }
}
