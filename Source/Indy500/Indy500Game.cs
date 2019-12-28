using Indy500.SceneManagement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Indy500
{
    public class Indy500Game : Game
    {
        private GraphicsDeviceManager graphics;

        private List<IScene> allScenes;
        private MainMenuScene mainMenuScene;
        private InGameScene inGameScene;
        private SceneManager sceneManager;
        
        public Indy500Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameSettings.Initialize();

            sceneManager = new SceneManager();
            inGameScene = new InGameScene();
            mainMenuScene = new MainMenuScene(sceneManager);

            allScenes = new List<IScene>();
            allScenes.Add(mainMenuScene);
            allScenes.Add(inGameScene);
            sceneManager.AddScene(SceneState.MainMenu, mainMenuScene);
            sceneManager.AddScene(SceneState.InGame, inGameScene);
            sceneManager.TransitionTo(SceneState.MainMenu);

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
            foreach(IScene scene in allScenes)
                scene.LoadContent(GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            sceneManager.ActiveScene.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            sceneManager.ActiveScene.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}
