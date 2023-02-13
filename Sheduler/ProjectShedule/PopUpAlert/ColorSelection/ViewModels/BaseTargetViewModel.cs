using ProjectShedule.PopUpAlert.ColorSelection.Interfaces;
using ProjectShedule.PopUpAlert.ColorSelection.States;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.ViewModels
{
    public abstract class BaseTargetViewModel : BindableBase<BaseTargetViewModel>, IStateControll, IChangeColor<INoteVisualColor>
    {
        public BaseTargetViewModel(TargetState state)
        {
            SetProperty(state, nameof(ButtonState));
        }

        public string Text { get => GetProperty<string>(); set => SetProperty(value); }
        public ICommand PressedCommand { get => GetProperty<ICommand>(); set => SetProperty(value); }
        public TargetState ButtonState { get => GetProperty<TargetState>(); private set => SetProperty(value); }

        public void SwichState()
        {
            ButtonState = ButtonState.GetOppositeState();
        }
        public abstract void ChangeColor(INoteVisualColor target, Color color);
        public abstract Color GetColorInTarget(INoteViewModel target);
    }
}
