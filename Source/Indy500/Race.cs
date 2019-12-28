using Microsoft.Xna.Framework;
using System.Collections.Generic;
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

        }
    }
}
