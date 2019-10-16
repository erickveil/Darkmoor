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

        private enum CombatantState { 
            COMBATANT_STATE_RALLIED, COMBATANT_STATE_ROUTED, 
            COMBATANT_STATE_ELIMINATED
        };

        private CombatantState _attackerState = 
            CombatantState.COMBATANT_STATE_RALLIED;
        private CombatantState _defenderState = 
            CombatantState.COMBATANT_STATE_RALLIED;

        private int _attackerStartingForces;
        private int _defenderStartingForces;
        readonly Dice _dice;

        public Battle(Dice dice)
        {
            _dice = dice;
        }

        public void ResolveBattle(Civilization attacker, HexData attackerHome,
            Civilization defender, HexData defenderHome)
        {
            GatherAttackers(attacker, attackerHome);
            GatherDefenders(defender, defenderHome);
            _attackerStartingForces = GetTotalCombatants(AttackerList);
            _defenderStartingForces = GetTotalCombatants(DefenderList);

            do
            {
                ExecuteBattleRound();
            } while (
            (_attackerState == CombatantState.COMBATANT_STATE_RALLIED) && 
            (_defenderState == CombatantState.COMBATANT_STATE_RALLIED)
            );

            // TODO: determine outcome of battle.
            if (_attackerState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // attacker wins!
            }
            else if (_defenderState == CombatantState.COMBATANT_STATE_RALLIED)
            {
                // defender wins!
            }
            else
            {
                // mutual destruction is highly unlikely, but not impossible.
            }
        }

        public void GatherAttackers(Civilization attacker, 
            HexData attackerHome)
        {
            foreach(var lair in attackerHome.LairList)
            {
                if (lair.HomeCiv.GetFullName() == attacker.GetFullName())
                {
                    AttackerList.Add(lair.HomeCiv);
                }
            }
        }

        public void GatherDefenders(Civilization defender, 
            HexData defenderHome)
        {
            foreach(var lair in defenderHome.LairList)
            {
                if (lair.HomeCiv.GetFullName() == defender.GetFullName())
                {
                    DefenderList.Add(lair.HomeCiv);
                }
            }
        }

        public int GetTotalCombatants(List<Civilization> allianceList)
        {
            int total = 0;
            foreach(var ally in allianceList)
            {
                total += ally.Patricians.Members;
            }
            return total;
        }

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
                    // TODO: what to do if a civ is destroyed
                    // home becomes a ruin with no civ, but how?
                    // should subtraction of members be a function of the civ, population, or lair?
                    // should we provide the lairs to the class instead of the civs so we can alter them?
                    // perhaps we could just iterate the lairs at the end of the battle, and manipulate them
                    // when their pop shows 0.
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

            // determine loser and check morale...
            int moraleRoll = _dice.Roll(1, 20);
            if (attackerUnitLosses < defenderUnitLosses)
            {
                // attacker lost 
                if (GetTotalCombatants(AttackerList) 
                    <= _attackerStartingForces / 3)
                {
                    _attackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if (moraleRoll < 10)
                {
                    _attackerState = CombatantState.COMBATANT_STATE_ROUTED;
                }
            }
            else if (defenderUnitLosses < attackerUnitLosses)
            {
                // defender lost
                if (GetTotalCombatants(DefenderList)
                    <= _defenderStartingForces / 3)
                {
                    _defenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }
                else if (moraleRoll < 10)
                {
                    _defenderState = CombatantState.COMBATANT_STATE_ROUTED;
                }

            }
            // else stalemate, no moaral checks

            // Very unlikely, but check for genocide
            if (GetTotalCombatants(AttackerList) == 0)
            {
                _attackerState = CombatantState.COMBATANT_STATE_ELIMINATED;
            }
            if (GetTotalCombatants(DefenderList) == 0)
            {
                _defenderState = CombatantState.COMBATANT_STATE_ELIMINATED;
            }
        }
    }
}
