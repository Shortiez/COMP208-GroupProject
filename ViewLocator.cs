using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;

namespace GroupProject;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object data)
    {
        var viewName = data.GetType().FullName.Replace("ViewModel", "View");
        var viewType = Type.GetType(viewName);

        if (viewType != null)
        {
            return (Control)Activator.CreateInstance(viewType);
            
        }

        return new TextBlock { Text = "Not Found: " + viewName };
    }

    public bool Match(object? data)
    {
        throw new System.NotImplementedException();
    }
}