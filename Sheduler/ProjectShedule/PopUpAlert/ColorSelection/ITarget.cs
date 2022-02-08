using System;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public interface ITarget
    {
        event EventHandler<Color> ColorSelected;
        event EventHandler<bool> SelectedChanged;
        Color BorderColor { get; }
        Color TiedColor { get; }
        bool IsSelected { get; }
        string Text { get; }
    }
}
