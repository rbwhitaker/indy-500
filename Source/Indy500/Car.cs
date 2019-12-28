using Microsoft.Xna.Framework;

namespace Indy500
{
    public class Car
    {
        public Vector2 Position { get; set; }
        public float Speed { get; set; }
        public float Heading { get; set; }

        public IPlayer ControllingPlayer { get; }

        public Car(IPlayer player)
        {
            ControllingPlayer = player;
            Position = new Vector2(20, 12);
        }
    }
}
