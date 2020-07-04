using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using StackWars.Core.Army;
using StackWars.Core.Units;

namespace StackWars.Core.Game
{
    class Game : IGame
    {
        private IArmy _firstArmy;
        private IArmy _secondArmy;

        private IList<AliveUnitsEnumerable> _firstArmyBattlers;
        private IList<AliveUnitsEnumerable> _secondArmyBattlers;

        private IBattleStrategy _strategy;

        private static Random _random = new Random();

        public Game(IArmy firstArmy, IArmy secondArmy, IBattleStrategy strategy)
        {
            _firstArmy = firstArmy;
            _secondArmy = secondArmy;
            _strategy = strategy;
            _firstArmyBattlers = strategy.GetBattlers(firstArmy);
            _secondArmyBattlers = strategy.GetBattlers(secondArmy);
        }

        private bool IsAlive(IUnit unit) => unit.Health > 0;

        private void Attack(IUnit attacker, IUnit defender)
        {
            var defenderHpBefore = defender.Health;
            defender.TakeDamage(attacker.Attack);
            var defenderHpAfter = defender.Health;
            Logger.Instance.Log($"{attacker} наносит {defenderHpBefore - defenderHpAfter} урона юниту {defender}");
            if (IsAlive(defender))
            {
                var attackerHpBefore = attacker.Health;             
                attacker.TakeDamage(defender.Attack);
                var attackerHpAfter = attacker.Health;
                Logger.Instance.Log($"{defender} отвечает и наносит {attackerHpBefore - attackerHpAfter} урона юниту {attacker}");
            }
            else
            {
                Logger.Instance.Log($"{defender} погиб");
            }
                
        }

        private void SetArmiesReferences(IArmy allies, IArmy enemies)
        {
            foreach (var unit in allies)
            {
                if (unit is IReferencingAllies refAllies)
                    refAllies.Allies = allies;
                if (unit is IReferencingEnemies refEnemies)
                    refEnemies.Enemies = enemies;
            }
        }

        private void CastAbilities()
        {
            var allSpecAction = _firstArmy
                .Concat(_secondArmy)
                .OfType<ISpecialAction>()
                .ToArray();
            foreach (var spec in allSpecAction)
            {
                if (IsAlive(spec as IUnit))
                    spec.DoSpecialAction();
            }
        }

        private void ChooseOrder(
            out AliveUnitsEnumerable attackers, 
            out AliveUnitsEnumerable defenders
            )
        {
            if (_random.NextDouble() < 0.5)
            {
                attackers = _firstArmyBattlers[_firstArmyBattlersPtr];
                defenders = _secondArmyBattlers[_secondArmyBattlersPtr];
                Logger.Instance.Log("Ход первой армии");
            }
            else
            {
                attackers = _secondArmyBattlers[_secondArmyBattlersPtr];
                defenders = _firstArmyBattlers[_firstArmyBattlersPtr];
                Logger.Instance.Log("Ход второй армии");
            }
        }

        private int _firstArmyBattlersPtr = 0;
        private int _secondArmyBattlersPtr = 0;

        private bool _firstArmyWon = false;
        private bool _secondArmyWon = false;

        public void Turn()
        {
            if (!_firstArmyWon && !_secondArmyWon)
            {
                SetArmiesReferences(_firstArmy, _secondArmy);
                SetArmiesReferences(_secondArmy, _firstArmy);
                SingleTurn();
            }
        }

        private void SingleTurn()
        {
            ChooseOrder(
                out AliveUnitsEnumerable attackers,
                out AliveUnitsEnumerable defenders
            );
            for (var i = 0; i < Math.Min(attackers.Count(), defenders.Count()); i++)
            {
                Attack(attackers.ElementAt(i), defenders.ElementAt(i));
            }
            CastAbilities();
            if (!_firstArmyBattlers[_firstArmyBattlersPtr].IsAnyAlive())
                _firstArmyBattlersPtr++;
            if (!_secondArmyBattlers[_secondArmyBattlersPtr].IsAnyAlive())
                _secondArmyBattlersPtr++;
            if (_firstArmyBattlersPtr >= _firstArmyBattlers.Count)
            {
                _firstArmyWon = true;
                Logger.Instance.Log("Победа первой армии");
            }               
            if (_secondArmyBattlersPtr >= _secondArmyBattlers.Count)
            {
                _secondArmyWon = true;
                Logger.Instance.Log("Победа второй армии");
            }

        }

        public void TurnToEnd()
        {
            while (!_firstArmyWon && !_secondArmyWon)
            {
                SetArmiesReferences(_firstArmy, _secondArmy);
                SetArmiesReferences(_secondArmy, _firstArmy);
                SingleTurn();
            }
        }

    }
}
