using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Media.Imaging;

using GroupProject.ViewModels;
using static GroupProject.ViewModels.TableUnionQuizPageViewModel;
using System.Collections.ObjectModel;
using Avalonia.VisualTree;
using System.Linq;

namespace GroupProject.Views;

public partial class TableUnionQuizPageView : UserControl
{
    public TableUnionQuizPageView()
    {
        InitializeComponent();

        // tell program which function handles event when the event is called
        AddHandler(DragDrop.DragOverEvent, Drag);
        AddHandler(DragDrop.DropEvent, Drop);
    }

    private async void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        if (sender is not Image image) return;
        if (image.DataContext is not DraggableImage draggableImage) return;

        // get the border control
        var border = this.FindControl<ItemsControl>("AnswerControl");
        if (border == null) return;

        // get the position of the cursor relative to the border
        var cursorPos = e.GetPosition(border);
        if (WithinBounds(border, cursorPos))
        {
            // call BeginDelete in View Model
            var viewModel = DataContext as TableUnionQuizPageViewModel;
            viewModel.BeginDelete(draggableImage);
        }

        var dragData = new DataObject();
        dragData.Set(TableUnionQuizPageViewModel.customFormat, draggableImage);
        var result = await DragDrop.DoDragDrop(e, dragData, DragDropEffects.Move);
    }

    private void Drag(object? sender, DragEventArgs e)
    {

    }

    private void Drop(object? sender, DragEventArgs e)
    {
        var data = e.Data.Get(TableUnionQuizPageViewModel.customFormat);

        if (data is not DraggableImage draggableImage) return;

        // get the border control
        var border = this.FindControl<ItemsControl>("AnswerControl");

        if (border == null) return;

        // get the position of the cursor relative to the border
        var cursorPos = e.GetPosition(border);

        var viewModel = DataContext as TableUnionQuizPageViewModel;
        // a number 0-7 which tells us which element in answer list we are dropping over
        int index = GetIndex(cursorPos, TableUnionQuizPageViewModel.answerMAX);

        if (TableUnionQuizPageViewModel.deletingRN)
        {
            if (!WithinBounds(border, cursorPos))
            {
                viewModel.DeleteInAnswer(index);
            }
            else
            {
                viewModel.ReplaceInAnswer(index);
            }
        }
        else
        {
            // if we are not moving stuff from Answer table
            // but putting stuff into Answer table...
            // ALSO need to ensure it's at right index in answer table
            if (WithinBounds(border, cursorPos))
            {
                viewModel.AddInAnswer(draggableImage, index);
            }
        }
    }

    // check if the cursor is within the bounds
    private bool WithinBounds(Control control, Point cursorPos)
    {
        if (cursorPos.X < 0 || cursorPos.Y < 0 ||
            cursorPos.Y > control.Height ||
            cursorPos.X > control.Width) return false;
        else
            return true;
    }
    // tells us which square we are hovering over in the answer table
    private int GetIndex(Point cursorPos, int maxRows)
    {
        int temp = maxRows / 2;
        double sectionHeight = AnswerControl.Bounds.Height / temp;

        // if cursor is in the left half of the control
        if (cursorPos.X < AnswerControl.Bounds.Width / 2)
        {
            for (int x = temp - 1; x >= 0; x--)
            {
                if (cursorPos.Y > x * sectionHeight) return 2*x;
            }
            return 0;
        }

        // if cursor is in the right half of the control
        else
        {
            for (int x = temp - 1; x >= 0; x--)
            {
                if (cursorPos.Y > x * sectionHeight) return 2 * x + 1;
            }
            return 1;
        }
    }

    private void Binding(object? sender, Avalonia.Input.PointerPressedEventArgs e)
    {
    }
}