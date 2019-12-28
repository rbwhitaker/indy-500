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
        private DefaultBackgroundMusicPlaybackService music;

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
                Color carColor = c == race.Cars[0] ? Color.Red : Color.Blue;
                Vector2 centerPoint = new Vector2(car.Width / 2f, car.Height / 2f);
                Vector2 scale = new Vector2(1f / car.Width * tileSize * c.Size.X, 1f / car.Height * tileSize * c.Size.Y);
                spriteBatch.Draw(car, c.Position * tileSize, null, carColor, c.Heading, centerPoint, scale, SpriteEffects.None, 0);
            }
            spriteBatch.End();
        }

        public void LoadContent(GraphicsDevice graphicsDevice, ContentManager content)
        {
            spriteBatch = new SpriteBatch(graphicsDevice);
            car = content.Load<Texture2D>("Car");
            rectangle = content.Load<Texture2D>("Rectangle");
            music = new DefaultBackgroundMusicPlaybackService(content);
            System.Random rand = new System.Random();
            int SongNumber = rand.Next(15) + 1;
            music.StartBackgroundMusic(SongNumber.ToString());
        }
    }
}
