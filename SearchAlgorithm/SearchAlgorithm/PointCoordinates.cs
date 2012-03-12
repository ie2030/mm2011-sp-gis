using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    interface IPointCoordinates
    {

    }

    class PointCoordinates
    {
        public readonly double X;
        public readonly double Y;

        public static double Distance(PointCoordinates point1, PointCoordinates point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public double Distance(PointCoordinates point2)
        {
            return Math.Sqrt(Math.Pow(X - point2.X, 2) + Math.Pow(Y - point2.Y, 2));
        }

        public PointCoordinates(double x, double y)
        {
            if (x == null ||
                y == null)
            {
                throw new ArgumentNullException();
            }
            this.X = x;
            this.Y = y;
        }
    }
}
