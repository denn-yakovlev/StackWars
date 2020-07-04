using StackWars.Core.Army;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWars.Core.Units
{
    class Archer : Unit, ISpecialAction, IReferencingAllies, IReferencingEnemies, IHealable
    {
        public override int MaxHealth { get; protected set; } = 80;

        public override int Attack { get; protected set; } = 30;

        public override int Armor { get; protected set; } = 5;

        public override int Cost { get; protected set; } = 125;

        public IArmy? Allies { get; set; }

        public IArmy? Enemies { get; set; }

        private RangeAttack? _rangeAttack;

        private RangeAttack _RangeAttack
        {
            get
            {
                if (_rangeAttack == null)
                    _rangeAttack = new RangeAttack(this);
                return _rangeAttack;
            }
        }

        public void DoSpecialAction() => _RangeAttack.Attack();

        private class RangeAttack
        {
            Archer _archer;

            public RangeAttack(Archer archer)
            {
                _archer = archer;
            }

            public int Damage => 60;

            private const int _range = 5;

            private const double _rangeHitChance = 0.75;

            private static Random _random = new Random();

            private int _EnemyPos => _range - (_archer as IReferencingAllies).PositionAmongAllies - 1;

            private bool ReachesEnemy() =>
                _EnemyPos >= 0 &&
                _EnemyPos < _archer.Enemies.Count;

            private bool HasChanceToHit() => _random.NextDouble() <= _rangeHitChance;

            public void Attack()
            {
                Logger.Instance.Log($"{_archer} пытается применить спец. способность.");
                if (!ReachesEnemy())
                {
                    Logger.Instance.Log($"{_archer} не дотягивается до противника");
                    return;
                }                 
                IUnit? targetEnemy = _archer.Enemies.ElementAt(_EnemyPos);
                if (HasChanceToHit())
                {
                    targetEnemy?.TakeDamage(Damage);
                    Logger.Instance.Log($"{_archer} дальней атакой наносит {Damage} урона юниту {targetEnemy}");
                }
                    
            }

        }
    }
}
