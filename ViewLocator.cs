using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GroupProject.ViewModels;

namespace GroupProject;

public class ViewLocator : IDataTemplate
{
    public Control? Build(object? data)
    {
        if (data is null)
            return null;

        var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
        var type = Type.GetType(name);

        if (type != null)
        {
            var control = (Control)Activator.CreateInstance(type)!;
            control.DataContext = data;
            
            // Debug
            Console.WriteLine($"ViewLocator: {name}");
            Console.WriteLine($"ViewLocator Created Control: {control}");
            Console.WriteLine($"ViewLocator Created Control DataContext: {control.DataContext}");
            
            return control;
        }

        return new TextBlock { Text = "Not Found: " + name };
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}