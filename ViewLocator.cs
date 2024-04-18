using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using GroupProject.ViewModels;
using GroupProject.Views;

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
            
            return control;
        }

        // If the view is not found, return the FallbackPageView
        name = typeof(FallbackPageView).FullName!;
        type = Type.GetType(name);
        var fallbackControl = (Control)Activator.CreateInstance(type)!;
        fallbackControl.DataContext = data;
            
        return fallbackControl;
    }

    public bool Match(object? data)
    {
        return data is ViewModelBase;
    }
}