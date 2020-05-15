using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWars.Core.Units
{
    class Archer : Unit, ISpecialAction, IReferencingAllies, IReferencingEnemies
    {
        public override int Health { get; protected set; } = 80;

        public override int Attack { get; } = 30;

        public override int Armor { get; } = 5;

        public override int Cost { get; } = 125;      

        public IEnumerable<IUnit> Allies { get; set; }

        public IEnumerable<IUnit> Enemies { get; set; }

        private int _PositionAmongAllies => Allies.TakeWhile(ally => ally != this).Count();

        private RangeAttack _rangeAttack;

        private RangeAttack _RangeAttack
        {
            get
            {
                if (_rangeAttack == null)
                    _rangeAttack = new RangeAttack { _Archer = this };
                return _rangeAttack;
            }
        }

        public int SpecialActionStrength => _RangeAttack.Damage;

        public void DoSpecialAction() => _RangeAttack.Attack();

        private class RangeAttack
        {
            public Archer _Archer;

            public int Damage => 60;

            private const int _range = 5;

            private const double _rangeHitChance = 0.75;

            private static Random _random = new Random();

            private int _EnemyPos => _range - _Archer._PositionAmongAllies - 1;

            private bool ReachesEnemy() =>
                 _EnemyPos >= 0 &&
                _EnemyPos < _Archer.Enemies.Count();

            private bool HasChanceToHit() => _random.NextDouble() <= _rangeHitChance;

            public void Attack()
            {
                if (!ReachesEnemy())
                    return;
                IUnit targetEnemy = _Archer.Enemies.ElementAt(_EnemyPos);
                if (HasChanceToHit())
                    targetEnemy.TakeDamage(Damage);
            }

        }
    }
}
