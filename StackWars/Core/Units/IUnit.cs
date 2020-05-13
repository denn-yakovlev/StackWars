using System;

namespace StackWars.Core.Units
{
    interface IUnit : ICloneable
    {
        int Health { get; }

        int Attack { get; }

        int Armor { get; }

        int Cost { get; }

        void TakeDamage(int damage);
    }
}
