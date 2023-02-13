using ProjectShedule.Core;
using ProjectShedule.PopUpAlert.ColorSelection.Models;
using ProjectShedule.PopUpAlert.ColorSelection.States;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.ViewModels
{
    public interface IColoredMark
    {
        public MarkState State { get; }
        public Color ValueColor { get; }
    }
    public class ColoredMarkViewModel : BaseViewModel, IColoredMark
    {
        private readonly ColoredMarkModel _model;

        public ColoredMarkViewModel(ColoredMarkModel coloredBoxModel)
        {
            _model = coloredBoxModel;
        }

        public MarkState State { get => _model.State; }
        public Color ValueColor { get => _model.ValueColor; }

        public ICommand PressedCommand { get; set; }

        public void SwichState()
        {
            _model.SwichState();
            OnPropertyChanged(nameof(State));
        }
    }
}
