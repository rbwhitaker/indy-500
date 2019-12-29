using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public class Race
    {
        public Track Track { get; }

        public IReadOnlyList<Car> Cars { get; }
        public IGameMode Mode { get; }

        private MessageDispatcher messageDispatcher;

        public Race(Track track, IEnumerable<Car> cars, IGameMode gameMode, MessageDispatcher messageDispatcher)
        {
            Track = track;
            Mode = gameMode;
            Cars = cars.ToList();
            this.messageDispatcher = messageDispatcher;
        }

        private Dictionary<TrackTileType, float> speedsByType = new Dictionary<TrackTileType, float>
        {
            [TrackTileType.Road] = 10f,
            [TrackTileType.Dirt] = 1f
        };

        public void Update(GameTime gameTime)
        {
            if (Mode.IsOver()) return;

            const float accelerationRate = 10f;
            const float turnRate = 2f;

            foreach(Car car in Cars)
            {
                PlayerInput input = car.ControllingPlayer.Update(gameTime, this, car);
                
                // Update speed based on acceleration
                float acceleration = MathHelper.Clamp(input.AccelerationAmount, 0, 1);
                car.Speed += acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds * accelerationRate;
                //car.Speed *= (float)car.MaxSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds * acceleration;
                car.Speed *= 1 - (0.5f * (float)gameTime.ElapsedGameTime.TotalSeconds);

                // Update heading based on turning
                float turning = MathHelper.Clamp(input.TurnAmount, -1, +1);
                car.Heading += turning * turnRate * (float)gameTime.ElapsedGameTime.TotalSeconds;
                car.Position += new Vector2((float)Math.Cos(car.Heading), (float)Math.Sin(car.Heading)) * car.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Wrap around
                if (car.Position.X > Track.Columns) car.Position = new Vector2(0, car.Position.Y);
                if (car.Position.X < 0) car.Position = new Vector2(Track.Columns, car.Position.Y);
                if (car.Position.Y > Track.Rows) car.Position = new Vector2(car.Position.X, 0);
                if (car.Position.Y < 0) car.Position = new Vector2(car.Position.X, Track.Rows);

                Polygon boundary = CollisionDetection.GetBoundaryFor(car);
                var maxSpeed = CollisionDetection.IntersectedCells(boundary).Select(c => Track[c.Row, c.Column]).Distinct().Select(t => speedsByType[t]).Min();
                
                if (car.Speed > maxSpeed) car.Speed = maxSpeed;

                if(maxSpeed == 0.5f && car.ControllingPlayer is ControlledPlayer)
                    messageDispatcher.InvokeMessage(MessageType.Collision, car, new MessageArgs());
            }

            Mode.Update(gameTime, this);
        }

    }
}
