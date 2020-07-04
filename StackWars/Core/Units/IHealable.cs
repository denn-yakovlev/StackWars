using System;
using System.Collections.Generic;
using System.Text;

namespace StackWars.Core.Units
{
    interface IHealable
    {
        //int MaxHealth { get; }

        //int Health { get; protected set; }

        void Heal(int amount);
    }
}
