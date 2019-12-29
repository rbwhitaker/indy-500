using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public class Simple2DRenderer : IRenderer
    {
        private Texture2D car;
        private Texture2D rectangle;
        private SpriteFont mainFont;

        private float tileSize = 20;
        private SpriteBatch spriteBatch;
        private ParticleEngine particleEngine;
        private Race race;

        MessageDispatcher messageDispatcher;

        private Dictionary<int, Color> playerColors = new Dictionary<int, Color>
        {
            [0] = Color.Red,
            [1] = Color.Blue,
            [2] = Color.Green,
            [3] = Color.Yellow
        };

        public Simple2DRenderer(MessageDispatcher messageDispatcher)
        {
            this.messageDispatcher = messageDispatcher;
        }
        public void Draw(Race race, GameTime gameTime)
        {
            this.race = race;
            spriteBatch.Begin();

            for(int row = 0; row < race.Track.Rows; row++)
            {
                for(int column = 0; column < race.Track.Columns; column++)
                {
                    spriteBatch.Draw(rectangle, new Rectangle((int)(column * tileSize), (int)(row * tileSize), (int)tileSize, (int)tileSize), race.Track[row, column] == TrackTileType.Dirt ? Color.SaddleBrown : Color.Gray);
                }
            }

            //Car playerCar = race.Cars[0];
            //foreach(var x in CollisionDetection.IntersectedCells(CollisionDetection.GetBoundaryFor(playerCar)))
            //{
            //    spriteBatch.Draw(rectangle, new Rectangle((int)(x.Column * tileSize), (int)(x.Row * tileSize), (int)tileSize, (int)tileSize), new Color(Color.Red, 0.2f));
            //    foreach (var point in CollisionDetection.GetBoundaryFor(playerCar).Points)
            //    spriteBatch.Draw(rectangle, new Rectangle((int)(point.X * tileSize - 2), (int)(point.Y * tileSize - 2), 4, 4), Color.Azure);
            //
            //}

            spriteBatch.End();

            particleEngine.Draw(spriteBatch);

            spriteBatch.Begin();
            foreach (Car c in race.Cars)
            {
                Color carColor = playerColors[race.Cars.ToList().IndexOf(c)];
                Vector2 centerPoint = new Vector2(car.Width / 2f, car.Height / 2f);
                Vector2 scale = new Vector2(1f / car.Width * tileSize * c.Size.X, 1f / car.Height * tileSize * c.Size.Y);
                spriteBatch.Draw(car, c.Position * tileSize, null, carColor, c.Heading, centerPoint, scale, SpriteEffects.None, 0);
            }

            if(race.Mode is RaceMode raceMode)
            {
                //foreach (var waypointLine in raceMode.WaypointGates)
                //spriteBatch.Draw(rectangle, new Rectangle((int)(waypointLine.Midpoint.X * tileSize - 2), (int)(waypointLine.Midpoint.Y * tileSize - 2), 4, 4), Color.Pink);

                foreach (Car c in race.Cars)
                {
                    int index = race.Cars.ToList().IndexOf(c);
                    spriteBatch.DrawString(mainFont, raceMode.ScoreForCar(c).ToString(), new Vector2(0, index * 20), Color.White);
                }
                if (raceMode.IsOver())
                    spriteBatch.DrawString(mainFont, raceMode.Winner == race.Cars[0] ? "Player 1 Wins!" : "Player 2 Wins!", new Vector2(500, 300), Color.Yellow);
            }

            spriteBatch.End();
        }

        public void Update(GameTime gameTime)
        {
            particleEngine.Update();

            if (race != null)
                foreach (Car c in race.Cars)
                {
                    if (c.ControllingPlayer is ControlledPlayer)
                    {
                        particleEngine.EmitterLocation = c.Position * tileSize;
                    }
                }
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            car = content.Load<Texture2D>("Car");
            rectangle = content.Load<Texture2D>("Rectangle");
            mainFont = content.Load<SpriteFont>("MainFont");

            List<Texture2D> textures = new List<Texture2D>();
            textures.Add(content.Load<Texture2D>("Circle"));
            particleEngine = new ParticleEngine(textures);
            messageDispatcher.RegisterMessage(MessageType.Collision, delegate (object sender, MessageArgs e) { particleEngine.GenerateCrashParticles(1); });
        }

    }
}
