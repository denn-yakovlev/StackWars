using System;
using System.Collections.Generic;
using System.Linq;

namespace StackWars.Core.Units
{
    class Archer : Unit, ISpecialAction
    {
        public override int Health { get; protected set; } = 75;

        public override int Attack => 25;

        public override int Armor => 5;

        public override int Cost => 125;      

        private IEnumerable<IUnit> _allies;

        private IEnumerable<IUnit> _enemies;

        public Archer(IEnumerable<IUnit> allies, IEnumerable<IUnit> enemies)
        {
            _allies = allies;
            _enemies = enemies;
            _positionAmongAllies = _allies.TakeWhile(ally => ally != this).Count();
        }

        #region SpecialAction
        public int SpecialActionStrength => 60;

        private const int _range = 5;

        private int _positionAmongAllies;

        private const double _rangeHitChance = 0.75;

        private static Random _random = new Random();

        private bool ReachesEnemy() => _positionAmongAllies < _range;

        private bool HasChanceToHit() => _random.NextDouble() <= _rangeHitChance;

        public void DoSpecialAction()
        {
            if (!ReachesEnemy())
                return;
            IUnit targetEnemy = _enemies.ElementAt(_range - _positionAmongAllies - 1);
            if (HasChanceToHit())
                targetEnemy.TakeDamage(SpecialActionStrength);
        }
        #endregion
    }
}
