using ProjectShedule.Core.Interfaces;

namespace ProjectShedule.Core.Stepper
{
    public interface IStepControll
    {
        ISimpleButtonViewModel UpButtonViewModel { get; }
        ISimpleButtonViewModel DownButtonViewModel { get; }
    }
}