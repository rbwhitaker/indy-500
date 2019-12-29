using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

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
            MakeNewLevel();

            renderer.LoadContent(graphicsDevice, content);
        }

        private void MakeNewLevel()
        {
            Level level = Level.Parse(System.IO.File.ReadAllText("LevelExample.txt"));
            activeRace = RaceBuilder.FromLevel(level, messageDispatcher);
        }

        private KeyboardState previousState = new KeyboardState();
        public void Update(GameTime gameTime)
        {
            activeRace.Update(gameTime);
            renderer.Update(gameTime);

            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Back) && previousState.IsKeyUp(Keys.Back))
                MakeNewLevel();
            previousState = currentState;
        }

        public void Draw(GameTime gameTime)
        {
            renderer.Draw(activeRace, gameTime);
        }

        public void Reset()
        {
            previousState = new KeyboardState();
        }
    }
}
