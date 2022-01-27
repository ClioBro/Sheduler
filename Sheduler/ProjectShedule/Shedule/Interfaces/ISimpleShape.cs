using Xamarin.Forms;

namespace ProjectShedule.Shedule
{
    public interface ISimpleShape
    {
        public void SetSize(double size);
        public Size GetSize();
        public void SetCornerRadius(float radius);
        public float GetCornerRadius();
        public void SetOpacity(double opacity);
        public double GetOpacity();
    }
}
