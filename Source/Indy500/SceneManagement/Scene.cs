using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Indy500.SceneManagement
{
    public interface IScene
    {
        void Update(GameTime gameTime);
        void Draw(GameTime gameTime);
        void LoadContent(GraphicsDevice graphicsDevice, ContentManager content);
    }
}
