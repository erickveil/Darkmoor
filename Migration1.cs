using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{
    class Migration
    {
        private Dice _dice;
        private HexDataIndex _worldMap;

        public Migration(Dice dice, HexDataIndex worldMap)
        {
            _dice = dice;
            _worldMap = worldMap;
        }

        /// <summary>
        /// New monsters move in from the edge of the map.
        /// </summary>
        /// <param name="hex"></param>
        public void ResolveOutsideSingleHexMigration(HexData hex)
        {
            // only for edge chances
            int numEdges = _worldMap.NumberOutsideEdges(hex);
            if (numEdges <= 0) { return; }

            // "5% chance per edge"
            int chance = numEdges * 5;
            int roll = _dice.Roll(1, 100);
            if (roll > chance) { return; }

            // Are they invaders, or settlers?
            var invaders = new Civilization(_dice);
            invaders.InitializeAsRandomCiv();

            // Depends on where they land:
            int subHexLoc = _dice.Roll(1, HexData.SUB_HEXES);
            var results = ResolveInvasionConflicts(hex, invaders, subHexLoc);
            if (results is null)
            {
                // no battle. settle in unclaimed land.
                var lair = new Lair(_dice);
                lair.InitializeAsSettlerLair(invaders, subHexLoc);
                hex.LairList.Add(lair);
                return;
            }
            if (
                (results.AttackerState
                == Battle.CombatantState.COMBATANT_STATE_ELIMINATED)
                || (results.AttackerState
                == Battle.CombatantState.COMBATANT_STATE_ROUTED)
                )
            {
                // invaders have been dealt with.
                return;
            }
            // else the invaders win, and have been moved into their new lair.
        }

        public void ResolveInternalMigrations(HexData hex)
        {
            // isolated hexes can't migrate
            // (prevents infinite loop when searching)
            int outerEdges = _worldMap.NumberOutsideEdges(hex);
            if (outerEdges >= 6) { return; }

            foreach (var lair in hex.LairList)
            {
                // 15% chance for internal civs to migrate
                int roll = _dice.Roll(1, 100);
                if (roll > 15) { continue; }

                string record = "The " + lair.HomeCiv.GetPluralName()
                    + " are migrating to a new home.";
                lair.HomeCiv.History.addRecord(record);

                HexData targetHex = null;
                do
                {
                    int migrationDirection = _dice.Roll(1, 6);
                    var neighborLocation =
                        hex.FindNeighborByIndex(migrationDirection);
                    targetHex =
                        _worldMap.GetHexByCoordinates(neighborLocation);
                } while (targetHex is null);

                int subHexIndex = _dice.Roll(1, HexData.SUB_HEXES);
                var existingLair = targetHex.GetLairAtLocation(subHexIndex);
                if (existingLair is null)
                {
                    // Free real estate!
                    var newLair = new Lair(_dice);
                    newLair.InitializeAsSettlerLair(lair.HomeCiv, subHexIndex);
                    targetHex.LairList.Add(newLair);
                    lair.ForceAbandon();
                    return;
                }
                // Going to have to fight for the space.
                var battle = new Battle(_dice, _worldMap);
                battle.ResolveBattle(hex, targetHex, lair, existingLair);
            }
        }

        /// <summary>
        /// If there's a lair already in the area, they will fight for it.
        /// Otherwise, there's no battle to return.
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="invaders"></param>
        /// <param name="subHexIndex"></param>
        /// <returns></returns>
        private Battle ResolveInvasionConflicts(HexData hex, 
            Civilization invaders, int subHexIndex)
        {
            foreach (var lair in hex.LairList)
            {
                if (subHexIndex != lair.SubhexIndex) { continue; }
                var battle = new Battle(_dice, _worldMap);
                battle.ResolveBattle(invaders, hex, lair);
                return battle;
            }
            return null;
        }
    }
}
