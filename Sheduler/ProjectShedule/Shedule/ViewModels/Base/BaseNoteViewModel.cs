using ProjectShedule.Core;
using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class BaseNoteViewModel : BaseViewModel, INoteViewModel
    {
        private readonly Note _note;
        public BaseNoteViewModel(Note note)
        {
            _note = note;

            BackGroundColor = string.IsNullOrEmpty(BackgroundColorKey)
                ? DefaulBackgroundColor : Color.FromHex(BackgroundColorKey);

            LineColor = string.IsNullOrEmpty(LineColorKey)
                ? DefaulLineColor : Color.FromHex(LineColorKey);
        }

        #region Properties
        protected Note Note { get => _note; }
        public int Id => _note.Id;
        public string BackgroundColorKey => _note.BackgroundColorKey;
        public string LineColorKey => _note.LineColorKey;
        public bool IsDeleted => Note.IsDeleted;

        #region BindableProperties
        public string Header
        {
            get => _note.Header;
            set
            {
                if (value == _note.Header)
                    return;
                _note.Header = value;
                OnPropertyChanged();
            }
        }
        public string DopText
        {
            get => _note.DopText;
            set
            {
                if (value == _note.DopText)
                    return;
                _note.DopText = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedDateTime
        {
            get => _note.CreatedDateTime;
            set
            {
                if (value == _note.CreatedDateTime)
                    return;
                _note.CreatedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime? DeletedDateTime
        {
            get => _note.DeletedDateTime;
            set
            {
                if (value == _note.DeletedDateTime)
                    return;
                _note.DeletedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime? AppointmentDate
        {
            get => _note.AppointmentDate;
            set
            {
                if (value == _note.AppointmentDate)
                    return;
                _note.AppointmentDate = value;
                OnPropertyChanged();
            }
        }
        public int RepeatIdKey
        {
            get => _note.RepeatIdKey;
            set
            {
                if (value == _note.RepeatIdKey)
                    return;
                _note.RepeatIdKey = value;
                OnPropertyChanged();
            }
        }
        public bool HasRepeat => RepeatIdKey != (int)RepeatType.NoRepeat;
        public bool Notify
        {
            get => _note.Notify;
            set
            {
                if (value == _note.Notify)
                    return;
                _note.Notify = value;
                OnPropertyChanged();
            }
        }
        public bool IsAppointmentDate => _note.IsAppointmentDate;
        public Color BackGroundColor
        {
            get
            {
                var backColor = Color.FromHex(BackgroundColorKey);
                return backColor == Color.Default ? DefaulBackgroundColor : backColor;
            }
            set
            {
                Note.BackgroundColorKey = value.ToHex();
                OnPropertyChanged();
            }
        }
        public Color LineColor
        {
            get
            {
                var lineColor = Color.FromHex(LineColorKey);
                return lineColor == Color.Default ? DefaulLineColor : lineColor;
            }
            set
            {
                Note.LineColorKey = value.ToHex();
                OnPropertyChanged();
            }
        }
        #endregion

        public Color DefaulBackgroundColor { get; protected set; } = Color.FromHex("#E6F2FF");
        public Color DefaulLineColor { get; protected set; } = Color.FromHex("#CCCCFF");

        #endregion Properties

        public abstract object Clone();
        Note IHasData<Note>.GetData()
        {
            return _note;
        }
    }
}
