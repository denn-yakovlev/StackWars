using System;

namespace StackWars.Core.Units
{
    public interface IUnit : ICloneable
    {
        int Health { get; }

        int MaxHealth { get; }

        int Attack { get; }

        int Armor { get; }

        int Cost { get; }

        void TakeDamage(int damage);
    }
}
