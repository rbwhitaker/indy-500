using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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
            uiRoot.AddChild(new MenuItem(content.Load<Texture2D>("MenuItemLeftMarker"), content.Load<Texture2D>("MenuItemRightMarker"), content.Load<SpriteFont>("MenuItemFont"), "Start!".ToUpper()) { ActiveColor = Color.White, InactiveColor = Color.DarkGray, Active = true });
            uiRoot.AddChild(new MenuItem(content.Load<Texture2D>("MenuItemLeftMarker"), content.Load<Texture2D>("MenuItemRightMarker"), content.Load<SpriteFont>("MenuItemFont"), "Credits".ToUpper()) { ActiveColor = Color.White, InactiveColor = Color.DarkGray });
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
                if(GetSelectedMenuItem() == GetMenuItems()[0]) SceneManager.TransitionTo(SceneState.InGame);
                if(GetSelectedMenuItem() == GetMenuItems()[1]) SceneManager.TransitionTo(SceneState.Credits);
            }

            int increment = 0;
            if (currentState.IsKeyDown(Keys.Down) && previousKeyboardState.IsKeyUp(Keys.Down))
                increment = +1;
            else if (currentState.IsKeyDown(Keys.Up) && previousKeyboardState.IsKeyUp(Keys.Up))
                increment = -1;

            if(increment != 0)
            {
                var menuItems = GetMenuItems();
                var selectedItem = GetSelectedMenuItem();
                foreach (var menuItem in menuItems)
                    menuItem.Active = false;
                menuItems[((menuItems.IndexOf(selectedItem) + 1) % menuItems.Count)].Active = true;
            }

            previousKeyboardState = currentState;
        }

        private List<MenuItem> GetMenuItems()
        {
            return uiRoot.Children.OfType<MenuItem>().ToList();
        }

        private MenuItem GetSelectedMenuItem()
        {
            var menuItems = GetMenuItems();
            return menuItems.FirstOrDefault(mi => mi.Active) ?? menuItems.First();
        }
        public void Reset()
        {
            previousKeyboardState = new KeyboardState();
        }
    }
}
