namespace StackWars.Core.Units
{
    interface IEquipable : IUnit
    {
        /// <exception cref="EquipmentFailedException"></exception>
        IEquipable EquipWith(IEquipmentItem item);
    }
}
