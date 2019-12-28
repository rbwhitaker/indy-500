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

        private Particle GenerateNewParticle()
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            Vector2 position = EmitterLocation;
            Vector2 velocity = new Vector2(
                1f * (float)(random.NextDouble() * 2 - 1),
                1f * (float)(random.NextDouble() * 2 - 1));

            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            Color color = new Color(
                (float)random.NextDouble(),
                (float)random.NextDouble(),
                (float)random.NextDouble());
            float size = (float)random.NextDouble();
            int ttl = 20 + random.Next(40);

            return new Particle(texture, position, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Update()
        {
            int total = 10;
            for (int i = 0; i < total; i++)
                particles.Add(GenerateNewParticle());

            for(int particle = 0; particle < particles.Count; particle++)
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
