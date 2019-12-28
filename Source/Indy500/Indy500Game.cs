using Indy500.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace Indy500
{
    public class Indy500Game : Game
    {
        private GraphicsDeviceManager graphics;
        private InGameScene inGameScene;
        
        public Indy500Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

        }

        protected override void Initialize()
        {
            GameSettings.Initialize();
            inGameScene = new InGameScene();
            graphics.IsFullScreen = GameSettings.IsFullScreen;
            graphics.PreferredBackBufferWidth = GameSettings.Width;
            graphics.PreferredBackBufferHeight = GameSettings.Height;
            graphics.ApplyChanges();
            Window.AllowUserResizing = true;
            Window.ClientSizeChanged += WindowSizeChanged;
            base.Initialize();
        }

        private void WindowSizeChanged(object sender, EventArgs e)
        {
            GameSettings.Width = Window.ClientBounds.Width;
            GameSettings.Height = Window.ClientBounds.Height;
            GameSettings.SaveConfigFile();
        }

        protected override void LoadContent()
        {
            inGameScene.LoadContent(GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            inGameScene.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            inGameScene.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
