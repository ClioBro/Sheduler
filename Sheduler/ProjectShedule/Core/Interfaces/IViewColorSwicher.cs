using Xamarin.Forms;

namespace ProjectShedule.Core.Interfaces
{
    public interface IColorSwich
    {
        Color On { get; set; }
        Color In { get; set; }
        void SwichColor();
    }
}
