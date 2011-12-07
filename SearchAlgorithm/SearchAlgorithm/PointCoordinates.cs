using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    class PointCoordinates
    {
        public double x;
        public double y;

        public static double Distance(PointCoordinates point1, PointCoordinates point2)
        {
            return Math.Sqrt(Math.Pow(point1.x - point2.x, 2) + Math.Pow(point1.y - point2.y, 2));
        }
    }
}
