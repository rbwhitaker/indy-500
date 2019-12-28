using Microsoft.Xna.Framework;

namespace Indy500
{
    public interface IGameMode
    {
        void Update(GameTime gameTime, Race race);
    }
}
