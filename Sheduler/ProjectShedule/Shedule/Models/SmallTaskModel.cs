using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using System;

namespace ProjectShedule.Shedule.Models
{
    public class SmallTaskModel : ISmallTaskModel, IHasData<SmallTask>
    {
        private readonly SmallTask _smallTask;
        public SmallTaskModel(SmallTask baseSmallTask)
        {
            _smallTask = baseSmallTask;
        }
        public int Id
        {
            get => _smallTask.Id;
            private set => _smallTask.Id = value;
        }
        public string Header
        {
            get => _smallTask.Header;
            set => _smallTask.Header = value;
        }
        public bool Status
        {
            get => _smallTask.Status;
            set => _smallTask.Status = value;
        }
        public DateTime? DeletedDateTime
        {
            get => _smallTask.DeletedDateTime;
            set => _smallTask.DeletedDateTime = value;
        }
        public bool IsDeleted => _smallTask.IsDeleted;

        public virtual object Clone()
        {
            return new SmallTaskModel(_smallTask.Clone() as SmallTask);
        }

        SmallTask IHasData<SmallTask>.GetData()
        {
            return _smallTask;
        }
    }
}
