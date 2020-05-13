namespace StackWars.Core.Units
{
    class Knight : Unit // выс. цена, выс.урон и защита
    {
        public override int Health { get; protected set; } = 150;

        public override int Attack { get; } = 50;

        public override int Armor { get; } = 25;

        public override int Cost { get; } = 200;
    }
}
