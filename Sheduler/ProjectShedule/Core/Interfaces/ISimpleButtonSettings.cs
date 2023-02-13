using System.Windows.Input;

namespace ProjectShedule.Core.Interfaces
{
    public interface ISimpleButtonViewModel
    {
        string Text { get; set; }
        ICommand Command { get; set; }
        object CommandParameter { get; set; }
        double Opacity { get; set; }
        bool IsEnable { get; set; }
        int CornerRadius { get; set; }
        double WidthRequest { get; set; }
        double HeightRequest { get; set; }
        void SetByDisabled(bool value);
    }
}