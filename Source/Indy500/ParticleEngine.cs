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
    public class ParticleEngine
    {
        private Random random = new Random();
        public Vector2 EmitterLocation { get; set; }
        private List<Particle> particles = new List<Particle>();
        private List<Texture2D> textures = new List<Texture2D>();

        public ParticleEngine(List<Texture2D> textures)
        {
            this.textures = textures;
        }
        
        public void GenerateCrashParticles(int howMany)
        {
            for(int i = 0; i < howMany; i++)
            {
                Texture2D texture = textures[random.Next(textures.Count)];
                Vector2 position = EmitterLocation;
                Vector2 velocity = new Vector2(
                    1f * (float)(random.NextDouble() * 2 - 1),
                    1f * (float)(random.NextDouble() * 2 - 1));

                float angle = 0;
                float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
                float colorValue = (float)random.NextDouble();
                Color color = new Color(
                    MathHelper.Lerp(0xD4 / 255f, 0x80 / 255f, colorValue),
                    MathHelper.Lerp(0x55 / 255f, 0x33 / 255f, colorValue),
                    MathHelper.Lerp(0x00 / 255f, 0x00 / 255f, colorValue));
                float size = (float)random.NextDouble();
                int ttl = 20 + random.Next(40);


                particles.Add(new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl));
            }
        }

        public void Update()
        {

            for (int particle = 0; particle < particles.Count; particle++)
            {
                particles[particle].Update();
                if(particles[particle].TTL <= 0)
                {
                    particles.RemoveAt(particle);
                    particle--;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            for(int index = 0; index < particles.Count; index++)
                particles[index].Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
