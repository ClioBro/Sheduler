using ProjectShedule.Shedule;
using Xamarin.Forms;

namespace ProjectShedule.GlobalSetting.Settings.SheduleEvents
{
    public class ShapeEventSetting : Setting<ShapeEventSetting>, ISimpleShape
    {
        private const float _defaultCornerRadius = 4f;
        private const double _defaultSize = 7d;
        private const double _defaultOpacity = 1d;

        public readonly double MaxSize = 10d;
        public readonly double MinSize = 0d;

        public readonly float MaxCornerRadius = 5f;
        public readonly float MinCornerRadius = 0f;

        public readonly double MaxOpacity = 1d;
        public readonly double MinOpacity = 0d;
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
            if (radius >= MinCornerRadius && radius <= MaxCornerRadius)
            {
                SavePreference(nameof(Type.CornerRadius), radius);

            }
        }
        public float GetCornerRadius() => GetPreference(nameof(Type.CornerRadius), _defaultCornerRadius);

        public void SetOpacity(double opacity)
        {
            if (opacity >= MinOpacity && opacity <= MaxOpacity)
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
