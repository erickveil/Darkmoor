using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    /// <summary>
    /// A collection of Hexes
    /// </summary>
    class HexDataIndex
    {
        private readonly Dice _dice;


        public GameTime TimeObj;
        public List<HexData> HexList = new List<HexData>();

        public HexDataIndex(Dice dice)
        {
            _dice = dice;
            TimeObj = GameTime.Instance;
            TimeObj.Init(_dice);
        }

        public void GenerateWorld(int width, int height)
        {
            for (int x = 0; x < width; ++x)
            {
                for (int y = 0; y < height; ++y)
                {
                    HexList.Add(CreateRandomHex(x, y));
                }
            }
        }

        public HexData CreateRandomHex(int x, int y)
        {
            HexData hex = new HexData(_dice, this);
            hex.InitializeAsRandomHex(x, y);
            return hex;
        }

        public HexData GetHexByCoordinates(int x, int y)
        {
            foreach(var hex in HexList)
            {
                if (hex.XCoord == x && hex.YCoord == y) { return hex; }
            }
            return null;
        }

        public HexData GetHexByCoordinates(Tuple<int, int> coords)
        {
            return GetHexByCoordinates(coords.Item1, coords.Item2);
        }

        /// <summary>
        /// after battles, we remove the civilizations that have no members
        /// left.
        /// </summary>
        public void CleanOutRuins()
        {
            foreach (var hex in HexList)
            {
                foreach (var lair in hex.LairList)
                {
                    if (lair.IsRuins()) { continue; }
                    lair.AbandonIfEmpty();
                }

            }
        }

        /// <summary>
        /// Determines the number of edges of the hex that fall outside the 
        /// existing map.
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public int NumberOutsideEdges(HexData hex)
        {
            int numOuterEdges = 0;
            for (int i = 0; i < 6; ++i)
            {
                int index = i + 1;
                var neighborCoords = hex.FindNeighborByIndex(index);
                var neighbor = GetHexByCoordinates(neighborCoords.Item1, 
                    neighborCoords.Item2);
                if (neighbor == null) { ++numOuterEdges; }
            }
            return numOuterEdges;
        }

        public void ResolveAllMigrations()
        {
            var migration = new Migration(_dice, this);

            // outside invaders
            foreach (var hex in HexList)
            {
                migration.ResolveOutsideSingleHexMigration(hex);
            }

            // internal migrations
            // todo: these should be resolved via date initiative settlement
            migration.ClearMigrationSchedule();
            foreach (var hex in HexList)
            {
                migration.QueueMigrations(hex);
            }
            migration.ResolveQueuedMigrations();
            
        }

        public void IncreaseAllPopulations()
        {
            foreach (var hex in HexList)
            {
                foreach(var lair in hex.LairList)
                {
                    if (lair.IsRuins()) { continue; }
                    lair.HomeCiv.IncreasePopulation();
                }
            }
        }
    }
}
