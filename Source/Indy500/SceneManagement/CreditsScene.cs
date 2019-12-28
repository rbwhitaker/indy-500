using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace Indy500.SceneManagement
{
    public class CreditsScene : IScene
    {
        private SpriteBatch spriteBatch;
        private Texture2D background;

        private StackPanel uiRoot;

        public SceneManager SceneManager { get; }

        public CreditsScene(SceneManager sceneManager)
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
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("TitleFont"), "Credits".ToUpper()) { Color = Color.Yellow });
            uiRoot.AddChild(new Placeholder(30, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "MooCow"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "PiscesMike"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "Edo"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("MenuItemFont"), "RB"));

            uiRoot.AddChild(new Placeholder(30, 0));
            uiRoot.AddChild(new MenuItem(content.Load<Texture2D>("MenuItemLeftMarker"), content.Load<Texture2D>("MenuItemRightMarker"), content.Load<SpriteFont>("MenuItemFont"), "Back".ToUpper()) { ActiveColor = Color.White, InactiveColor = Color.DarkGray, Active = true  });
        }

        public void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(background, new Rectangle(0, 0, GameSettings.Width, GameSettings.Height), Color.White);
            uiRoot.Draw(spriteBatch);
            spriteBatch.End();
        }


        private KeyboardState previousKeyboardState = new KeyboardState();
        public void Update(GameTime gameTime)
        {
            KeyboardState currentState = Keyboard.GetState();
            if (currentState.IsKeyDown(Keys.Space) && previousKeyboardState.IsKeyUp(Keys.Space))
            {
                SceneManager.TransitionTo(SceneState.MainMenu);
            }


            previousKeyboardState = currentState;
        }

        public void Reset()
        {
            previousKeyboardState = new KeyboardState();
        }
    }
}
