using System;
using System.Collections.Generic;
using System.Text;

namespace StackWars.Core.Units
{
    interface ISpecialAction
    {
        int SpecialActionStrength { get; }

        void DoSpecialAction();
    }
}
