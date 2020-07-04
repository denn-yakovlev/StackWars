using System;

namespace StackWars.Core.Units
{
    class EquipmentFailedException : Exception
    {
        private IEquipmentItem _item;

        private IEquipable _equipable;

        public EquipmentFailedException(IEquipmentItem item, IEquipable equipable)
        {
            _item = item;
            _equipable = equipable;
        }

        public override string Message => $"{_item } is already equipped on {_equipable}";
    }
}
