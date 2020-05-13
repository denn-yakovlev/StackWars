using System;
using System.Collections.Generic;
using System.Text;

namespace StackWars.Core.Units
{
    class ArcherFactory : IUnitFactory
    {
        private IEnumerable<IUnit> _allies;

        private IEnumerable<IUnit> _enemies;

        public ArcherFactory(IEnumerable<IUnit> allies, IEnumerable<IUnit> enemies)
        {
            _allies = allies;
            _enemies = enemies;
        }

        public IUnit Create() => new Archer(_allies, _enemies);
    }
}
