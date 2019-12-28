using Microsoft.Xna.Framework;

namespace Indy500
{
    internal class DoNothingPlayer : IPlayer
    {
        public PlayerInput Update(GameTime gameTime, Race race, Car carToControl)
        {
            return new PlayerInput(0, 0);
        }
    }
}