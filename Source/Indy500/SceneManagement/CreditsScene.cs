﻿using Microsoft.Xna.Framework;
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
            //uiRoot.AddChild(new Image(content.Load<Texture2D>("Logo")));
            //uiRoot.AddChild(new Placeholder(30, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("TitleFont"), "Credits".ToUpper()) { Color = Color.Yellow });
            uiRoot.AddChild(new Placeholder(10, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "Edgar Cocco    https://github.com/edgarcocco"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "       Joel    https://github.com/moolicc   "));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), " PiscesMike    https://github.com/PiscesMike"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "RB Whitaker    https://github.com/rbwhitaker"));
            uiRoot.AddChild(new Placeholder(10, 0));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "Music from https://filmmusic.io/"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "\"Android Sock Hop\", \"Arroz Con Pollo\", \"Captain Scurvy\", \"Deep and Dirty\""));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "\"Desert of Lost Souls\", \"Fantasia Fantasia\", \"Half Mystery\", \"Laserpack\""));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "\"Loopster\", \"Onion Capers\", \"Raving Energy\", \"Realizer\""));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "\"Rising Tide (faster)\", \"Robozo\", \"Tyrant\", \"Verano Sensual\""));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "by Kevin MacLeod (https://incompetech.com)"));
            uiRoot.AddChild(new TextBlock(content.Load<SpriteFont>("BodyFont"), "License: CC BY (http://creativecommons.org/licenses/by/4.0/)"));

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
