﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indy500
{
    public class Simple2DRenderer : IRenderer
    {
        private Texture2D car;
        private Texture2D rectangle;
        private float tileSize = 20;
        private SpriteBatch spriteBatch;
        
        public void Draw(Race race, GameTime gameTime)
        {
            spriteBatch.Begin();

            for(int row = 0; row < race.Track.Rows; row++)
            {
                for(int column = 0; column < race.Track.Columns; column++)
                {
                    spriteBatch.Draw(rectangle, new Rectangle((int)(column * tileSize), (int)(row * tileSize), (int)tileSize, (int)tileSize), race.Track[row, column] == TrackTileType.Dirt ? Color.Green : Color.Gray);
                }
            }

            foreach(Car c in race.Cars)
            {
                spriteBatch.Draw(car, c.Position * tileSize, null, c == race.Cars[0] ? Color.Red : Color.Blue, c.Heading, new Vector2(car.Width / 2f, car.Height / 2f), 1f / car.Width * tileSize * 2, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            car = content.Load<Texture2D>("Car");
            rectangle = content.Load<Texture2D>("Rectangle");
        }
    }
}
