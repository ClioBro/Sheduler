using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.ViewModels;

namespace ProjectShedule.DataBase.Entities.Base
{
    public abstract class BaseSmallTask : ISmallTask
    {
        public virtual int Id { get; set; }

        public int IdNote { get; set; }
        public string Text { get; set; }
        public bool Status { get; set; }

        public abstract object Clone();
    }

    public interface IBuilderSmallTask : IBuilder<BaseSmallTask>
    {
        IBuilderSmallTask SetText(string text);
        IBuilderSmallTask SetStatus(bool value);
    }

    public class BuilderSmallTaskModel : IBuilderSmallTask
    {
        private string _text;
        private bool _status;
        public BaseSmallTask Build()
        {
            return new SmallTask()
            {
                Text = _text,
                Status = _status
            };
        }

        public IBuilderSmallTask SetStatus(bool value)
        {
            _status = value;
            return this;
        }
        public IBuilderSmallTask SetText(string text)
        {
            _text = text;
            return this;
        }
    }
}
