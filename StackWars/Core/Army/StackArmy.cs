using StackWars.Core.Game;
using StackWars.Core.Units;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace StackWars.Core.Army
{
    class StackArmy : IArmy
    {
        public ICollection<IUnit> Units { get; }

        public StackArmy(IArmyFactory armyFactory, int armyCost)
        {
            Units = armyFactory.Create(armyCost);
        }

        public int Count => ToAliveUnitsEnumerable().Count();

        public bool IsReadOnly => Units.IsReadOnly;

        public void Add(IUnit item) => Units.Add(item);

        public void Clear() => Units.Clear();

        public bool Contains(IUnit item) => ToAliveUnitsEnumerable().Contains(item);

        public void CopyTo(IUnit[] array, int arrayIndex) => Units.CopyTo(array, arrayIndex);

        public bool Remove(IUnit item) => Units.Remove(item);

        //public static implicit operator AliveUnitsEnumerable (StackArmy army) => 
        //    new AliveUnitsEnumerable(army.Units);

        public AliveUnitsEnumerable ToAliveUnitsEnumerable()
            => new AliveUnitsEnumerable(Units);

        public IEnumerator<IUnit> GetEnumerator() => ToAliveUnitsEnumerable().GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public override string ToString()
        {
            return string.Join(Environment.NewLine, ToAliveUnitsEnumerable().Select(u => u.ToString()));
        }
    }
}
