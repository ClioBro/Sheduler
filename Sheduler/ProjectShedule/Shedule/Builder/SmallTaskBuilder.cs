
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.BusinessLayer.Entities.Base;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Builder
{
    public class SmallTaskBuilder : ISmallTaskBuilder<BaseSmallTask>
    {
        private string _text;
        private bool _status;
        
        public virtual BaseSmallTask Build()
        {
            return new SmallTask()
            {
                Header = _text,
                Status = _status
            };
        }

        public virtual ISmallTaskBuilder<BaseSmallTask> SetStatus(bool value)
        {
            _status = value;
            return this;
        }
        public virtual ISmallTaskBuilder<BaseSmallTask> SetText(string text)
        {
            _text = text;
            return this;
        }
    }
}
