namespace StackWars.Core.Units
{
    interface IEquipable : IUnit
    {
        /// <exception cref="EquipmentFailed"></exception>
        IEquipable EquipWith(IEquipmentItem item);
    }
}
