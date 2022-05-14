using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ProjectShedule.Shedule.ViewModels
{
    public class SmallTaskViewModel : INotifyPropertyChanged, IHasSmallTask
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly SmallTask _smallTask;
        public SmallTaskViewModel() : this(new SmallTask()) { }
        public SmallTaskViewModel(SmallTask smallTask)
        {
            _smallTask = smallTask;
        }

        #region Properties
        public int Id
        {
            get => _smallTask.Id;
            set
            {
                if (_smallTask.Id != value)
                {
                    _smallTask.Id = value;
                }
            }
        }
        public int IdNote
        {
            get => _smallTask.IdNote;
            set
            {
                if (_smallTask.IdNote != value) 
                {
                    _smallTask.IdNote = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get => _smallTask.Name;
            set
            {
                if (_smallTask.Name != value)
                {
                    _smallTask.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Status
        {
            get => _smallTask.Status;
            set
            {
                if (_smallTask.Status != value)
                {
                    _smallTask.Status = value;
                    OnPropertyChanged();
                }
            }
        }
        public ICommand DeleteMeCommand { get; set; }
        public ICommand CheckChangedCommand { get; set; }

        SmallTask IHasSmallTask.SmallTask => _smallTask;
        #endregion
    }
}
