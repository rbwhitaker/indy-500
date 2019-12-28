using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public class RaceMode : IGameMode
    {
        private LineSegment finishLine;
        // TODO: At some point, and this will require level loading alterations also, use polygons for this.
        // That way we can allow alternative routes along a track and a waypoint can be split across multiple routes.
        private IReadOnlyList<LineSegment> alternateLines;
        private int lapsRequired;

        private Dictionary<Car, Dictionary<LineSegment, int>> _traversals;

        public int ScoreForCar(Car car)
        {
            return lapsCompleted[car];
        }
        public RaceMode(int lapsRequired, LineSegment finishLine, IEnumerable<LineSegment> waypoints)
        {
            this.lapsRequired = lapsRequired;
            this.finishLine = finishLine;
            alternateLines = waypoints.ToList();
            _traversals = new Dictionary<Car, Dictionary<LineSegment, int>>();
        }

        private Dictionary<Car, int> lapsCompleted = new Dictionary<Car, int>();


        public void Update(GameTime gameTime, Race race)
        {
            EnsureInitialized(race);
            foreach(Car car in race.Cars)
            {
                Polygon boundary = CollisionDetection.GetBoundaryFor(car);
                foreach(LineSegment carSegment in boundary.Segments)
                {
                    if (carSegment.Intersects(finishLine))
                    {
                        bool incrementLap = true;
                        int lapsCompletedForCar = lapsCompleted[car];
                        foreach (LineSegment traversalSegment in alternateLines)
                        {
                            if (_traversals[car][traversalSegment] - lapsCompletedForCar != 1)
                            {
                                incrementLap = false;
                            }
                        }
                        if (incrementLap)
                        {
                            lapsCompleted[car]++;
                        }
                    }
                    foreach (LineSegment traversalSegment in alternateLines)
                    {
                        if (carSegment.Intersects(traversalSegment))
                        {
                            // Prevents the car from incrementing a single waypoint twice along a single lap.
                            // That would ruin the lap incrementation.
                            if(_traversals[car][traversalSegment] == lapsCompleted[car])
                            {
                                _traversals[car][traversalSegment]++;
                            }
                        }
                    }
                }
            }
        }

        private void EnsureInitialized(Race race)
        {
            foreach(Car car in race.Cars)
            {
                if (!_traversals.ContainsKey(car))
                {
                    _traversals.Add(car, new Dictionary<LineSegment, int>());
                    foreach (var item in alternateLines)
                    {
                        _traversals[car].Add(item, 0);
                    }
                }

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
