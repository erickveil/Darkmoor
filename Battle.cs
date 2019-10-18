using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darkmoor
{

    class Battle
    {

        public List<Civilization> AttackerList = new List<Civilization>();
        public List<Civilization> DefenderList = new List<Civilization>();

        public enum CombatantState { 
            COMBATANT_STATE_RALLIED, COMBATANT_STATE_ROUTED, 
            COMBATANT_STATE_ELIMINATED
        };

        public CombatantState AttackerState = 
            CombatantState.COMBATANT_STATE_RALLIED;
        public CombatantState DefenderState = 
            CombatantState.COMBATANT_STATE_RALLIED;

        public int NumRounds;

        private int _attackerStartingForces;
        private int _defenderStartingForces;
        private int _attackerScore;
        private int _defenderScore;

        private readonly HexDataIndex _worldMap;

        readonly Dice _dice;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dice"></param>
        /// <param name="worldMap"></param>
        public Battle(Dice dice, HexDataIndex worldMap)
        {
            _dice = dice;
            _worldMap = worldMap;
        }

        /// <summary>
        /// The main battle resolution
        /// Combatants must come from a lair in a hex.
        /// </summary>
        /// <param name="attackerLands"></param>
        /// <param name="defenderLands"></param>
        /// <param name="attackerBase"></param>
        /// <param name="defenderBase"></param>
        public void ResolveBattle(HexData attackerLands, HexData defenderLands, 
            Lair attackerBase, Lair defenderBase)
        {
            if (attackerBase.IsRuins())
            {
                // no attackers! do not pass!
                AttackerState = CombatantState.COMBATANT_STATE_ELIMINATED;
                return;
            }
            Civilization attacker = attackerBase.HomeCiv;

            if (defenderBase.IsRuins())
            {
                defenderBase.MoveCivIn(attacker);
                attackerBase.ForceAbandon();
                DefenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
                return;
            }
            Civilization defender = defenderBase.HomeCiv;

            string record = "The " 
                + attacker.GetPluralName() + " are attacking the "
                + defender.GetPluralName() + " at " 
                + defenderBase.GetFullName() + "!";
            attacker.History.addRecord(record);
            defender.History.addRecord(record, isLogged: false);
            defenderBase.History.addRecord(record, isLogged: false);

            GatherAttackers(attacker, attackerLands);
            GatherDefenders(defender, defenderLands);
            _attackerStartingForces = GetTotalCombatants(AttackerList);
            _defenderStartingForces = GetTotalCombatants(DefenderList);

            _executeMainBattleLoop();

            // recover from battle
            ResolveSurvivors(_attackerStartingForces, AttackerList);
            ResolveSurvivors(_defenderStartingForces, DefenderList);

            // determine outcome of battle.
            if (AttackerState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // defender loses!
                record = "The " 
                    + defender.GetPluralName() + " have been defeated by the "
                    + attacker.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";

                string defenderBaseName = defenderBase.Name;
                MoveLosers(defender, defenderLands, defenderBaseName);
                defenderBase.MoveCivIn(attacker);
                attackerBase.ForceAbandon();
            }
            else if (DefenderState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // attacker loses!
                record = "The " 
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";

                string attackerBaseName = attackerBase.Name;
                // It's interesting that attackers don't go back home.
                MoveLosers(attacker, attackerLands, attackerBaseName);
                attackerBase.ForceAbandon();
            }
            else if (AttackerState == CombatantState.COMBATANT_STATE_ROUTED)
            {
                if (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED)
                {
                    // defenders lose
                    record = "The " 
                        + defender.GetPluralName() 
                        + " have been defeated by the "
                        + attacker.GetPluralName() + " at "
                        + defenderBase.GetFullName() + "!";
                    string defenderBaseName = defenderBase.Name;
                    MoveLosers(defender, defenderLands, defenderBaseName);
                    defenderBase.MoveCivIn(attacker);
                    attackerBase.ForceAbandon();
                }
                // attackers lose
                record = "The " 
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";
                string attackerBaseName = attackerBase.Name;
                // It's interesting that attackers don't go back home.
                MoveLosers(attacker, attackerLands, attackerBaseName);
                attackerBase.ForceAbandon();
            }
            else if (DefenderState == CombatantState.COMBATANT_STATE_ROUTED)
            {
                // attackers are not rallied, so they lose by default.
                record = "The " 
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";
                string attackerBaseName = attackerBase.Name;
                // It's interesting that attackers don't go back home.
                MoveLosers(attacker, attackerLands, attackerBaseName);
                attackerBase.ForceAbandon();
            }
            else
            {
                // mutual destruction is highly unlikely, but not impossible.
                record = "The "
                    + attacker.GetPluralName() + " and the "
                    + defender.GetPluralName()
                    + " have achieved mutual destruction at "
                    + defenderBase.GetFullName() + "!";

                attacker.DissolvePopulation();
                defender.DissolvePopulation();
            }

            _recordBattleReport(record, attacker, defender, defenderBase);

            // any lingering civs with zero population are removed.
            _worldMap.CleanOutRuins();
        }

        /// <summary>
        /// Overloaded function for determining battles started by outside
        /// or homeless invaders.
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="defenderLands"></param>
        /// <param name="defenderBase"></param>
        public void ResolveBattle(Civilization attacker, HexData defenderLands, 
            Lair defenderBase)
        {
            if (defenderBase.IsRuins())
            {
                defenderBase.MoveCivIn(attacker);
                DefenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
                return;
            }
            Civilization defender = defenderBase.HomeCiv;

            string record = "The " 
                + attacker.GetPluralName() 
                + " are attacking the "
                + defender.GetPluralName() + " at " 
                + defenderBase.GetFullName()
                + "!";
            attacker.History.addRecord(record);
            defender.History.addRecord(record, isLogged: false);
            defenderBase.History.addRecord(record, isLogged: false);

            AttackerList.Add(attacker);
            GatherDefenders(defender, defenderLands);
            _attackerStartingForces = GetTotalCombatants(AttackerList);
            _defenderStartingForces = GetTotalCombatants(DefenderList);

            _executeMainBattleLoop();

            // recover from battle
            ResolveSurvivors(_attackerStartingForces, AttackerList);
            ResolveSurvivors(_defenderStartingForces, DefenderList);

            // determine outcome of battle.
            if (AttackerState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // defender loses!
                record = "The "
                    + defender.GetPluralName() + " have been defeated by the "
                    + attacker.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";

                string defenderBaseName = defenderBase.Name;
                MoveLosers(defender, defenderLands, defenderBaseName);
                defenderBase.MoveCivIn(attacker);
            }
            else if (DefenderState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // attacker loses!
                record = "The "
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";

                // In this case, the attackers seek to allign themselves 
                // possibly where they landed.
                MoveLosers(attacker, defenderLands, baseName: "");
            }
            else if (AttackerState == CombatantState.COMBATANT_STATE_ROUTED)
            {
                if (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED)
                {
                    // defenders lose
                    record = "The " 
                        + defender.GetPluralName() 
                        + " have been defeated by the "
                        + attacker.GetPluralName() + " at "
                        + defenderBase.GetFullName() + "!";
                    string defenderBaseName = defenderBase.Name;
                    MoveLosers(defender, defenderLands, defenderBaseName);
                    defenderBase.MoveCivIn(attacker);
                }
                // attackers lose
                record = "The " 
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";
                MoveLosers(attacker, defenderLands, baseName: "");
            }
            else if (DefenderState == CombatantState.COMBATANT_STATE_ROUTED)
            {
                // attackers are not rallied, so they lose by default.
                record = "The " 
                    + attacker.GetPluralName() + " have been repelled by the "
                    + defender.GetPluralName() + " at "
                    + defenderBase.GetFullName() + "!";
                MoveLosers(attacker, defenderLands, baseName: "");
            }
            else
            {
                // mutual destruction is highly unlikely, but not impossible.
                record = "The "
                    + attacker.GetPluralName() + " and the "
                    + defender.GetPluralName()
                    + " have achieved mutual destruction at "
                    + defenderBase.GetFullName() + "!";

                attacker.DissolvePopulation();
                defender.DissolvePopulation();
            }

            _recordBattleReport(record, attacker, defender, defenderBase);

            // any lingering civs with zero population are removed.
            _worldMap.CleanOutRuins();
        }

        private void _recordBattleReport(string record, Civilization attacker, 
            Civilization defender, Lair defenderBase)
        {
            record += " Battle lasted " + NumRounds + " rounds. " 
                + attacker.GetFullName() + " survivors: " 
                + (_attackerStartingForces - _defenderScore) + ". "
                + defender.GetFullName() + " survivors: "
                + (_defenderStartingForces - _attackerScore) 
                + ". Final tally: "
                + attacker.GetFullName() + ": " + attacker.Patricians.Members
                + " vs " 
                + defender.GetFullName() + ": " + defender.Patricians.Members;
            
            attacker.History.addRecord(record);
            defender.History.addRecord(record, isLogged: false);
            defenderBase.History.addRecord(record, isLogged: false);
        }

        /// <summary>
        /// Fight battle rounds until there is a winner.
        /// </summary>
        private void _executeMainBattleLoop()
        {
            do
            {
                ++NumRounds;
                ExecuteBattleRound();
            } while (
            (AttackerState == CombatantState.COMBATANT_STATE_RALLIED) && 
            (DefenderState == CombatantState.COMBATANT_STATE_RALLIED)
            );
        }

        /// <summary>
        /// attackers gather any like race allied they can muster in the hex
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="attackerHome"></param>
        public void GatherAttackers(Civilization attacker, 
            HexData attackerHome)
        {
            AttackerList.Add(attacker);
            foreach(var lair in attackerHome.LairList)
            {
                if (lair.IsRuins()) { continue; }
                if (lair.HomeCiv.GetFullName() == attacker.GetFullName())
                {
                    continue;
                }
                if (lair.HomeCiv.Patricians.BaseAncestry.Name 
                    == attacker.Patricians.BaseAncestry.Name)
                {
                    if (attacker.LeaderCompetency > _dice.Roll(1, 12))
                    {
                        AttackerList.Add(lair.HomeCiv);
                    }
                }
            }
        }

        /// <summary>
        /// Defenders gather any like allied forces they can muster in the hex
        /// </summary>
        /// <param name="defender"></param>
        /// <param name="defenderHome"></param>
        public void GatherDefenders(Civilization defender, 
            HexData defenderHome)
        {
            DefenderList.Add(defender);
            foreach(var lair in defenderHome.LairList)
            {
                if (lair.IsRuins()) { continue; }
                if (lair.HomeCiv.GetFullName() == defender.GetFullName())
                {
                    continue;
                }
                if (lair.HomeCiv.Patricians.BaseAncestry.Name 
                    == defender.Patricians.BaseAncestry.Name)
                {
                    if (defender.LeaderCompetency > _dice.Roll(1, 12))
                    {
                        DefenderList.Add(lair.HomeCiv);
                    }
                }
            }
        }

        /// <summary>
        /// Tally up the total forces on a side
        /// </summary>
        /// <param name="allianceList"></param>
        /// <returns></returns>
        public int GetTotalCombatants(List<Civilization> allianceList)
        {
            int total = 0;
            foreach(var ally in allianceList)
            {
                total += ally.Patricians.Members;
            }
            return total;
        }

        /// <summary>
        /// Run a single round of combat
        /// </summary>
        public void ExecuteBattleRound()
        {
            // TODO: should these calculations be performaed by the population obj?
            int attackerForces = GetTotalCombatants(AttackerList);
            var attackerPop = AttackerList[0].Patricians;
            var attackerAncestry = attackerPop.BaseAncestry;
            int numAttackerUnits = AttackerList.Count;
            int attackerNumAttacks = 
                (attackerAncestry.BaseNumAttacks * attackerForces) 
                + attackerPop.BonusAttacks;
            int attackerHitBonus = attackerAncestry.BaseToHit 
                + attackerPop.ToHitMod;
            int attackerAc = attackerAncestry.BaseAc + attackerPop.AcMod;
            int attackerHd = attackerAncestry.HitDice 
                + attackerPop.HitDiceBonus;
            int attackerDamageTaken = 0;
            int defenderForces = GetTotalCombatants(DefenderList);
            var defenderPop = DefenderList[0].Patricians;
            var defenderAncestry = defenderPop.BaseAncestry;
            int numDefenderunits = DefenderList.Count;
            int defenderNumAttacks = 
                defenderAncestry.BaseNumAttacks * defenderForces 
                + defenderPop.BonusAttacks;
            int defenderHitBonus = defenderAncestry.BaseToHit 
                + defenderPop.ToHitMod;
            int defenderAc = defenderAncestry.BaseAc + defenderPop.AcMod;
            int defenderHd = defenderAncestry.HitDice 
                + defenderPop.HitDiceBonus;
            int defenderDamageTaken = 0;

            // attacker attacks
            for (int i = 0; i < attackerNumAttacks; ++i)
            {
                int attackRoll = _dice.Roll(1, 20) + attackerHitBonus;
                if (attackRoll >= defenderAc) { ++defenderDamageTaken; }
            }
            int attackerCasualties = attackerDamageTaken / attackerHd;

            // defender counters 
            for (int i = 0; i < defenderNumAttacks; ++i)
            {
                int attackRoll = _dice.Roll(1, 20) + defenderHitBonus;
                if (attackRoll >= attackerAc) { ++attackerDamageTaken; }
            }
            int defenderCasualties = defenderDamageTaken / defenderHd;

            // attackers take losses
            float attackerUnitLosses = attackerCasualties / numAttackerUnits;
            if (attackerUnitLosses > 0 && attackerUnitLosses < 1)
            {
                attackerUnitLosses = 1;
            }
            foreach (var attacker in AttackerList)
            {
                if (attacker.Patricians.Members <= attackerUnitLosses)
                {
                    attacker.Patricians.Members = 0;
                }
                else
                {
                    attacker.Patricians.Members -= (int)attackerUnitLosses;
                }
            }

            // defender takes losses
            float defenderUnitLosses = defenderCasualties / numDefenderunits;
            if (defenderUnitLosses > 0 && defenderUnitLosses < 1)
            {
                defenderUnitLosses = 1;
            }
            foreach (var defender in DefenderList)
            {
                if (defender.Patricians.Members <= defenderUnitLosses)
                {
                    defender.Patricians.Members = 0;
                }
                else
                {
                    defender.Patricians.Members -= (int)defenderUnitLosses;
                }
            }

            _attackerScore += defenderCasualties;
            _defenderScore += attackerCasualties;

            // determine loser and check morale...
            int moraleRoll = _dice.Roll(1, 20);
            int attackMoraleBonus = AttackerList[0].Patricians.MoraleBonus +
                AttackerList[0].Patricians.BaseAncestry.MoraleBonus;
            int defenseMoraleBonus = DefenderList[0].Patricians.MoraleBonus +
                DefenderList[0].Patricians.BaseAncestry.MoraleBonus;

            if (_defenderScore > _attackerScore)
            {
                // attacker losing
                if (GetTotalCombatants(AttackerList) 
                    <= _attackerStartingForces / 3)
                {
                    AttackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if ((moraleRoll + attackMoraleBonus) < 10)
                {
                    AttackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
            }
            else if (_attackerScore > _defenderScore)
            {
                // defender losing
                if (GetTotalCombatants(DefenderList)
                    <= _defenderStartingForces / 3)
                {
                    DefenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if ((moraleRoll + defenseMoraleBonus) < 10)
                {
                    DefenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }

            }
            // else stalemate, no moaral checks

            // Very unlikely, but check for genocide
            if (GetTotalCombatants(AttackerList) == 0)
            {
                AttackerState = CombatantState.COMBATANT_STATE_ELIMINATED;
            }
            if (GetTotalCombatants(DefenderList) == 0)
            {
                DefenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
            }
        }

        /// <summary>
        /// Not all who fall in battle are lost
        /// </summary>
        /// <param name="startingNumbers"></param>
        /// <param name="army"></param>
        public void ResolveSurvivors(int startingNumbers, List<Civilization> army)
        {
            var unharmed = GetTotalCombatants(army);
            var losses = startingNumbers - unharmed;
            int result = _dice.Roll(1, 10);
            int replacements;
            if (result <= 3) { 
                replacements = (int)(losses * 0.3); 
            }
            else if (result <= 5) { 
                replacements = (int)(losses * 0.5); 
            }
            else if (result <= 7) { 
                replacements = (int)(losses * 0.75); 
            }
            else if (result == 8) { 
                replacements = losses; 
            }
            else if (result == 9) { 
                replacements = losses + (int)(startingNumbers * 0.1); 
            }
            else
            {
                replacements = 0;
            }

            int unitReplacements = replacements / army.Count;

            foreach(var unit in army)
            {
                unit.Patricians.Members += unitReplacements;
            }
        }

        /// <summary>
        /// The refugees civ searches the current hex for a similar race, and 
        /// joins them if they find it.
        /// </summary>
        /// <param name="refugees"></param>
        /// <param name="hex"></param>
        /// <param name="homeName"></param>
        /// <returns>Returns true if a sutible settlement was found, 
        /// returns false if not.</returns>
        bool RefugeesSearchHex(Civilization refugees, HexData hex, 
            string homeName)
        {
            foreach(var lair in hex.LairList)
            {
                // can't keep the base they just lost.
                if (lair.Name == homeName) { continue; }

                // note it is interesting that a refugee group will not take up 
                // in a ruins...
                if (lair.IsRuins()) { continue; }

                // search for a matching race in the hex
                Civilization targetCiv = lair.HomeCiv;
                if (targetCiv.Patricians.BaseAncestry.Name !=
                    refugees.Patricians.BaseAncestry.Name)
                {
                    continue;
                }

                // found a match
                targetCiv.JoinOurCivilization(refugees.GetFullName());
                targetCiv.Patricians.Members += refugees.Patricians.Members;
                refugees.DissolvePopulation();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Losers of a battle will try to search their home hex for a like
        /// race to join, or and adjacent hex if that fails.
        /// If that fails, they are eliminated.
        /// </summary>
        /// <param name="losers"></param>
        /// <param name="loserHome"></param>
        /// <param name="baseName">The name of the home base that the refugees 
        /// just left. They can't go back here.</param>
        void MoveLosers(Civilization losers, HexData loserHome, 
            string baseName)
        {
            bool isFoundHex = RefugeesSearchHex(losers, loserHome, baseName);
            if (isFoundHex) { return; }
            int nextIndex = _dice.Roll(1, 6);
            bool isFound = false;
            for (int i = 0; i < 6; ++i)
            {
                ++nextIndex;
                if (nextIndex > 6) { nextIndex = 1; }
                var nextCoords = loserHome.FindNeighborByIndex(nextIndex);
                var nextHex = _worldMap.GetHexByCoordinates(nextCoords.Item1, 
                    nextCoords.Item2); 
                if (nextHex is null) { continue; }
                isFound = RefugeesSearchHex(losers, nextHex, baseName);
                if (isFound) { break; }
            }
            if (isFound) { return; }

            // no available place to go!
            string report = "The " + losers.GetPluralName()
                + " could not find refuge.";
            losers.History.addRecord(report);

            losers.DissolvePopulation();
        }
    }
}
