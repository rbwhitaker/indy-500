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
                new Vector2(-car.Size.X / 2, +car.Size.Y / 2),
                new Vector2(-car.Size.X / 2, -car.Size.Y / 2),
                new Vector2(+car.Size.X / 2, -car.Size.Y / 2)
            }.Select(p => new Vector2(p.X * (float)Math.Cos(car.Heading) + p.Y * (float)Math.Sin(car.Heading)))
            .Select(p => p + car.Position));
        }

        public static IEnumerable<GridCell> IntersectedCells(Polygon polygon)
        {
            BoundingBox vehicleBoundingBox = polygon.BoundingBox;
            int minRow = (int)vehicleBoundingBox.Min.Y;
            int maxRow = (int)vehicleBoundingBox.Max.Y + 1;
            int minColumn = (int)vehicleBoundingBox.Min.X;
            int maxColumn = (int)vehicleBoundingBox.Max.X + 1;

            for (int row = minRow; row <= maxRow; row++)
                for (int column = minColumn; column < maxColumn; column++)
                    yield return new GridCell(row, column);
        }
    }
}
