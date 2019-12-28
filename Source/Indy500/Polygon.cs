using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public class Polygon
    {
        public IReadOnlyList<Vector2> Points { get; }
        public Polygon(IEnumerable<Vector2> points)
        {
            Points = points.ToList();
        }

        public BoundingBox BoundingBox => new BoundingBox(new Vector3(Points.Min(p => p.X), Points.Min(p => p.Y), 0), new Vector3(Points.Max(p => p.X), Points.Max(p => p.Y), 0));

        public IEnumerable<LineSegment> Segments
        {
            get
            {
                for (int index = 0; index < Points.Count; index++)
                    yield return new LineSegment(Points[index], Points[(index + 1) % Points.Count]);
            }
        }
    }
}
