using System;
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
        private float tileSize = 10;
        private SpriteBatch spriteBatch;
        
        public void Draw(Race race, GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(car, race.Cars[0].Position * tileSize, null, Color.Red, race.Cars[0].Heading, new Vector2(car.Width / 2f, car.Height / 2f), (1f/car.Width)*tileSize*2, SpriteEffects.None, 0);
            spriteBatch.End();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            car = content.Load<Texture2D>("Car"); 
        }
    }
}
