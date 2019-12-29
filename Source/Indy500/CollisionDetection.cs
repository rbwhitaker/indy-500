using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Indy500
{
    public static class CollisionDetection
    {
        public static Polygon GetBoundaryFor(Car car)
        {
            return new Polygon(new List<Vector2>
            {
                new Vector2(+car.Size.X / 2, +car.Size.Y / 2),
                new Vector2(+car.Size.X / 2, -car.Size.Y / 2),
                new Vector2(-car.Size.X / 2, -car.Size.Y / 2),
                new Vector2(-car.Size.X / 2, +car.Size.Y / 2)
            }.Select(p => new Vector2(
                p.X * (float)Math.Sin(car.Heading) + p.Y * (float)Math.Cos(car.Heading),
                -p.X * (float)Math.Cos(car.Heading) + p.Y * (float)Math.Sin(car.Heading)))
            .Select(p => p + car.Position));
        }

        public static IEnumerable<GridCell> IntersectedCells(Polygon polygon)
        {
            BoundingBox vehicleBoundingBox = polygon.BoundingBox;
            int minRow = (int)vehicleBoundingBox.Min.Y;
            int maxRow = (int)vehicleBoundingBox.Max.Y + 1;
            int minColumn = (int)vehicleBoundingBox.Min.X;
            int maxColumn = (int)vehicleBoundingBox.Max.X + 1;

            for (int row = minRow; row < maxRow; row++)
                for (int column = minColumn; column < maxColumn; column++)
                    if(Intersects(polygon, CreatePolygonFromCellBoundary(row, column)))
                        yield return new GridCell(row, column);
        }

        private static Polygon CreatePolygonFromCellBoundary(int row, int column)
        {
            return new Polygon(new Vector2(column, row), new Vector2(column, row + 1), new Vector2(column + 1, row + 1), new Vector2(column + 1, row));
        }

        public static bool Intersects(Polygon a, Polygon b) // Assumes convex polygons
        {
            foreach(LineSegment aSegment in a.Segments)
                if (b.Points.All(p => IsOnOutside(aSegment, p))) return false;
            foreach (LineSegment bSegment in b.Segments)
                if (a.Points.All(p => IsOnOutside(bSegment, p))) return false;
            return true;
        }

        public static bool IsOnOutside(LineSegment segment, Vector2 point)
        {
            var value = (segment.End.X - segment.Start.X) * (point.Y - segment.Start.Y) - (segment.End.Y - segment.Start.Y) * (point.X - segment.Start.X);
            return Math.Sign(value) > 0;
        }
    }
}
