using Microsoft.Xna.Framework;

namespace Indy500
{
    public interface IGameMode
    {
        // Maybe have a CreateAICar here somewhere to return a custom ai implementation per-game mode?
        // If we go this route, we can remove that nasty reference Update has to Race.
        void Update(GameTime gameTime, Race race);
        bool IsOver();
        Car Winner { get; }
    }
}
