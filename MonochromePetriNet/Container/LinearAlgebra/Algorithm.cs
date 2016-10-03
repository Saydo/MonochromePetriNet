using System;
using System.Drawing;

namespace MonochromePetriNet.Container.LinearAlgebra
{
    public static class Algorithm
    {
        public static void ExpandLine(Point center, ref Point p, double extent)
        {
            int dx = p.X - center.X;
            int dy = p.Y - center.Y;
            double scaleFactor = extent / Math.Sqrt(dx * dx + dy * dy);
            p.X += (int)(scaleFactor * dx);
            p.Y += (int)(scaleFactor * dy);
        }

        public static void ResizeLine(Point center, ref Point p, double length)
        {
            int dx = p.X - center.X;
            int dy = p.Y - center.Y;
            if ((dx == 0) && (dy == 0))
            {
                return;
            }
            double scaleFactor = length / Math.Sqrt(dx * dx + dy * dy);
            p.X = center.X + (int)(scaleFactor * dx);
            p.Y = center.Y + (int)(scaleFactor * dy);
        }

        public static double GetTriangleCos(double a, double b, double c)
        {
            return (b * b + c * c - a * a) / (2 * b * c);
        }

        public static Point[] GetLineBorder(Point p1, Point p2, int extent)
        {
            Point[] lineBorder = new Point[4];
            Equation eq1 = new Equation(p1, p2);
            Equation eq2 = eq1.GetNormalEquation(p1);
            Equation eq3 = eq1.GetNormalEquation(p2);
            if (((p2.X >= p1.X) && (p2.Y >= p1.Y)) || ((p2.X < p1.X) && (p2.Y < p1.Y)))
            {
                lineBorder[0] = eq2.GetPoint(p1, -extent);
                lineBorder[1] = eq3.GetPoint(p2, -extent);
                lineBorder[2] = eq3.GetPoint(p2, extent);
                lineBorder[3] = eq2.GetPoint(p1, extent);
            }
            else
            {
                lineBorder[0] = eq2.GetPoint(p1, extent);
                lineBorder[1] = eq3.GetPoint(p2, extent);
                lineBorder[2] = eq3.GetPoint(p2, -extent);
                lineBorder[3] = eq2.GetPoint(p1, -extent);
            }
            return lineBorder;
        }

        public static int MinX(Point[] points)
        {
            if (points.Length > 0)
            {
                int minValue = points[0].X;
                for (int i = 1; i < points.Length; ++i)
                {
                    if (points[i].X < minValue)
                    {
                        minValue = points[i].X;
                    }
                }
                return minValue;
            }
            return int.MinValue;
        }

        public static int MinY(Point[] points)
        {
            if (points.Length > 0)
            {
                int minValue = points[0].Y;
                for (int i = 1; i < points.Length; ++i)
                {
                    if (points[i].Y < minValue)
                    {
                        minValue = points[i].Y;
                    }
                }
                return minValue;
            }
            return int.MinValue;
        }

        public static int MaxX(Point[] points)
        {
            if (points.Length > 0)
            {
                int maxValue = points[0].X;
                for (int i = 1; i < points.Length; ++i)
                {
                    if (points[i].X > maxValue)
                    {
                        maxValue = points[i].X;
                    }
                }
                return maxValue;
            }
            return int.MinValue;
        }

        public static int MaxY(Point[] points)
        {
            if (points.Length > 0)
            {
                int maxValue = points[0].Y;
                for (int i = 1; i < points.Length; ++i)
                {
                    if (points[i].Y > maxValue)
                    {
                        maxValue = points[i].Y;
                    }
                }
                return maxValue;
            }
            return int.MinValue;
        }

        public static Point GetNormalToLine(Point p1, Point p2, int length)
        {
            Equation eq1 = new Equation(p1, p2);
            Equation eq2 = eq1.GetNormalEquation(p2);
            return eq2.GetPoint(p2, length);
        }
    }
}
