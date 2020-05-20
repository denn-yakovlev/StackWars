using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StackWars.Core.Units;

namespace StackWars.Core.Game
{
    class Game : IGame
    {
        private IEnumerable<IUnit> _firstArmy;

        private IEnumerable<IUnit> _secondArmy;

        private static Random _random = new Random();

        public Game(IEnumerable<IUnit> firstArmy, IEnumerable<IUnit> secondArmy)
        {
            _firstArmy = new List<IUnit>(firstArmy);
            _secondArmy = new List<IUnit>(secondArmy);
        }

        private bool IsAlive(IUnit unit) => unit.Health > 0;

        private void Attack(IUnit attacker, IUnit defender)
        {
            defender.TakeDamage(attacker.Attack);
            if (IsAlive(defender))
                attacker.TakeDamage(defender.Attack);
        }

        private void SetArmiesReferences(IEnumerable<IUnit> allies, IEnumerable<IUnit> enemies)
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

        private IEnumerable<IUnit> DeadRemoved(IEnumerable<IUnit> army) =>
            army.Where(unit => IsAlive(unit));

        private void RemoveAllDeads()
        {
            _firstArmy = DeadRemoved(_firstArmy);
            _secondArmy = DeadRemoved(_secondArmy);
        }

        private void ChooseOrder(
            out IEnumerable<IUnit> attackingArmy, 
            out IEnumerable<IUnit> defendingArmy
            )
        {
            if (_random.NextDouble() < 0.5)
            {
                attackingArmy = _firstArmy;
                defendingArmy = _secondArmy;
            }
            else
            {
                attackingArmy = _secondArmy;
                defendingArmy = _firstArmy;
            }
        }

        public void Turn()
        {
            ChooseOrder(
                out IEnumerable<IUnit> attackingArmy,
                out IEnumerable<IUnit> defendingArmy
                );

            var attacker = attackingArmy.First();
            var defender = defendingArmy.First();

            Attack(attacker, defender);
            CastAbilities();
            RemoveAllDeads();
        }

        private bool OneArmyDestroyed() => _firstArmy.Count() == 0 || _secondArmy.Count() == 0;

        public void TurnToEnd()
        {
            while (!OneArmyDestroyed())
            {
                SetArmiesReferences(_firstArmy, _secondArmy);
                SetArmiesReferences(_secondArmy, _firstArmy);
                Turn();
            }       
        }

    }
}
