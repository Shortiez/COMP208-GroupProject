using System;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Media;
using GroupProject.UI.PetriNets;
using VectSharp;

namespace GroupProject;

public partial class MainWindow : Window
{
    PetriNetUIBuilder petriNetUIBuilder = new PetriNetUIBuilder();
    public MainWindow()
    {
        InitializeComponent();
    }

    private void StyledElement_OnInitialized(object? sender, EventArgs e)
    {
        petriNetUIBuilder.AddPlace("P1", 100, 100);
        petriNetUIBuilder.AddPlace("P2", 200, 200);
        petriNetUIBuilder.AddPlace("P3", 300, 300);
        
        petriNetUIBuilder.AddToken("P1", 1);
        petriNetUIBuilder.AddToken("P2", 2);
        petriNetUIBuilder.AddToken("P3", 3);
        
        petriNetUIBuilder.AddEdge("P1", "P2");
        petriNetUIBuilder.AddEdge("P2", "P3");
        petriNetUIBuilder.AddEdge("P1", "P3");
        
        petriNetUIBuilder.AddTransition("P1 - P2", new Point(300, 300));

        var canvas = sender as Canvas;
        var petriCanvas = petriNetUIBuilder.ToCanvas();
        canvas.Children.Add(petriCanvas);
        
        // Debug
        petriNetUIBuilder.ToCanvas();
    }
}