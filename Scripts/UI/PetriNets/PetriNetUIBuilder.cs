using System.Collections.Generic;
using Avalonia.Controls;
using VectSharp;
using VectSharp.Canvas;
using VectSharp.Raster.ImageSharp;
using VectSharp.SVG;

namespace GroupProject.UI.PetriNets;

public class PetriNetUIBuilder
{
    private Page visualisation;
    private Graphics visualisationGraphics;
    private Animation animation;
    
    private List<PetriNetNode?> places;
    
    public const double Radius = 12;
    public const double TokenSize = 4;
    
    public const double ScreenWidth = 500;
    public const double ScreenHeight = 500;
    private static Point TopLeft = new Point(0, 0);
    public static Point TopRight = new Point(ScreenWidth, 0);
    public static Point BottomLeft = new Point(0, ScreenHeight);
    public static Point BottomRight = new Point(ScreenWidth, ScreenHeight);
    public static Point Centre = new Point(ScreenWidth / 2, ScreenHeight / 2);
    
    public PetriNetUIBuilder()
    {
        visualisation = new Page(ScreenWidth, ScreenHeight);
        visualisationGraphics = visualisation.Graphics;
        visualisation.Background = Colours.White;
        
        places = new List<PetriNetNode?>();
    }
    
    public void AddPlace(string name, Point position)
    {
        visualisationGraphics.DrawCircle(position, Radius);
        
        // Add text
        Font font = new Font(FontFamily.ResolveFontFamily("Helvetica"), 4);
        Point textPosition = new Point(position.X, (position.Y - Radius * 1.5f));
        visualisationGraphics.FillText(textPosition, name, font, new SolidColourBrush(Colours.Black));
        
        places.Add(new PetriNetNode(name, 0, position.X, position.Y));
    }
    public void AddPlace(string name, double x, double y)
    {
        Point position = new Point(x, y);
        AddPlace(name, position);
    }
    
    public void AddToken(string name, int amount)
    {
        PetriNetNode? place = places.Find(place => place.Name == name);
        
        if (place != null)
        {
            place.Tokens += amount;
        }
        
        UpdateTokenVisual(name);
    }
    public void AddToken(string name)
    {
        AddToken(name, 1);
    }
    
    public void AddEdge(string from, string to)
    {
        PetriNetNode? fromPlace = places.Find(place => place.Name == from);
        PetriNetNode toNetNode = places.Find(place => place.Name == to);
        Point fromPosition = new Point(fromPlace.X + Radius, fromPlace.Y);
        Point toPosition = new Point(toNetNode.X - Radius, toNetNode.Y);
        
        // Check if any petri net nodes intersect with the edge through from - to position
        // If so, draw an arc instead of a straight line

        bool intersecting = IsIntersecting(fromPosition, toPosition);

        if (intersecting)
            visualisationGraphics.DrawArc(fromPosition, toPosition, Colours.Black, lineWidth: 0.5);
        else
            visualisationGraphics.DrawLine(fromPosition, toPosition, Colours.Black, lineWidth: 0.5);
    }
    
    private bool IsIntersecting(Point from, Point to)
    {
        return true;
    }
    
    public void AddTransition(string name, Point position)
    {
        visualisationGraphics.FillRectangle(position, new Size((Radius * 1.8), (Radius * 5)), Colours.Black);
        // Add text
        Font font = new Font(FontFamily.ResolveFontFamily("Helvetica"), 4);
        Point textPosition = new Point(position.X, (position.Y - Radius * 1.5f));
        visualisationGraphics.FillText(textPosition, name, font, new SolidColourBrush(Colours.Black));
    }

    public void ConsumeToken(string name, int amount)
    {
        PetriNetNode? place = places.Find(place => place.Name == name);
        
        if (place != null)
        {
            place.Tokens -= amount;
        }
        
        UpdateTokenVisual(name);
    }
    public void ConsumeToken(string name)
    {
        ConsumeToken(name, 1);
    }
    
    public void MoveToken(string from, string to)
    {
        PetriNetNode fromNetNode = places.Find(place => place.Name == from);
        PetriNetNode toNetNode = places.Find(place => place.Name == to);
        
        fromNetNode.Tokens--;
        toNetNode.Tokens++;

        Frame frame0 = new Frame(visualisationGraphics, 0);
        
        Frame frame1 = new Frame(visualisationGraphics, 1);
        Transition transition0to1 = new Transition(1000);

        animation = new Animation(100, 100, 1);
        animation.AddFrame(frame0);
        animation.AddFrame(frame1, transition0to1);
    }

    private void UpdateTokenVisual(string name)
    {
        // Add the correct amount of circles
        PetriNetNode? place = places.Find(place => place.Name == name);
        
        if (place != null)
        {
            for (int i = 0; i < place.Tokens; i++)
            {
                // Spread token positions out evenly
                double x = place.X + i * Radius / 2;
                double y = place.Y;
                
                var tokenSize = Radius / 4;
                visualisationGraphics.DrawCircleFilled(x, y, tokenSize, Colours.Black);
            }
        }
    }
    
    public Canvas ToCanvas()
    {
        return visualisation.PaintToCanvas();
    }
    public void ToSVG(string filename)
    {
        visualisation.SaveAsSVG("Visuals/" + filename);
    }
    public void ToGIF(string filename)
    {
        animation.SaveAsAnimatedGIF("Visuals/" + filename);
    }
}