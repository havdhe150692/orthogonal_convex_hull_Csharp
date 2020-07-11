# Orthogonal Convex Hull/ C# #
Group Assignment for MAD101, Orthogonal Convex Hull.

FPT University, Hanoi, Vietnam. July, 2020

------------

This is a program made for drawing the Orthogonal Convex Hull of the 100 randomly-generated points within a given-sized box.

# Tech

Window Form Application.

Written in C#.

IDE used: Visual Studio Preview 2019.

# Version
## 1.00 

Date: 11/7/2020

First attempt of uploading the source code to GitHub.

* Works perfectly in general cases. Under developement for special cases and for fitting the definition from **On the Definition and Computation of Rectilinear Convex Hulls, Thomas Ottmann (1984)* **

# Manual

Run the Draw Test.exe in the Program folder to get an instance of the program.

To see the source code, open Form1.cs file in your prefered editor or click the Form1.cs file in Visual Studio and press F7. This is the place where all the algorithm stays. Program.cs is computer-generated file of which the purpose is simply runs the Form1. OrthogonalObject.cs is an unused class. There is also a lot of unsused code in Form1.cs, for further development. Simply ignore them if you don't want to see.

To change the input, expands the Property to change the number of given dots and the size of the box (the region where the dots spawn).


# The Algorithm

The way I do is quite simple. Break this program into 2 parts: Find & Draw:

## Find

Given a group of dots like this


![Alt text](https://github.com/havdhe150692/pictures/blob/master/1.png?raw=true)


Let's take a very simple approach into this because (sadly) the definition from Wikipedia might be too advanced for me. I search Google for a lot of pictures to at least visualize what would a Convex Hull, with Orthogonal lines, look like. I found this.


![Alt text](https://upload.wikimedia.org/wikipedia/commons/thumb/e/e0/Orthogonal-convex-hull.svg/220px-Orthogonal-convex-hull.svg.png)


So describing it, by commoners' language, I will have **"A set of outer dots that connects to each other by orthogonal shape, which also has the *concave* corner be closer to the highest/ lowest point"**. This, in fact, seems to be less **convex** than the original Convex Hull problem, as the "convex" refers to  the geometrically natural shape: there can no set of 3 adjacent point can form a less than 180 degree corner.

In this problem, things are different. You might think I just have to replace every straight lines in the Original Convex Hull problem by a right triangle. That can create stuff like this:

![Alt text](https://github.com/havdhe150692/pictures/blob/master/2.png)

So the way we do this problem must be changed. But it's may not be too hard to do. Now, instead of finding a way to make sure it fits that 3-dots rule, we just have to make sure the dot we are going to connect from a specific dot is **possible to create a right triangle without leaving any dot outside**. 

*Take a note that due to the nature of the Bitmap processing in the WinForm Application I use, the axises is quite different. **The coordinate origin is the top left point of the graph, to the left is positive X, straight down is positive Y.***

![Alt text](https://github.com/havdhe150692/pictures/blob/master/5.png)

Firstly, I will find 4 special dots in this big set. They are those dots **at the highest position**, **lowest position**, **furthest to the left and to the right**. I will make a program which search, form the furthest dot at the right side of the set, to the left, for a dot it can make a good right triangle with. And that be the nearest dot possible with it in term of horizontal distance (X), but to keep it controllable, it's must be (this time) lower than the dot it started from (Y). Like this.

![Alt text](https://github.com/havdhe150692/pictures/blob/master/3.png)

Repeat this until we got to the lowerest dot.


![Alt text](https://github.com/havdhe150692/pictures/blob/master/4.png)



I call this the Bottom Right side. We can look at this as some C#. p is the inserted dot, where we start from. pointsArray is the total set of these dots, which is converted into an array. Searching from the X value of p, to the left until it reach the border where we know certainly that there can be no dot there. Also, the Y value of the next dot must be higher than the previous dot. If we've found one, end and return the dot. Repeat inserting the dots until it've reach the Lowest Dot.
```
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
```


Therefore, by doing this at all 4 sides of the hull, we get the final result is a list which contains all the dot satisfy the condition. Time complexity of the program will be ```O(Border Size * Point Set Size)```

## Draw

Given you found the First Dot and The Next Dot you want it to connect to. Create a ghost dot which has First Dot's Y and The Next Dot's X - we want it's concave corner to be closer to the highest/ lowest dot. Reverse to the Dot's corresponding X and Y if doing at the Left side

The Drawing is done by C#'s System.Drawing. On run, the WinForm App triggers the **PaintEventArgs**, so that it can draw to the program. The **PaintsEventArgs e** throughout the code are for this purpose.

Some pictures of the results with a set of 50, 100 dots.

![Alt text](https://github.com/havdhe150692/pictures/blob/master/6.png) ![Alt text](https://github.com/havdhe150692/pictures/blob/master/7.png)

Because the *Draw* part of this program is only just that, it creates some problem.

## Drawbacks and Unfinised Problems

The drawing's job is just draw an orthogonal shape. End. **In most cases (general case) where the amount of dots is large enough so the program we run doesn't seem to be wrong, the way these dots form make something like a circle. There are Highest Dot, Lowest Dot, Left Dot and Right Dot separately.** *But what will happen if we insert a set whose the Lowest Dot is also the Left Dot?*

![Alt text](https://github.com/havdhe150692/pictures/blob/master/8.png) 

So I trace back to the definition of an Orthogonal Convex Hull and find these.

![Alt text](https://github.com/havdhe150692/pictures/blob/master/9.png)
> Ottmann, T., Soisalon-Soininen, E., & Wood, D. (1984). On the definition and computation of rectilinear convex hulls.

> https://sci-hub.tw/10.1016/0020-0255(84)90025-2

The drawing program is too simple, it cannot draw zig-zag lines or cutting at the middle if it realize it's crossing some other line. This part is quite tricky and still under development. My nearest attempt is trying to create a class for recognizing the intersect point of two line secment.

# Conclusion

This program successed at finding all the outer dots for creating a Orthogonal Convex Hull and drawing it in the general cases. Though, it failed at mimic-ing the exact definition from *On the definition and computation of rectilinear convex hulls. (1984)*, which shows at some special cases.
