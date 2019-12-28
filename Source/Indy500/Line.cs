using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Indy500
{
    //Note that this could become a class inheriting polygon..
    struct Line
    {
        public Point StartPoint
        {
            get => _startPoint;
            set
            {
                _startPoint = value;
            }
        }
        public Point EndPoint
        {
            get => _endPoint;
            set
            {
                _endPoint = value;
            }
        }

        public int StartX
        { get => _startPoint.X; set => _startPoint.X = value; }
        public int StartY
        { get => _startPoint.Y; set => _startPoint.Y = value; }
        public int EndX
        { get => _startPoint.X; set => _startPoint.X = value; }
        public int EndY
        { get => _endPoint.Y; set => _endPoint.Y = value; }

        private Point _startPoint;
        private Point _endPoint;

        public Line(int startX, int startY, int endX, int endY)
        {
            _startPoint = new Point(startX, startY);
            _endPoint = new Point(endX, endY);
        }

        public Line(Point start, Point end)
        {
            _startPoint = start;
            _endPoint = end;
        }
    }
}
