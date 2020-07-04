using StackWars.Core.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StackWars.Core.Army
{
    public class AliveUnitsEnumerable : IEnumerable<IUnit>
    {
        IEnumerable<IUnit> _units;

        public AliveUnitsEnumerable(IEnumerable<IUnit> units)
        {
            _units = units;
        }

        public bool IsAnyAlive() => _units.Any(u => u.Health > 0);

        public IEnumerator<IUnit> GetEnumerator() => 
            _units.Where(u => u.Health > 0).GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => 
            GetEnumerator();
    }
}
