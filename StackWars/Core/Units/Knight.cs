using System.Collections.Generic;
using System.Linq;

namespace StackWars.Core.Units
{
    class Knight : Unit, IEquipable // выс. цена, выс.урон и защита
    {
        public override int Health { get; protected set; } = 120;

        public override int Attack { get; protected set; } = 35;

        public override int Armor { get; protected set; } = 20;

        public override int Cost { get; protected set; } = 175;

        public IEquipable EquipWith(IEquipmentItem item) =>
            new EquippedUnit(this, item);    
    }
}
