using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server
{
    abstract class AbstractCoordinates
    {
        public abstract double Distance(PointCoordinates point2);
    }

    class PointCoordinates : AbstractCoordinates
    {
        private readonly double X;
        private readonly double Y;

        public static double Distance(PointCoordinates point1, PointCoordinates point2)
        {
            return Math.Sqrt(Math.Pow(point1.X - point2.X, 2) + Math.Pow(point1.Y - point2.Y, 2));
        }

        public override double Distance(PointCoordinates point2)
        {
            return Math.Sqrt(Math.Pow(X - point2.X, 2) + Math.Pow(Y - point2.Y, 2));
        }

        public PointCoordinates(double x, double y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
