using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace Indy500
{
    public class Indy500Game : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        private Race activeRace;
        
        public Indy500Game()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            base.Initialize();

            activeRace = new Race(CreateSimpleTrack(), CreatePlayers());
        }

        private Track CreateSimpleTrack()
        {
            int rows = 24;
            int columns = 40;
            Track track = new Track(rows, columns);

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

            return track;
        }

        private IEnumerable<Car> CreatePlayers()
        {
            return new List<Car>
            {
                new Car(new DoNothingPlayer()),
                new Car(new DoNothingPlayer())
            };
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
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

            base.Draw(gameTime);
        }
    }
}
