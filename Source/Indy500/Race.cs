using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Indy500
{
    public class Race
    {
        public Track Track { get; }

        public IReadOnlyList<Car> Cars { get; }

        public Race(Track track, IEnumerable<Car> cars)
        {
            Track = track;
            Cars = cars.ToList();
        }

        public void Update(GameTime gameTime)
        {
            const float accelerationRate = 2f;
            const float turnRate = 3f;
            const float maxRoadSpeed = 5f;

            foreach(Car car in Cars)
            {
                PlayerInput input = car.ControllingPlayer.Update(gameTime, this, car);
                
                // Update speed based on acceleration
                float acceleration = MathHelper.Clamp(input.AccelerationAmount, 0, 1);
                car.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * accelerationRate;
                car.Speed *= (float)car.MaxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * acceleration;
                if (car.Speed > maxRoadSpeed) car.Speed = maxRoadSpeed;
                //Debug.WriteLine(car.Speed);
                // Update heading based on turning
                float turning = MathHelper.Clamp(input.TurnAmount, -1, +1);
                car.Heading += turning * turnRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
                car.Position += new Vector2((float)Math.Cos(car.Heading), (float)Math.Sin(car.Heading)) * car.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Wrap around
                if (car.Position.X > Track.Columns) car.Position = new Vector2(0, car.Position.Y);
                if (car.Position.X < 0) car.Position = new Vector2(Track.Columns, car.Position.Y);
                if (car.Position.Y > Track.Rows) car.Position = new Vector2(car.Position.X, 0);
                if (car.Position.Y < 0) car.Position = new Vector2(car.Position.X, Track.Rows);
            }
        }
    }
}
