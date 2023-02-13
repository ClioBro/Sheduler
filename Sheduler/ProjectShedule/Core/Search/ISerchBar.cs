using System.Windows.Input;

namespace ProjectShedule.Core.Search
{
    internal interface ISerchBar
    {
        string Text { get; set; }
        string Placeholder { get; set; }
        ICommand SerchCommand { get; set; }
    }
}