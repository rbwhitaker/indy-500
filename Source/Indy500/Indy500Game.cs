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
        private SpriteBatch spriteBatch;
        private Race activeRace;
        private IRenderer renderer;
        
        public Indy500Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();
            GameSettings.Initialize();
            activeRace = new Race(CreateSimpleTrack(), CreatePlayers());
        }

        private Track CreateSimpleTrack(int rows = 24, int columns = 40, int islandWidth = 18, int islandHeight = 5)
        {
            Track track = new Track(rows, columns);

            // Set up the outside edge.
            for (int x = 0; x < columns; x++)
            {
                track[0, x] = TrackTileType.Dirt;
                track[rows - 1, x] = TrackTileType.Dirt;
            }

            for (int y = 0; y < rows; y++)
            {
                track[y, 0] = TrackTileType.Dirt;
                track[y, columns - 1] = TrackTileType.Dirt;
            }

            // Set up the "island" in the middle
            for (int x = (columns - islandWidth) / 2; x < (columns + islandWidth) / 2; x++)
                for (int y = (rows - islandHeight) / 2; y < (rows + islandHeight) / 2; y++)
                    track[y, x] = TrackTileType.Dirt;

            return track;
        }

        private IEnumerable<Car> CreatePlayers()
        {
            return new List<Car>
            {
                new Car(new ControlledPlayer()),
                new Car(new DoNothingPlayer())
            };
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            renderer = new Simple2DRenderer();
            renderer.LoadContent(GraphicsDevice, Content);
        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            activeRace.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            renderer.Draw(activeRace, gameTime);

            base.Draw(gameTime);
        }
    }
}
