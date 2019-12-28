using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public class RaceMode : IGameMode
    {
        private LineSegment finishLine;
        private LineSegment alternateLine;
        private int lapsRequired;

        public int ScoreForCar(Car car)
        {
            return lapsCompleted[car];
        }
        public RaceMode(int lapsRequired, int rows, int columns, int islandWidth, int islandHeight)
        {
            this.lapsRequired = lapsRequired;
            finishLine = new LineSegment(columns / 2, rows / 2 + islandHeight / 2, columns / 2, rows);
            alternateLine = new LineSegment(columns / 2, 0, columns / 2, rows / 2 + islandHeight / 2);
        }

        private Dictionary<Car, LineSegment> nextTarget = new Dictionary<Car, LineSegment>();
        private Dictionary<Car, int> lapsCompleted = new Dictionary<Car, int>();

        public void Update(GameTime gameTime, Race race)
        {
            EnsureInitialized(race);
            foreach(Car car in race.Cars)
            {
                Polygon boundary = CollisionDetection.GetBoundaryFor(car);
                foreach(LineSegment segment in boundary.Segments)
                {
                    LineSegment target = nextTarget[car];
                    if (segment.Intersects(target))
                    {
                        if (target == finishLine)
                        {
                            lapsCompleted[car]++;
                        }

                        nextTarget[car] = target == finishLine ? alternateLine : finishLine;
                    }
                }
            }
        }

        private void EnsureInitialized(Race race)
        {
            foreach(Car car in race.Cars)
            {
                if (!nextTarget.ContainsKey(car)) nextTarget[car] = alternateLine;
                if (!lapsCompleted.ContainsKey(car)) lapsCompleted[car] = 0;
            }
        }

        public bool IsOver()
        {
            return lapsCompleted.Values.Any(lapsCompleted => lapsCompleted >= lapsRequired);
        }

        public Car Winner => lapsCompleted.Where(kvp => kvp.Value >= lapsRequired).First().Key;
    }
}
