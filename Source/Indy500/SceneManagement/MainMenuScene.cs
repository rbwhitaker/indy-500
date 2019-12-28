using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Indy500.SceneManagement
{
    public class MainMenuScene : IScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;

        private StackPanel uiRoot;

        public SceneManager SceneManager { get; }

        public MainMenuScene(SceneManager sceneManager)
        {
            SceneManager = sceneManager;
        }
        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            background = content.Load<Texture2D>("MenuBackground");
            spriteBatch = new SpriteBatch(graphicsDevice);

            uiRoot = new StackPanel();
            uiRoot.Bounds = new Rectangle(0, 0, GameSettings.Width, GameSettings.Height);
            uiRoot.AddChild(new Placeholder(50, 0));
            uiRoot.AddChild(new Image(content.Load<Texture2D>("Logo")));
            uiRoot.AddChild(new Placeholder(30, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("TitleFont"), "Main Menu".ToUpper()) { Color = Color.Yellow });
            uiRoot.AddChild(new Placeholder(30, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "Start!".ToUpper()) { Color = Color.White });
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "Credits".ToUpper()) { Color = Color.DarkGray });
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GameSettings.Width, GameSettings.Height), Color.White);
            uiRoot.Draw(spriteBatch);
            spriteBatch.End();
        }


        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().GetPressedKeys().Length > 0 || Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed)
                SceneManager.TransitionTo(SceneState.InGame);
        }
    }
}
