using System;
using System.Drawing;
using System.Security.Cryptography.Pkcs;

namespace Draw_Test
{
    class OrthogonalObject
    {
        public Point p1 = new Point();
        public Point p2 = new Point();
        public Point p3 = new Point();
        public OrthogonalObject()
        {
        }

        public OrthogonalObject(Point p1, Point p2, Point p3)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.p3 = p3;
        }
    }
}