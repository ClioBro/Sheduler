using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.Interfaces
{
    public interface IChangeColor<T>
    {
        void ChangeColor(T target, Color newColor);
    }
}
