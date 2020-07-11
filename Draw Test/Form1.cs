using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Draw_Test
{
    public partial class Form1 : Form
    {
        #region Property
        public static int sizeOfArray = 100;
        int TopBorderY = 100;
        int LeftBorderX = 100;
        int BottomBorderY = 800; 
        int RightBorderX = 800;
        
        Point Base = new Point(0, 0);
        Point[] pointsArray = new Point[sizeOfArray];
        List<Point> pointListTopLeft = new List<Point>();
        List<Point> pointListTopRight = new List<Point>();
        List<Point> pointListBottomLeft = new List<Point>();
        List<Point> pointListBottomRight = new List<Point>();
        Point[] pointArrayTopLeft = new Point[1];
        Point[] pointArrayTopRight = new Point[1];
        Point[] pointArrayBottomLeft = new Point[1];
        Point[] pointArrayBottomRight = new Point[1];

        #endregion  

        public Form1()
        {   
            InitializeComponent();
        }

        private void PointsArray(PaintEventArgs e)
        {
            var rand = new Random();
            for (int i = 0; i < sizeOfArray; i++)
            {
                pointsArray[i].X = rand.Next(LeftBorderX, RightBorderX);
                pointsArray[i].Y = rand.Next(TopBorderY, BottomBorderY);
                CreateDots(pointsArray[i], e);
            }
                pointListTopLeft = TopLeftDotsSetUp(pointListTopLeft);
                ListToArrayTopLeft(pointArrayTopLeft);

                pointListTopRight = TopRightDotsSetUp(pointListTopRight);
                ListToArrayTopRight(pointArrayTopRight);

                pointListBottomLeft = BottomLeftDotsSetUp(pointListBottomLeft);
                ListToArrayBottomLeft(pointArrayBottomLeft);

                pointListBottomRight = BottomRightDotsSetUp(pointListBottomRight);
                ListToArrayBottomRight(pointArrayBottomRight);


                ListToArrayAndDrawTopSide(pointArrayTopLeft, e);

                ListToArrayAndDrawTopSide(pointArrayTopRight, e);

                ListToArrayAndDrawBottomSide(pointArrayBottomLeft, e);

                ListToArrayAndDrawBottomSide(pointArrayBottomRight, e);
        }
        #region Methods

        #region Draw Orthogonal from the Array List

        private Point[] ListToArrayTopLeft(Point[] pointArrayX)
        {
            pointArrayTopLeft = pointListTopLeft.ToArray();
            return pointArrayTopLeft;
        }

        private Point[] ListToArrayTopRight(Point[] pointArrayX)
        {
            pointArrayTopRight = pointListTopRight.ToArray();
            return pointArrayTopRight;
        }

        private Point[] ListToArrayBottomLeft(Point[] pointArrayX)
        {
            pointArrayBottomLeft = pointListBottomLeft.ToArray();
            return pointArrayBottomLeft;
        }

        private Point[] ListToArrayBottomRight(Point[] pointArrayX)
        {
            pointArrayBottomRight = pointListBottomRight.ToArray();
            return pointArrayBottomRight;
        }

        private void ListToArrayAndDrawTopSide(Point[] pointListArray, PaintEventArgs e)
        {
            int sizeOfArrayList = pointListArray.Length;
            for (int i = 0; i < sizeOfArrayList - 1; i++)
            { 
                 DrawOrthogonal(pointListArray[i], pointListArray[i + 1], FindTopDot(pointsArray), e);
            }
            #region Unused Code
            /* if (Point.Equals(FindTopDot(pointsArray), FindLeftDot(pointsArray)))
            {
                DrawOrthogonalSpecialCaseUp(pointArrayTopRight[pointArrayTopRight.Length - 2],
                                          pointArrayTopRight[pointArrayTopRight.Length - 1],
                                          pointArrayBottomLeft[pointArrayBottomLeft.Length - 2],
                                          pointArrayBottomLeft[pointArrayBottomLeft.Length - 1],
                                          e);
            }
            else if (Point.Equals(FindTopDot(pointsArray), FindRightDot(pointsArray)))
            {
                DrawOrthogonalSpecialCaseUp(pointArrayTopLeft[pointArrayTopLeft.Length - 2],
                                          pointArrayTopLeft[pointArrayTopLeft.Length - 1],
                                          pointArrayBottomRight[pointArrayBottomRight.Length - 2],
                                          pointArrayBottomRight[pointArrayBottomRight.Length - 1],
                                          e);
            }
            else
            {
            DrawOrthogonal(pointListArray[sizeOfArrayList - 2],
                           pointListArray[sizeOfArrayList - 1],
                           FindTopDot(pointsArray), e);
            } */
            #endregion
        }

        private void ListToArrayAndDrawBottomSide(Point[] pointListArray, PaintEventArgs e)
        {
            int sizeOfArrayList = pointListArray.Length;
            for (int i = 0; i < sizeOfArrayList - 1; i++)
            {
                 DrawOrthogonal(pointListArray[i], pointListArray[i + 1], FindBottomDot(pointsArray), e);
            }
            #region Unused Code
            /* if (Point.Equals(FindBottomDot(pointsArray), FindLeftDot(pointsArray)))
            {
                DrawOrthogonalSpecialCaseDown(pointArrayTopLeft[pointArrayTopLeft.Length - 2],
                                          pointArrayTopLeft[pointArrayTopLeft.Length - 1],
                                          pointArrayBottomRight[pointArrayBottomRight.Length - 2],
                                          pointArrayBottomRight[pointArrayBottomRight.Length - 1],
                                          
                                          e);
            }
            if (Point.Equals(FindBottomDot(pointsArray), FindRightDot(pointsArray)))
            {
                DrawOrthogonalSpecialCaseDown(pointArrayTopRight[pointArrayTopRight.Length - 2],
                                          pointArrayTopRight[pointArrayTopRight.Length - 1],
                                          pointArrayBottomLeft[pointArrayBottomLeft.Length - 2],
                                          pointArrayBottomLeft[pointArrayBottomLeft.Length - 1],                                                                      
                                          e);
            }
            else
            {
                DrawOrthogonal(pointListArray[sizeOfArrayList - 2],
                               pointListArray[sizeOfArrayList - 1],
                               FindBottomDot(pointsArray), e);
            } */
            #endregion
        }
        #endregion

        #region Setup 4 List
        private List<Point> TopLeftDotsSetUp(List<Point> pointListArray)
        {
            Point LeftDot = FindLeftDot(pointsArray);
            Point TopDot = FindTopDot(pointsArray);
            Point BufferDot = LeftDot;
            pointListTopLeft.Add(BufferDot);
            while (!Point.Equals(BufferDot, TopDot))
            {
                BufferDot = SearchForTheNextDotsTopLeft(BufferDot, pointsArray);
                pointListTopLeft.Add(BufferDot);
            }
            return pointListArray;
        }

        private List<Point> TopRightDotsSetUp(List<Point> pointListArray)
        {
            Point RightDot = FindRightDot(pointsArray);
            Point TopDot = FindTopDot(pointsArray);
            Point BufferDot = RightDot;
            pointListTopRight.Add(BufferDot);
            while (!Point.Equals(BufferDot, TopDot))
            {
                BufferDot = SearchForTheNextDotsTopRight(BufferDot, pointsArray);
                pointListTopRight.Add(BufferDot);
            }
            return pointListArray;
        }

        private List<Point> BottomLeftDotsSetUp(List<Point> pointListArray)
        {
            Point LeftDot = FindLeftDot(pointsArray);
            Point BottomDot = FindBottomDot(pointsArray);
            Point BufferDot = LeftDot;
            pointListBottomLeft.Add(BufferDot);
            while (!Point.Equals(BufferDot, BottomDot))
            {
                BufferDot = SearchForTheNextDotsBottomLeft(BufferDot, pointsArray);
                pointListBottomLeft.Add(BufferDot);
            }
            return pointListArray;
        }

        private List<Point> BottomRightDotsSetUp(List<Point> pointListArray)
        {
            Point RightDot = FindRightDot(pointsArray);
            Point BottomDot = FindBottomDot(pointsArray);
            Point BufferDot = RightDot;
            pointListBottomRight.Add(BufferDot);
            while (!Point.Equals(BufferDot, BottomDot))
            {
                BufferDot = SearchForTheNextDotsBottomRight(BufferDot, pointsArray);
                pointListBottomRight.Add(BufferDot);
            }
            return pointListArray;
        }
        #endregion

        #region Search for the next dots basic method
        private Point SearchForTheNextDotsTopRight(Point p, Point[] pointsArray)
        {
            Point PointNext = p;
            for (int j = p.X; j >= LeftBorderX; j--)
            {
                for (int i = 0; i < sizeOfArray; i++)
                {
                    if ((pointsArray[i].X == j) &&
                        ((pointsArray[i].Y) <= p.Y))
                    {
                        PointNext = pointsArray[i];   
                    }
                    if (!Point.Equals(PointNext, p))
                    {
                        goto endOfLoop;
                    }
                }
            }
            endOfLoop:
            return PointNext;    
        }   

        private Point SearchForTheNextDotsTopLeft(Point p, Point[] pointsArray)
        {
            Point PointNext = p;
            for (int j = p.X; j <= RightBorderX; j++)
            {
                for (int i = 0; i < sizeOfArray; i++)
                {
                    if ((pointsArray[i].X == j) &&
                        ((pointsArray[i].Y) <= p.Y))
                    {
                        PointNext = pointsArray[i];
                    }
                    if (!Point.Equals(PointNext, p))
                    {
                        goto endOfLoop;
                    }
                }
            }
            endOfLoop:
            return PointNext;
        }

        private Point SearchForTheNextDotsBottomRight(Point p, Point[] pointsArray)
        {
            Point PointNext = p;
            for (int j = p.X; j >= LeftBorderX; j--)
            {
                for (int i = 0; i < sizeOfArray; i++)
                {
                    if ((pointsArray[i].X == j) &&
                        ((pointsArray[i].Y) >= p.Y))
                    {
                        PointNext = pointsArray[i];
                    }
                    if (!Point.Equals(PointNext, p))
                    {
                        goto endOfLoop;
                    }
                }
            }
            endOfLoop:
            return PointNext;
        }

        private Point SearchForTheNextDotsBottomLeft(Point p, Point[] pointsArray)
        {
            Point PointNext = p;
            for (int j = p.X; j <= RightBorderX; j++)
            {
                for (int i = 0; i < sizeOfArray; i++)
                {
                    if ((pointsArray[i].X == j) &&
                        ((pointsArray[i].Y) >= p.Y))
                    {
                        PointNext = pointsArray[i];
                    }
                    if (!Point.Equals(PointNext, p))
                    {
                        goto endOfLoop;
                    }
                }
            }
            endOfLoop:
            return PointNext;
        }
        #endregion
    
        #region Find 4 base dots
        private Point FindTopDot(Point[] pointsArray)
        {
            Point TopDot = pointsArray[0];
            for (int i = 0; i < sizeOfArray; i++)
            {
                if (pointsArray[i].Y < TopDot.Y)
                {
                    TopDot = pointsArray[i];
                }  
            }
            return TopDot;
        }

        private Point FindRightDot(Point[] pointsArray)
        {
            Point RightDot = pointsArray[0];
            for (int i = 0; i < sizeOfArray; i++)
            {
                if (pointsArray[i].X > RightDot.X)
                {
                    RightDot = pointsArray[i];
                }    
            }
            return RightDot;
        }

        private Point FindBottomDot(Point[] pointsArray)
        {
            Point BottomDot = pointsArray[0];
            for (int i = 0; i < sizeOfArray; i++)
            {
                if (pointsArray[i].Y > BottomDot.Y)
                {
                    BottomDot = pointsArray[i];
                }
            }
            return BottomDot;
        }

        private Point FindLeftDot(Point[] pointsArray)
        {
            Point LeftDot = pointsArray[0];
            for (int i = 0; i < sizeOfArray; i++)
            {
                if (pointsArray[i].X < LeftDot.X)
                {
                    LeftDot = pointsArray[i];
                }
            }
            return LeftDot;
        }
        #endregion

        #region Drawing basic method
        private void CreateDots(Point p, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Red, 5);
            int width = 3;
            int height = 3;
            int pointXform = p.X - width / 2;
            int pointYform = p.Y - height / 2;
            Rectangle r = new Rectangle(pointXform, pointYform, width, height);
            g.DrawEllipse(pen, r);
            g.FillEllipse(Brushes.Red, r);
        }

        private void DrawOrthogonal(Point p1, Point p2, Point flagpoint, PaintEventArgs e)
        {
            if ((Math.Abs(p1.X - flagpoint.X) <
                (Math.Abs(p2.X - flagpoint.X))))
            {
                Point p3 = new Point(p1.X, p2.Y);
                CreateLines(p1, p3, e);
                CreateLines(p2, p3, e);
            }
            else
            {
                Point p3 = new Point(p2.X, p1.Y);
                CreateLines(p1, p3, e);
                CreateLines(p2, p3, e);
            }
        }

        private void DrawOrthogonalSpecialCaseUp(Point Ap1, Point Ap2, Point Bp1, Point Bp2, PaintEventArgs e)
        {
                Point Ap3 = new Point(Ap1.X, Ap2.Y);
                OrthogonalObject Ap1p3p2 = new OrthogonalObject(Ap1, Ap3, Ap2);
                Point Bp3 = new Point(Bp2.X, Bp1.Y);
                OrthogonalObject Bp1p3p2 = new OrthogonalObject(Bp1, Bp3, Bp2);
                Point crosspoint = CrossObject(Ap1p3p2, Bp1p3p2);
                CreateLines(Ap1, Ap3, e);
                CreateLines(Ap3, Ap2, e);
                CreateLines(Bp1, crosspoint, e);
        }

        private void DrawOrthogonalSpecialCaseDown(Point Ap1, Point Ap2, Point Bp1, Point Bp2, PaintEventArgs e)
        {
            Point Ap3 = new Point(Ap1.X, Ap2.Y);
            OrthogonalObject Ap1p3p2 = new OrthogonalObject(Ap1, Ap3, Ap2);
            Point Bp3 = new Point(Bp2.X, Bp1.Y);
            OrthogonalObject Bp1p3p2 = new OrthogonalObject(Bp1, Bp3, Bp2);
            Point crosspoint = CrossObject(Ap1p3p2, Bp1p3p2);
            CreateLines(Ap1, Ap3, e);
            CreateLines(Ap3, Ap2, e);
            CreateLines(Bp1, crosspoint, e);
        }

        private void CreateLines(Point p1, Point p2, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Blue, 3);
            g.DrawLine(pen, p1, p2);
        }

        private void DrawBox(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Black, 1);
            Point a = new Point(LeftBorderX, TopBorderY);
            Point b = new Point(RightBorderX, TopBorderY);
            Point c = new Point(LeftBorderX, BottomBorderY);
            Point d = new Point(RightBorderX, BottomBorderY);
            g.DrawLine(pen, a, b);
            g.DrawLine(pen, b, d);
            g.DrawLine(pen, c, d);
            g.DrawLine(pen, c, a);
        }

        #endregion

        #region Special cross-segment calculating (Unused)
        /*
        static Boolean onSegment(Point p, Point q, Point r)
        {
            if (q.X <= Math.Max(p.X, r.X) && q.X >= Math.Min(p.X, r.X) &&
                q.Y <= Math.Max(p.Y, r.Y) && q.Y >= Math.Min(p.Y, r.Y))
                return true;

            return false;
        }

        // To find orientation of ordered triplet (p, q, r). 
        // The function returns following values 
        // 0 --> p, q and r are colinear 
        // 1 --> Clockwise 
        // 2 --> Counterclockwise 
        static int orientation(Point p, Point q, Point r)
        {
            // See https://www.geeksforgeeks.org/orientation-3-ordered-points/ 
            // for details of below formula. 
            int val = (q.Y - p.Y) * (r.X - q.X) -
                    (q.Y - p.Y) * (r.Y - q.Y);

            if (val == 0) return 0; // colinear 

            return (val > 0) ? 1 : 2; // clock or counterclock wise 
        }

        // The main function that returns true if line segment 'p1q1' 
        // and 'p2q2' intersect. 
        static Boolean doIntersect(Point p1, Point q1, Point p2, Point q2)
        {
            // Find the four orientations needed for general and 
            // special cases 
            int o1 = orientation(p1, q1, p2);
            int o2 = orientation(p1, q1, q2);
            int o3 = orientation(p2, q2, p1);
            int o4 = orientation(p2, q2, q1);

            // General case 
            if (o1 != o2 && o3 != o4)
                return true;

            // Special Cases 
            // p1, q1 and p2 are colinear and p2 lies on segment p1q1 
            if (o1 == 0 && onSegment(p1, p2, q1)) return true;

            // p1, q1 and q2 are colinear and q2 lies on segment p1q1 
            if (o2 == 0 && onSegment(p1, q2, q1)) return true;

            // p2, q2 and p1 are colinear and p1 lies on segment p2q2 
            if (o3 == 0 && onSegment(p2, p1, q2)) return true;

            // p2, q2 and q1 are colinear and q1 lies on segment p2q2 
            if (o4 == 0 && onSegment(p2, q1, q2)) return true;

            return false; // Doesn't fall in any of the above cases 
        }

        static Point CrossObject(OrthogonalObject Obj1, OrthogonalObject Obj2)
        {
            Point crosspoint = new Point();
            if (Point.Equals(Obj1.p2, Obj2.p2) &&
               (doIntersect(Obj1.p1, Obj1.p3, Obj2.p1, Obj2.p3)))
            {
                crosspoint = new Point(Obj1.p1.X, Obj2.p2.Y);
            }
            return crosspoint;
        }
        #endregion
        */
        #endregion
        private void Form1_Paint(object sender, PaintEventArgs e)   
        {
            Graphics g = e.Graphics;
            DrawBox(e);
            PointsArray(e);
        }

    }
}
