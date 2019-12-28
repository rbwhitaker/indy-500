using Microsoft.Xna.Framework;

namespace Indy500
{
    public interface IPlayer
    {
        PlayerInput Update(GameTime gameTime, Race race, Car carToControl);
    }
}
