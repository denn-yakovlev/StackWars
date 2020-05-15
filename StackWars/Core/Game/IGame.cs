using System;
using System.Collections.Generic;
using System.Text;

namespace StackWars.Core.Game
{
    interface IGame
    {
        void Turn();

        void TurnToEnd();
    }
}
