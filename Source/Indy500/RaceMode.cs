using Microsoft.Xna.Framework;
using System;

namespace Indy500
{
    public class RaceMode : IGameMode
    {
        private LineSegment finishLine;
        public RaceMode(int rows, int columns, int islandWidth, int islandHeight)
        {
            finishLine = new LineSegment(columns / 2, rows / 2, columns / 2, rows);
        }

        public void Update(GameTime gameTime, Race race)
        {
            foreach(Car car in race.Cars)
            {
                Polygon boundary = CollisionDetection.GetBoundaryFor(car);
                foreach(LineSegment segment in boundary.Segments)
                {
                    if (segment.Intersects(finishLine)) Console.WriteLine("You're crossing the finish line!");
                }
            }
        }
    }
}
