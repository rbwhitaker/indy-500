using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indy500.SceneManagement
{
    public class InGameScene : IScene
    {
        private Race activeRace;
        private readonly IRenderer renderer;

        private MessageDispatcher messageDispatcher = new MessageDispatcher();

        public InGameScene()
        {
            renderer = new Simple2DRenderer(messageDispatcher);
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Level level = Level.Parse(System.IO.File.ReadAllText("LevelExample.txt"));

            activeRace = RaceBuilder.FromLevel(level, messageDispatcher);

            renderer.LoadContent(graphicsDevice, content);
        }

        public void Update(GameTime gameTime)
        {
            activeRace.Update(gameTime);
            renderer.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            renderer.Draw(activeRace, gameTime);
        }
        public void Reset()
        {
        }
    }
}
