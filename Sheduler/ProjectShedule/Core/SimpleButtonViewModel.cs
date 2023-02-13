using System.Windows.Input;
using ProjectShedule.Core.Interfaces;

namespace ProjectShedule.Core
{
    public abstract class BaseButtonViewModel : BindableBase<BaseButtonViewModel>, ISimpleButtonViewModel
    {
        public string Text { get => GetProperty<string>(); set => SetProperty(value); }
        public ICommand Command { get => GetProperty<ICommand>(); set => SetProperty(value); }
        public object CommandParameter { get => GetProperty<object>(); set => SetProperty(value); }
        public double Opacity { get => GetProperty<double>(); set => SetProperty(value); }
        public bool IsEnable { get => GetProperty<bool>(); set => SetProperty(value); }
        public int CornerRadius { get => GetProperty<int>(); set => SetProperty(value); }
        public double WidthRequest { get => GetProperty<double>(); set => SetProperty(value); }
        public double HeightRequest { get => GetProperty<double>(); set => SetProperty(value); }

        public void SetByDisabled(bool value)
        {
            Opacity = value ? 0.5 : 1;
            IsEnable = !value;
        }
    }
}