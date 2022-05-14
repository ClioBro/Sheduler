using ProjectShedule.Shedule.ViewModels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public class ColorSelectionViewModel : INotifyPropertyChanged
    {
        private readonly ColorSelectionModel _colorSelectionModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public ColorSelectionViewModel(ColorSelectionModel colorSelectionModel)
        {
            _colorSelectionModel = colorSelectionModel;

            ColorSelectCommand = new Command<Color>(SetColorInCurrentTarget);
            LineDesignatingButtonCommand = new Command(_colorSelectionModel.SetLineTarget);
            BackGroundDesignatingButtonCommand = new Command(_colorSelectionModel.SetBackGroundTarget);

            _colorSelectionModel.LineTarget.SelectedChanged += LineTarget_SelectedChanged;
            _colorSelectionModel.BackGroundTarget.SelectedChanged += BackGroundTarget_SelectedChanged;
        }

        private void BackGroundTarget_SelectedChanged(object sender, bool e)
        {
            OnProppertyChanged(this, nameof(BackGroundTargetButtonBorderColor));
        }
        private void LineTarget_SelectedChanged(object sender, bool e)
        {
            OnProppertyChanged(this, nameof(LinteTargetButtonBorderColor));
        }

        public ICommand ColorSelectCommand { get; set; }
        public ICommand LineDesignatingButtonCommand { get; set; }
        public ICommand BackGroundDesignatingButtonCommand { get; set; }
        public PackNoteViewModel PackNoteViewModel => _colorSelectionModel.PackNoteViewModel;
        public string HeaderText => _colorSelectionModel.HeaderText;
        public string BackGroundTargetButtonText => _colorSelectionModel.BackGroundTarget.Text;
        public string LineTargetButtonText => _colorSelectionModel.LineTarget.Text;
        public Color BackGroundTargetButtonBorderColor => _colorSelectionModel.BackGroundTarget.BorderColor;
        public Color LinteTargetButtonBorderColor => _colorSelectionModel.LineTarget.BorderColor;
        private void SetColorInCurrentTarget(Color color) => _colorSelectionModel.SetColorInCurrentTarget(color);

        private void OnProppertyChanged(object sender, [CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(sender, new PropertyChangedEventArgs(propertyName));
        }
    }
}
