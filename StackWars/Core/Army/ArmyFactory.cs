using System;
using System.Collections.Generic;
using StackWars.Core.Units;

namespace StackWars.Core.Army
{
    abstract class ArmyFactory : IArmyFactory
    {
        protected abstract IUnit? GetUnit();

        public virtual ICollection<IUnit> Create(int maxCost)
        {
            var cost = 0;
            var result = new List<IUnit>();
            while (true)
            {
                IUnit? unit = GetUnit();
                if (unit != null && unit.Cost + cost <= maxCost)
                {
                    result.Add(unit);
                    cost += unit.Cost;
                }      
                else
                    break;
            }
            return result;
        }
    }
}
