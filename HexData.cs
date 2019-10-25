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
        public const int SUB_HEXES = 7;

        public string Name;
        public int XCoord = 0;
        public int YCoord = 0;
        public List<Lair> LairList = new List<Lair>();
        public HistoryLog History = new HistoryLog();

        readonly Dice _dice;
        private readonly HexDataIndex _worldMap;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="worldMap"></param>
        public HexData(HexDataIndex worldMap)
        {
            _dice = Dice.Instance;
            _worldMap = worldMap;
        }

        /// <summary>
        /// AFter loading, we need to make sure the lairs all have home hexes.
        /// </summary>
        public void InitChildren()
        {
            foreach ( var lair in LairList)
            {
                lair.HomeHex = this;
            }
        }

        /// <summary>
        /// Sets this hex up with random components
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void InitializeAsRandomHex(int x, int y)
        {
            var nameGen = new RandomName();
            Name = nameGen.CreateWord();

            XCoord = x;
            YCoord = y;

            int numLairs = _dice.Roll(1, 6) - 1;
            for (int i = 0; i < numLairs; ++i)
            {
                var lair = new Lair();
                lair.InitializeAsRandomLair(this);
                // resolve any conflicting locations
                Battle results = ResolveSettlementConflicts(lair);
                if (results is null)
                {
                    // there was no battle. Just add the new lair.
                    LairList.Add(lair);
                    continue;
                }
                if (
                    (results.AttackerState 
                    == Battle.CombatantState.COMBATANT_STATE_ELIMINATED)
                    || (results.AttackerState 
                    == Battle.CombatantState.COMBATANT_STATE_ROUTED)
                    )
                {
                    // dont even save the lair.
                    continue;
                }
                // attackers win, and are moved into existing lair.
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
                    record += lair.GetFullName();
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
        }

        /// <summary>
        /// Do a serarch for neighboring hexes.
        /// Hexes are assumed to be flat bottomed, with the origin situated
        /// in the center.
        /// </summary>
        /// <param name="index">Numbers 0-6. 0 is self, 1 is north, 2 is NE,
        /// 3 is SE, 4 is south, 5 is SW, and anything else is NW</param>
        /// <returns>The HexData coordinates of the neighboring hex</returns>
        public Tuple<int, int> FindNeighborByIndex(int index)
        {
            switch(index)
            {
                case 0: return Tuple.Create(XCoord, YCoord);
                case 1: return Tuple.Create(XCoord, YCoord + 1);
                case 2: return Tuple.Create(XCoord + 1, YCoord);
                case 3: return Tuple.Create(XCoord + 1, YCoord - 1);
                case 4: return Tuple.Create(XCoord, YCoord - 1);
                case 5: return Tuple.Create(XCoord - 1, YCoord - 1);
                default: return Tuple.Create(XCoord - 1, YCoord);
            }
        }

        /// <summary>
        /// If two civilizations spawn in the same area, they must fight to
        /// determine who may settle there.
        /// </summary>
        /// <param name="newLair">the challenger to the conflict</param>
        /// <returns>Battle object holds the state results of the conflict. If 
        /// the spot is not challenged at all, then null is returned.
        /// </returns>
        private Battle ResolveSettlementConflicts(Lair newLair)
        {
            foreach(var lair in LairList)
            {
                if (newLair.SubhexIndex != lair.SubhexIndex) { continue; }
                var battle = new Battle(_worldMap);
                battle.ResolveBattle(this, this, newLair, lair);
                return battle;
            }
            return null;
        }

        public Lair GetLairAtLocation(int subHexIndex)
        {
            foreach(var lair in LairList)
            {
                if (lair.SubhexIndex == subHexIndex) { return lair; }
            }
            return null;
        }

        public string getNameWithLoc()
        {
            return Name + "(" + XCoord + ", " + YCoord + ")";
        }

    }
}
