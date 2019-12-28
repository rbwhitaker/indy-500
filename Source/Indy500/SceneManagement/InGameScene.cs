using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Linq;

namespace Indy500.SceneManagement
{
    public class InGameScene : IScene
    {
        private Race activeRace;
        private readonly IRenderer renderer;

        private MessageDispatcher messageDispatcher = new MessageDispatcher();

        public InGameScene()
        {
            renderer = new Simple2DRenderer(messageDispatcher);
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            Level level = Level.Parse(System.IO.File.ReadAllText("LevelExample.txt"));

            //activeRace = new Race(CreateSimpleTrack(), CreatePlayers(), new RaceMode(3, 24, 40, 18, 5));
            activeRace = new Race(CreateTrackFromLevel(level), CreatePlayersFromLevel(level), new RaceMode(3, level.StartLine, level.AIWaypoints.Select(l => (LineSegment)l)), messageDispatcher);

            renderer.LoadContent(graphicsDevice, content);
        }

        private Track CreateTrackFromLevel(Level level)
        {
            Track result = new Track(level.LevelSize.Y, level.LevelSize.X);

            for (int x = 0; x < level.LevelSize.X; x++)
            {
                for (int y = 0; y < level.LevelSize.Y; y++)
                {
                    result[y, x] = (TrackTileType)level.Tiles[x, y];
                }
            }

            return result;
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

        private IEnumerable<Car> CreatePlayersFromLevel(Level level)
        {
            int xOffset = 0;
            int yOffset = 0;

            void IncrementOffsets(Car car)
            {
                if (level.StartLine.StartX == level.StartLine.EndX)
                {
                    yOffset += (int)car.Size.Y + 1;
                }
                else if (level.StartLine.StartY == level.StartLine.EndY)
                {
                    xOffset += (int)car.Size.X + 1;
                }
                else
                {
                    xOffset += (int)car.Size.X + 1;
                    yOffset += (int)car.Size.Y + 1;
                }
            }
            var cars = new List<Car>();
            var playerCar = new Car(new ControlledPlayer());

            playerCar.Position = new Vector2(level.StartLine.StartX, level.StartLine.StartY);
            cars.Add(playerCar);

            IncrementOffsets(playerCar);

            for (int i = 1; i < level.MaxPlayers; i++)
            {
                var car = new Car(new DoNothingPlayer());
                car.Position = new Vector2(level.StartLine.StartX + xOffset, level.StartLine.StartY + yOffset);
                cars.Add(car);
                IncrementOffsets(car);
            }

            return cars;
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
            renderer.Update(gameTime);
        }

        public void Draw(GameTime gameTime)
        {
            renderer.Draw(activeRace, gameTime);
        }
        public void Reset()
        {
        }
    }
}
