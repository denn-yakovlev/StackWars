namespace StackWars.Core.Units
{
    class Knight : Unit // выс. цена, выс.урон и защита
    {
        public override int Health { get; protected set; } = 120;

        public override int Attack { get; } = 35;

        public override int Armor { get; } = 20;

        public override int Cost { get; } = 200;
    }
}
