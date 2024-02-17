using VectSharp;

namespace GroupProject.UI;

public static class VectorExtensions
{
    public static void DrawLine(this Graphics graphics, Point from, Point to, Colour colour, double lineWidth = 1)
    {
        GraphicsPath path = new GraphicsPath().MoveTo(from).LineTo(to);
        graphics.StrokePath(path, colour, lineWidth);
    }
    public static void DrawLine(this Graphics graphics, double fromX, double fromY, double toX, double toY, Colour colour, double lineWidth = 1)
    {
        Point from = new Point(fromX, fromY);
        Point to = new Point(toX, toY);
        
        GraphicsPath path = new GraphicsPath().MoveTo(from).LineTo(to);
        graphics.StrokePath(path, colour, lineWidth);
    }
    
    public static void DrawArc(this Graphics graphics, Point from, Point to, Colour colour, double lineWidth = 1)
    {
        Point centre = new Point((from.X + to.X) / 2, (from.Y + to.Y) / 2);

        GraphicsPath path = new GraphicsPath().MoveTo(from).Arc(centre, 10, 5, 5);
        graphics.StrokePath(path, colour, lineWidth);
    }
    
    public static void DrawCircle(this Graphics graphics, Point centre, double radius)
    {
        GraphicsPath path = new GraphicsPath();

        double startAngle = 0;
        double endAngle = 7;

        path.Arc(centre, radius, startAngle, endAngle);

        graphics.FillPath(path, Colours.Green);
        graphics.StrokePath(path, Colours.Black, lineWidth: 0.5);
    }
    public static void DrawCircle(this Graphics graphics, double x, double y, double radius)
    {
        GraphicsPath path = new GraphicsPath();
        Point centre = new Point(x, y);

        double startAngle = 0;
        double endAngle = 7;

        path.Arc(centre, radius, startAngle, endAngle);

        graphics.FillPath(path, Colours.Green);
        graphics.StrokePath(path, Colours.Black, lineWidth: 0.5);
    }
    
    public static void DrawCircleFilled(this Graphics graphics, Point centre, double radius, Colour colour)
    {
        GraphicsPath path = new GraphicsPath();

        double startAngle = 0;
        double endAngle = 7;

        path.Arc(centre, radius, startAngle, endAngle);

        graphics.FillPath(path, colour);
    }
    public static void DrawCircleFilled(this Graphics graphics, double x, double y, double radius, Colour colour)
    {
        GraphicsPath path = new GraphicsPath();
        Point centre = new Point(x, y);

        double startAngle = 0;
        double endAngle = 7;

        path.Arc(centre, radius, startAngle, endAngle);

        graphics.FillPath(path, colour);
    }
}