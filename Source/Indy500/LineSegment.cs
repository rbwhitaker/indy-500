using Microsoft.Xna.Framework;

namespace Indy500
{
    public class LineSegment
    {
        public Vector2 Start { get; }
        public Vector2 End { get; }
        public Vector2 Midpoint => (Start + End) / 2;

        public LineSegment(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public LineSegment(float startX, float startY, float endX, float endY)
        {
            Start = new Vector2(startX, startY);
            End = new Vector2(endX, endY);
        }

        public bool Intersects(LineSegment other)
        {
            double y1 = Start.Y, y2 = End.Y, y3 = other.Start.Y, y4 = other.End.Y;
            double x1 = Start.X, x2 = End.X, x3 = other.Start.X, x4 = other.End.X;

            double ta = ((y3 - y4) * (x1 - x3) + (x4 - x3) * (y1 - y3)) / ((x4 - x3) * (y1 - y2) - (x1 - x2) * (y4 - y3));
            double tb = ((y1 - y2) * (x1 - x3) + (x2 - x1) * (y1 - y3)) / ((x4 - x3) * (y1 - y2) - (x1 - x2) * (y4 - y3));
            if (ta >= 0 && ta <= 1 && tb >= 0 && tb <= 1) return true;
            return false;
        }
    }
}
