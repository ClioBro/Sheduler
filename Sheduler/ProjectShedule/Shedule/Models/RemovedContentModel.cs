using ProjectShedule.Core;
using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.Models
{
    public class RemovedContentModel : IThrashWriteOperation<IHasData<Note>>, IThrashWriteOperation<IHasData<SmallTask>>
    {
        private readonly IExtandedDeadNoteDataBase _extandedDeadNoteManager;

        public RemovedContentModel(IExtandedDeadNoteDataBase extandedDeadNoteDataBase)
        {
            _extandedDeadNoteManager = extandedDeadNoteDataBase;
        }

        public IEnumerable<Note> GetAllContent()
        {
            return _extandedDeadNoteManager.GetAllItems();
        }
        public IEnumerable<Note> GetByDateRange(DateTimeRange dateTimeRange)
        {
            return _extandedDeadNoteManager.GetByDateRange(dateTimeRange);
        }

        public void Delete(IHasData<Note> item)
        {
            _extandedDeadNoteManager.Delete(GetData(item));
        }
        public void Revive(IHasData<Note> item)
        {
            _extandedDeadNoteManager.Revive(GetData(item));
        }
        public void Delete(IHasData<SmallTask> item)
        {
            _extandedDeadNoteManager.SmallTaskDataBase.Delete(GetData(item));
        }
        public void Revive(IHasData<SmallTask> item)
        {
            _extandedDeadNoteManager.SmallTaskDataBase.Revive(GetData(item));
        }

        private T GetData<T>(IHasData<T> viewModel) => viewModel.GetData();
    }
}

