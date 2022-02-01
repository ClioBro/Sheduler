using ProjectShedule.Shedule;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents
{
    public class ShapeEventSetting : Setting<ShapeEventSetting>, ISimpleShape
    {
        private const float _defaultCornerRadius = 4f;
        private const double _defaultSize = 7.0;
        private const double _defaultOpacity = 1;

        public readonly double MaxSize = 10;
        public readonly double MinSize = 0;
        public void SetSize(double size) => SendSize(size);
        public void SetSize(Size size) => SendSize(size.Height);
        private void SendSize(double size)
        {
            if (size <= MaxSize && size >= MinSize)
            {
                SavePreference(nameof(Type.Size), size);
            }
        }

        public Size GetSize()
        {
            double symmetricalSides = GetPreference(nameof(Type.Size), _defaultSize);
            return new Size(symmetricalSides, symmetricalSides);
        }

        public void SetCornerRadius(float radius)
        {
            if (radius >= 0 && radius <= 10)
            {
                SavePreference(nameof(Type.CornerRadius), radius);
                
            }
        }
        public float GetCornerRadius() => GetPreference(nameof(Type.CornerRadius), _defaultCornerRadius);

        public void SetOpacity(double opacity)
        {
            if (opacity >= 0 && opacity <= 1)
            {
                SavePreference(nameof(Type.Opacity), opacity);
            }
        }
        public double GetOpacity() => GetPreference(nameof(Type.Opacity), _defaultOpacity);

        private enum Type
        {
            Size, Opacity, CornerRadius
        }
    }
}
