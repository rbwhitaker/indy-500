using Microsoft.Xna.Framework;
using System;

namespace Indy500
{
    internal class RBsAIPlayer : IPlayer
    {
        public RBsAIPlayer()
        {
        }

        private int waypointIndex = 0;
        private Vector2 waypointOffset = new Vector2(0, 0);
        private static Random random = new Random();
        public PlayerInput Update(GameTime gameTime, Race race, Car carToControl)
        {
            if(race.Mode is RaceMode raceMode)
            {
                Vector2 relativeToTarget = raceMode.WaypointGates[waypointIndex].Midpoint + waypointOffset - carToControl.Position;
                relativeToTarget = Vector2.Transform(relativeToTarget, Matrix.CreateRotationZ(-carToControl.Heading));
                double angle = Math.Atan2(-relativeToTarget.Y, relativeToTarget.X);
                if (relativeToTarget.Length() < 3f)
                {
                    waypointIndex = (waypointIndex + 1) % raceMode.WaypointGates.Count;
                    waypointOffset = new Vector2(MathHelper.Lerp(-0.5f, +0.5f, (float)random.NextDouble()), MathHelper.Lerp(-0.5f, +0.5f, (float)random.NextDouble()));
                }
                return new PlayerInput(1f, ComputeTurnRate(angle));

            }
            return new PlayerInput(0, 0);
        }

        private static int ComputeTurnRate(double angle)
        {
            if (Math.Abs(angle) < 0.01f) return 0;
            return angle > 0 ? -1 : 1;
        }
    }
}