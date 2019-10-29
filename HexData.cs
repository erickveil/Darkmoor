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
        public const int SUB_HEXES = 9;

        public string Name;
        public int XCoord = 0;
        public int YCoord = 0;
        public List<Lair> LairList = new List<Lair>();
        public HistoryLog History = new HistoryLog();

        public List<string> WanderingMonsterPool = new List<string>();
        public List<string> NaturalEncounterPool = new List<string>();
        public List<string> DungeonEcologyPool = new List<string>();

        public int Tier;

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
        public void InitializeAsRandomHex(int x, int y, Tuple<int, int> origin,
            int tierWidth)
        {
            var nameGen = new RandomName();
            Name = nameGen.CreateWord();

            XCoord = x;
            YCoord = y;

            _setTier(origin.Item1, origin.Item2, tierWidth);

            var creatureSource = AncestryIndex.Instance;
            creatureSource.LoadAllSources();
            // init nature
            int numNatural = _dice.Roll(2, 4) + 1;
            for (int i = 0; i < numNatural; ++i)
            {
                var naturalCreature = 
                    creatureSource.GetRandomNaturalAncestry(Tier);
                var tier = Tier;
                while (naturalCreature is null && tier > 0)
                {
                    --tier;
                    naturalCreature = creatureSource.GetRandomAncestry(tier);
                }
                if (naturalCreature is null) { break; }
                NaturalEncounterPool.Add(naturalCreature.Name);
            }

            // init dungeons
            int numMonsters = _dice.Roll(2, 4) + 1;
            for (int i = 0; i < numMonsters; ++i)
            {
                var dungeonLurkers =
                    creatureSource.GetRandomDungeonAncestry(Tier);
                var tier = Tier;
                while (dungeonLurkers is null && tier > 0)
                {
                    --tier;
                    dungeonLurkers = 
                        creatureSource.GetRandomDungeonAncestry(tier);
                }
                if (dungeonLurkers is null) { break; }
                DungeonEcologyPool.Add(dungeonLurkers.Name);
            }

            // init lairs
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
        /// Neet to set XCoord and YCoord before calling
        /// </summary>
        /// <param name="originX"></param>
        /// <param name="originY"></param>
        /// <param name="tierWidth"></param>
        private void _setTier(int originX, int originY, int tierWidth)
        {
            // These are the distances from the origin that the tier areas end
            int t1LimitX = originX + tierWidth;
            int t1LimitY = originY + tierWidth;
            int t2LimitX = t1LimitX + tierWidth;
            int t2LimitY = t1LimitY + tierWidth;
            int t3LimitX = t2LimitX + tierWidth;
            int t3LimitY = t2LimitY + tierWidth;

            // calculating tier ranges in square areas because I'm too laxy
            // to look up the distance formula again.
            int distFromOX = Math.Abs(XCoord - originX);
            int distFromOY = Math.Abs(YCoord - originY);

            // Only correct if we exclude the previous tiers by using else if
            bool isT1 = (distFromOX <= t1LimitX) && (distFromOY <= t1LimitY);
            bool isT2 = (distFromOX <= t2LimitX) && (distFromOY <= t2LimitY);
            bool isT3 = (distFromOX <= t3LimitX) && (distFromOY <= t3LimitY);

            if (isT1) { Tier = 1; }
            else if (isT2) { Tier = 2; }
            else if (isT3) { Tier = 3; }
            else { Tier = 4; }
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
