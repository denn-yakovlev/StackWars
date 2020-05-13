namespace StackWars.Core.Units
{
    class UnitFactory<T> : IUnitFactory 
        where T: IUnit, new()
    {
        public IUnit Create() => new T();
    }
}
