using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indy500.SceneManagement
{
    public class InGameScene : IScene
    {
        private Race activeRace;
        private readonly IRenderer renderer;

        public InGameScene()
        {
            renderer = new Simple2DRenderer();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            activeRace = new Race(CreateSimpleTrack(), CreatePlayers(), new RaceMode(3, 24, 40, 18, 5));
            renderer.LoadContent(graphicsDevice, content);
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


        public void Update(GameTime gameTime)
        {
            activeRace.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            renderer.Draw(activeRace, gameTime);
        }

    }
}
