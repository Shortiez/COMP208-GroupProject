namespace GroupProject.UI.PetriNets;

public class PetriNetNode
{
    public string Name { get; set; }
    public int Tokens { get; set; }
    
    public double X { get; set; }
    public double Y { get; set; }
    
    public PetriNetNode(string name, int tokens, double x, double y)
    {
        Name = name;
        Tokens = tokens;
        X = x;
        Y = y;
    }
}