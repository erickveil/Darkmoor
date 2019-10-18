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
        private HexDataIndex _workdMap;

        public Migration(Dice dice, HexDataIndex worldMap)
        {
            _dice = dice;
            _workdMap = worldMap;
        }

        /// <summary>
        /// New monsters move in from the edge of the map.
        /// </summary>
        /// <param name="hex"></param>
        public void ResolveOutsideSingleHexMigration(HexData hex)
        {
            // only for edge chances
            int numEdges = _workdMap.NumberOutsideEdges(hex);
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
                var battle = new Battle(_dice, _workdMap);
                battle.ResolveBattle(invaders, hex, lair);
                return battle;
            }
            return null;
        }
    }
}
