using System;

namespace GroupProject.Scripts;

public class SidebarListItem
{
    public string Label { get; set; }
    public Type Type { get; set; }
    
    public SidebarListItem(Type type)
    {
        Type = type;
        Label = type.Name.Replace("Window", "");
    }
}