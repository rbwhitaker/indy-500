using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Indy500
{
    public interface IRenderer
    {
        void LoadContent(GraphicsDevice graphicsDevice, ContentManager content);
        void Update(GameTime gameTime);
        void Draw(Race race, GameTime gameTime);
    }
}
