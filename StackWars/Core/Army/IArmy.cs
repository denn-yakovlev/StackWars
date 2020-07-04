using StackWars.Core.Game;
using StackWars.Core.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackWars.Core.Army
{
    public interface IArmy : ICollection<IUnit>
    {
        AliveUnitsEnumerable ToAliveUnitsEnumerable();
    }
}
