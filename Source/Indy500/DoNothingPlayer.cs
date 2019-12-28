using Microsoft.Xna.Framework;

namespace Indy500
{
    internal class DoNothingPlayer : IPlayer
    {
        private bool leftOrRight;
        private static bool nextLeftOrRight = true;

        public DoNothingPlayer()
        {
            leftOrRight = nextLeftOrRight;
            nextLeftOrRight = !nextLeftOrRight;
        }
        public PlayerInput Update(GameTime gameTime, Race race, Car carToControl)
        {
            return new PlayerInput(1, leftOrRight ? 1 : 0.05f);
        }
    }
}