using ProjectShedule.Core.Interfaces;
using ProjectShedule.Core.Notify;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Models
{
    public class ShedulePageModel : IShedulePageDataWrite
    {
        private readonly IExtandedLiveNoteDataBase _extandedLiveNoteDataBase;
        private readonly INotifyManager<INote> _notifyManager;

        public ShedulePageModel()
        {
            _extandedLiveNoteDataBase = App.ApplicationContext.UtilityExtendedLiveNoteRepository;
            _notifyManager = new NoteNotifyOnAppManager();
        }

        public IGetItemsDateTime<Note> DataBaseGetItemsDateTime => _extandedLiveNoteDataBase;

        public void Save(IHasData<Note> hasData)
        {
            Note note = hasData.GetData();
            if (note.Id is 0)
                _extandedLiveNoteDataBase.Insert(note);
            else
                _extandedLiveNoteDataBase.Update(note);
            if (note.Notify)
                _notifyManager.SendNotify(note);
        }
        public void Delete(IHasData<Note> item)
        {
            Note note = item.GetData();

            _extandedLiveNoteDataBase.Delete(note);
            if (note.Notify)
                _notifyManager.RemoveNotify(note);
        }

        public void Save(IHasData<SmallTask> item)
        {
            SmallTask smallTask = item.GetData();
            _extandedLiveNoteDataBase.SmallTaskDataBase.Update(smallTask);
        }
        public void Delete(IHasData<SmallTask> item)
        {
            SmallTask smallTask = item.GetData();
            _extandedLiveNoteDataBase.SmallTaskDataBase.Delete(smallTask);
        }
    }
}