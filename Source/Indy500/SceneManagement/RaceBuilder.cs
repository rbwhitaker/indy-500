using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Indy500.SceneManagement
{
    public class RaceBuilder
    {
        public static Race FromLevel(Level level, MessageDispatcher messageDispatcher)
        {
            return new Race(
                CreateTrackFromLevel(level),
                CreatePlayersFromLevel(level),
                CreateModeFromLevel(level), messageDispatcher);
        }

        public static IGameMode CreateModeFromLevel(Level level)
        {
            return new RaceMode(5, level.StartLine, level.AIWaypoints.Select(l => (LineSegment)l));
        }

        public static Track CreateTrackFromLevel(Level level)
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

        public static IEnumerable<Car> CreatePlayersFromLevel(Level level)
        {
            float xOffset = 0;
            float yOffset = 0;
            float spacing = 0.1f;

            void IncrementOffsets(Car car)
            {
                if (level.StartLine.StartX == level.StartLine.EndX)
                {
                    yOffset += (int)car.Size.Y + spacing;
                }
                else if (level.StartLine.StartY == level.StartLine.EndY)
                {
                    xOffset += (int)car.Size.X + spacing;
                }
                else
                {
                    xOffset += (int)car.Size.X + spacing;
                    yOffset += (int)car.Size.Y + spacing;
                }
            }
            var cars = new List<Car>();
            var playerCar = new Car(new ControlledPlayer());

            playerCar.Position = new Vector2(level.StartLine.StartX, level.StartLine.StartY);
            cars.Add(playerCar);

            IncrementOffsets(playerCar);

            for (int i = 1; i < level.MaxPlayers; i++)
            {
                var car = new Car(new RBsAIPlayer());
                car.Position = new Vector2(level.StartLine.StartX + xOffset, level.StartLine.StartY + yOffset);
                cars.Add(car);
                IncrementOffsets(car);
            }

            return cars;
        }
    }
}
