using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.PopUpAlert.Question;
using System.Threading.Tasks;


namespace ProjectShedule.Shedule.Interfaces
{
    public delegate Task<QuestionView.Answer> QuestionDelegate<T>(T item);
    public interface ISmallTaskBuilder<out T> : IBuilder<T>
        where T : ISmallTask
    {
        ISmallTaskBuilder<T> SetText(string text);
        ISmallTaskBuilder<T> SetStatus(bool status);
    }
}
