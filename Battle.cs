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
        public Battle(HexDataIndex worldMap)
        {
            _dice = Dice.Instance;
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

            string record;

            if (defenderBase.IsRuins())
            {
                record = (defenderBase.GetFullName() 
                    + " is found abandoned by the " 
                    + attacker.GetPluralName());
                defenderBase.History.addRecord(record);
                attacker.History.addRecord(record, isLogged: false);

                defenderBase.MoveCivIn(attacker);
                attackerBase.ForceAbandon();
                DefenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
                return;
            }
            Civilization defender = defenderBase.HomeCiv;

            record = "The " 
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
            bool isAttackerLost =
                (AttackerState == CombatantState.COMBATANT_STATE_ELIMINATED)
                || (AttackerState == CombatantState.COMBATANT_STATE_ROUTED);
            bool isDefenderLost =
                (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED)
                || (DefenderState == CombatantState.COMBATANT_STATE_ROUTED);
            bool isBothLost = isAttackerLost && isDefenderLost;
            bool isMutualDestruction =
                (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED
                && AttackerState == CombatantState.COMBATANT_STATE_ELIMINATED);
                

            if (isMutualDestruction)
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
            else if (isBothLost)
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
                else
                {
                    record = "The " 
                        + attacker.GetPluralName() + " have been repelled by the "
                        + defender.GetPluralName() + " at "
                        + defenderBase.GetFullName() + "!";
                    string attackerBaseName = attackerBase.Name;
                    // It's interesting that attackers don't go back home.
                    MoveLosers(attacker, attackerLands, attackerBaseName);
                    attackerBase.ForceAbandon();
                }
            }
            else if (isDefenderLost)
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
            else // attacker lost
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
            string record;
            if (defenderBase.IsRuins())
            {
                record = (defenderBase.GetFullName() 
                    + " is found abandoned by the " 
                    + attacker.GetPluralName());
                defenderBase.History.addRecord(record);
                attacker.History.addRecord(record, isLogged: false);

                defenderBase.MoveCivIn(attacker);
                DefenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
                return;
            }
            Civilization defender = defenderBase.HomeCiv;

            record = "The " 
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
            bool isAttackerLost =
                (AttackerState == CombatantState.COMBATANT_STATE_ELIMINATED)
                || (AttackerState == CombatantState.COMBATANT_STATE_ROUTED);
            bool isDefenderLost =
                (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED)
                || (DefenderState == CombatantState.COMBATANT_STATE_ROUTED);
            bool isBothLost = isAttackerLost && isDefenderLost;
            bool isMutualDestruction =
                (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED
                && AttackerState == CombatantState.COMBATANT_STATE_ELIMINATED);
             
            if (isMutualDestruction)
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
            else if (isBothLost)
            {
                if (DefenderState == CombatantState.COMBATANT_STATE_ELIMINATED)
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
                else
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
            }
            else if (isDefenderLost)
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
            else // attacker lost
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
                Console.WriteLine("Battle: Round " + NumRounds);
                ExecuteBattleRound(NumRounds);
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
                    != attacker.Patricians.BaseAncestry.Name)
                {
                    continue;
                }
                if (attacker.LeaderCompetency <= _dice.Roll(1, 12))
                {
                    continue;
                }
                AttackerList.Add(lair.HomeCiv);
                string record = "The " + attacker.GetPluralName()
                    + " have convinced the " + lair.HomeCiv.GetPluralName()
                    + " to aid them in their attack.";
                attacker.History.addRecord(record);
                lair.HomeCiv.History.addRecord(record, isLogged: false);
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
                    != defender.Patricians.BaseAncestry.Name)
                {
                    continue;
                }
                if (defender.LeaderCompetency <= _dice.Roll(1, 12))
                {
                    continue;
                }
                DefenderList.Add(lair.HomeCiv);
                string record = "The " + defender.GetPluralName()
                    + " have convinced the " + lair.HomeCiv.GetPluralName()
                    + " to come to their defense.";
                defender.History.addRecord(record);
                lair.HomeCiv.History.addRecord(record, isLogged: false);
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
        public void ExecuteBattleRound(int numRounds)
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

            Console.WriteLine(attackerForces + " attackers vs " 
                + defenderForces + " defenders");

            // attacker attacks
            for (int i = 0; i < attackerNumAttacks; ++i)
            {
                int attackRoll = _dice.Roll(1, 20) + attackerHitBonus;
                if (attackRoll >= defenderAc) { ++defenderDamageTaken; }
            }
            int defenderCasualties = defenderDamageTaken / defenderHd;


            // defender counters 
            for (int i = 0; i < defenderNumAttacks; ++i)
            {
                int attackRoll = _dice.Roll(1, 20) + defenderHitBonus;
                if (attackRoll >= attackerAc) { ++attackerDamageTaken; }
            }
            int attackerCasualties = attackerDamageTaken / attackerHd;

            Console.WriteLine("Attacker casualties: " + attackerCasualties
                + " | Defender casualties: " + defenderCasualties);

            // attackers take losses spread over units
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

            // defender takes losses spread over units
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

            Console.WriteLine("Total score: Attacker - " + _attackerScore
                + " | Defender - " + _defenderScore 
                + ". Current totals: Attacker - " 
                + GetTotalCombatants(AttackerList)
                + " | Defender - " + GetTotalCombatants(DefenderList)); 

            // determine loser and check morale...
            int moraleRoll = _dice.Roll(1, 20);
            int attackMoraleBonus = AttackerList[0].Patricians.MoraleBonus +
                AttackerList[0].Patricians.BaseAncestry.MoraleBonus;
            int defenseMoraleBonus = DefenderList[0].Patricians.MoraleBonus +
                DefenderList[0].Patricians.BaseAncestry.MoraleBonus;

            if (defenderCasualties < attackerCasualties)
            {
                Console.WriteLine("Attacker losing");
                // attacker losing
                if (GetTotalCombatants(AttackerList) 
                    <= _attackerStartingForces / 3)
                {
                    Console.WriteLine(
                        "Attacker lost over 1/3 foces and routing.");
                    AttackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if ((moraleRoll + attackMoraleBonus) < 10)
                {
                    Console.WriteLine("Attacker failed morale and routing.");
                    AttackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
            }
            else if (attackerCasualties < defenderCasualties)
            {
                Console.WriteLine("Defender losing");
                // defender losing
                if (GetTotalCombatants(DefenderList)
                    <= _defenderStartingForces / 3)
                {
                    Console.WriteLine(
                        "Defender lost over 1/3 forces and routing.");
                    DefenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if ((moraleRoll + defenseMoraleBonus) < 10)
                {
                    Console.WriteLine("Defender failed morale and routing.");
                    DefenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }
            }
            else if (numRounds > 10)
            {
                int defenseRoll = _dice.Roll(1, 20) + defenseMoraleBonus;
                int attackRoll = _dice.Roll(1, 20) + attackMoraleBonus;
                if (defenseRoll < attackRoll)
                {
                    DefenderState = CombatantState.COMBATANT_STATE_ROUTED;
                    Console.WriteLine("Defender routes from War Fatigue.");
                }
                else
                {
                    AttackerState = CombatantState.COMBATANT_STATE_ROUTED;
                    Console.WriteLine("Attacker routes form War Fatugue.");
                }
            }
            // else stalemate, no moaral checks until round 10

            // Very unlikely, but check for genocide
            if (GetTotalCombatants(AttackerList) == 0)
            {
                Console.WriteLine("Attacker forces have been eliminated!");
                AttackerState = CombatantState.COMBATANT_STATE_ELIMINATED;
            }
            if (GetTotalCombatants(DefenderList) == 0)
            {
                Console.WriteLine("Defender forces have been eliminated!");
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
            string report;
            if (result <= 3) { 
                replacements = (int)(losses * 0.3);
                report = "30%";
            }
            else if (result <= 5) { 
                replacements = (int)(losses * 0.5);
                report = "50%";
            }
            else if (result <= 7) { 
                replacements = (int)(losses * 0.75);
                report = "75%";
            }
            else if (result == 8) { 
                replacements = losses;
                report = "100%";
            }
            else if (result == 9) { 
                replacements = losses + (int)(startingNumbers * 0.1);
                report = "110%";
            }
            else
            {
                replacements = 0;
                report = "none";
            }

            Console.WriteLine(army[0].GetPluralName() + " have regained "
                + report + " (" + replacements 
                + ") of their losses after the fight: From "
                + unharmed + " to " + (replacements + unharmed));

            int unitReplacements = (int)((float)replacements / (float)army.Count);

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
