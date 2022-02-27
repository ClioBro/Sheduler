using System;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public class Target : ITarget
    {
        private Color _tiedColor;
        private bool _isSelected;

        public event EventHandler<Color> ColorSelected;
        public event EventHandler<bool> SelectedChanged;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    SelectedChanged?.Invoke(this, value);
                }
            }
        }
        public string Text { get; set; }
        public Color DefaultBorderColor { get; set; } = Color.Default;
        public Color SelectedBorderColor { get; set; } = Color.Black;
        public Color BorderColor => IsSelected
            ? SelectedBorderColor
            : DefaultBorderColor;
        public virtual Color TiedColor
        {
            get => _tiedColor;
            set
            {
                _tiedColor = value;
                ColorSelected?.Invoke(this, value);
            }
        }
    }
}
