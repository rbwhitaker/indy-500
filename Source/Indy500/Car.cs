using Microsoft.Xna.Framework;

namespace Indy500
{
    public class Car
    {
        public float MaxSpeed = 100;
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }

        public Vector2 Size { get; }

        public IPlayer ControllingPlayer { get; }

        public Car(IPlayer player)
        {
            ControllingPlayer = player;
            Position = new Vector2(20, 18);
            Size = new Vector2(2, 2);
            Heading = MathHelper.Pi;
        }
    }
}
