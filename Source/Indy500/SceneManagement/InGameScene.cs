using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indy500.SceneManagement
{
    public class InGameScene : IScene
    {
        private readonly IRenderer renderer;
        private readonly SceneManager sceneManager;

        public GameManager GameManager { get; }

        public InGameScene(GameManager gameManager, SceneManager sceneManager)
        {
            renderer = new Simple2DRenderer(gameManager.MessageDispatcher);
            GameManager = gameManager;
            this.sceneManager = sceneManager;
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            renderer.LoadContent(graphicsDevice, content);
        }

        public void Update(GameTime gameTime)
        {
            GameManager.CurrentRace.Update(gameTime);
            renderer.Update(gameTime);
            if (GameManager.CurrentRace.Mode.IsOver())
                sceneManager.TransitionTo(SceneState.MainMenu);
        }

        public void Draw(GameTime gameTime)
        {
            renderer.Draw(GameManager.CurrentRace, gameTime);
        }

        public void Reset()
        {
        }
    }
}
