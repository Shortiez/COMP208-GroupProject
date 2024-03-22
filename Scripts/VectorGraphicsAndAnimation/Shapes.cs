using System;
using VectSharp;

namespace GroupProject.VectorGraphicsAndAnimation;

public static class Shapes
{
    public static void Rectangle(this Graphics graphics, Point origin, Size size, Brush colour)
    {
        graphics.FillRectangle(origin, size, colour);
    }
    public static void Rectangle(this Graphics graphics, Point origin, double width, double height, Brush colour)
    {
        Size size = new Size(width, height);
        graphics.FillRectangle(origin, size, colour);
    }
    
    public static void Circle(this Graphics graphics, Point centre, double radius, Brush colour)
    {
        GraphicsPath path = new GraphicsPath();
        path.Arc(centre, radius, 0, 2*Math.PI);
        
        graphics.FillPath(path, colour);
    }
    public static void Circle(this Graphics graphics, Point centre, double radius, double outlineWidth, Brush colour)
    {
        GraphicsPath path = new GraphicsPath();
        path.Arc(centre, radius, 0, 2*Math.PI);
        
        graphics.FillPath(path, colour);
        graphics.StrokePath(path, colour, outlineWidth);
    }
    
    public static void Triangle(this Graphics graphics, Point a, Point b, Point c, Brush colour)
    {
        GraphicsPath path = new GraphicsPath();
        path.MoveTo(a).LineTo(b).LineTo(c).Close();
        
        graphics.FillPath(path, colour);
    }
    public static void Triangle(this Graphics graphics, Point a, Point b, Point c, double outlineWidth, Brush colour)
    {
        GraphicsPath path = new GraphicsPath();
        path.MoveTo(a).LineTo(b).LineTo(c).Close();
        
        graphics.FillPath(path, colour);
        graphics.StrokePath(path, colour, outlineWidth);
    }
}