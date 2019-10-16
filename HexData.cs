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
        public HistoryLog History = new HistoryLog();

        Dice _dice;

        public HexData(Dice dice)
        {
            _dice = dice;
        }

        public void InitializeAsRandomHex(int x, int y)
        {
            var nameGen = new RandomName(_dice);
            Name = nameGen.CreateWord();

            XCoord = x;
            YCoord = y;

            int numLairs = _dice.Roll(1, 6) - 1;
            for (int i = 0; i < numLairs; ++i)
            {
                var lair = new Lair(_dice);
                lair.InitializeAsRandomLair();
                LairList.Add(lair);
            }

            string record = Name + " (" + x + ", " + y 
                + ") has been discovered.";
            if (LairList.Count > 0)
            {
                record += " The following lairs are found within: ";
                for (int i = 0; i < LairList.Count; ++i)
                {
                    var lair = LairList[i];
                    int lastIndex = LairList.Count - 1;
                    int secondToLastIndex = lastIndex - 1;
                    record += lair.Name + " " + lair.Type;
                    if (i == secondToLastIndex)
                    {
                        record += ", and ";
                        continue;
                    }
                    if (i == lastIndex) { continue; }
                    record += ", ";
                }
            }
            History.addRecord(record);

            JoinLikeForces();
        }

        private void JoinLikeForces()
        {
            foreach(var lair in LairList)
            {
                Civilization lairRulers = lair.HomeCiv;
                foreach(var otherLair in LairList)
                {
                    if (otherLair.Name == lair.Name) { continue; }
                    bool isSameAncestry =
                        otherLair.HomeCiv.CitizenList[0].BaseAncestry.Name ==
                        lair.HomeCiv.CitizenList[0].BaseAncestry.Name;
                    if (!isSameAncestry) { continue; }
                    bool isSameCiv =
                        otherLair.HomeCiv.Name ==
                        lair.HomeCiv.Name;
                    if (isSameCiv) { continue; }
                    otherLair.HomeCiv.JoinOtherCivilization(lair.HomeCiv.Name);
                    lair.HomeCiv.JoinOurCivilization(otherLair.HomeCiv.Name);
                }
            }
        }
    }
}
