using ProjectShedule.Core.Notify;
using ProjectShedule.DataBase.BusinessLayer.Entities.Base;
using ProjectShedule.Shedule.Interfaces;
using System;
using Xamarin.Forms.Internals;

namespace ProjectShedule.Shedule.Models.Base
{
    public abstract class BaseNoteModel : IBasePackNoteModel
    {
        protected readonly BaseNote _baseNote;
        public BaseNoteModel(BaseNote baseNote)
        {
            _baseNote = baseNote;
            NotifyRepeats = NotifyRepeatCollections.NotifyRepeats;
        }

        #region Properties

        public virtual NotifyRepeat[] NotifyRepeats { get; protected set; }
        public NotifyRepeat NotifyRepeat
        {
            get => NotifyRepeats[_baseNote.RepeatIdKey];
            set => _baseNote.RepeatIdKey = NotifyRepeats.IndexOf(value);
        }

        public int Id { get => _baseNote.Id; set => _baseNote.Id = value; }
        public string Header { get => _baseNote.Header; set => _baseNote.Header = value; }
        public string DopText { get => _baseNote.DopText; set => _baseNote.DopText = value; }
        public DateTime CreatedDateTime { get => _baseNote.CreatedDateTime; set => _baseNote.CreatedDateTime = value; }
        public DateTime? DeletedDateTime => _baseNote.DeletedDateTime;
        public DateTime? AppointmentDate { get => _baseNote.AppointmentDate; set => _baseNote.AppointmentDate = value; }
        public int RepeatIdKey { get => _baseNote.RepeatIdKey; set => _baseNote.RepeatIdKey = value; }
        public bool Notify { get => _baseNote.Notify; set => _baseNote.Notify = value; }
        public bool IsAppointmentDate => _baseNote.IsAppointmentDate;
        public bool IsDeleted => _baseNote.IsDeleted;
        public string BackgroundColorKey { get => _baseNote.BackgroundColorKey; set => _baseNote.BackgroundColorKey = value; }
        public string LineColorKey { get => _baseNote.LineColorKey; set => _baseNote.LineColorKey = value; }

        #endregion
        public abstract object Clone();
    }
    //public abstract class BaseDeleteblePackNoteModel<T> : BasePackNoteModel<T> where T : BaseSmallTaskViewModel
    //{
    //    private readonly Note _note;
    //    public BaseDeleteblePackNoteModel(Note note) : base(note)
    //    {
    //        NoteRepository = App.ApplicationContext.UtilityRepository;
    //    }
    //    public IExtendedNoteRepository NoteRepository { get; protected set; }
    //    protected void DeleteInDataBase() => NoteRepository.Delete(_note);
    //}
}
