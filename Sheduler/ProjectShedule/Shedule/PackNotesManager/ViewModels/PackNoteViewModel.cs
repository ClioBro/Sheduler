using ProjectShedule.DataNote;
using ProjectShedule.GlobalSetting;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.NotifyOnApp.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class PackNoteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private readonly PackNoteModel _packNoteModel;

        public delegate void SmallTaskViewModelDelegate(SmallTaskViewModel smallTaskViewModel);
        public event SmallTaskViewModelDelegate TaskCheckChanged;
        public event SmallTaskViewModelDelegate TaskDeletePressed;

        public PackNoteViewModel() : this(new PackNoteModel()) { }
        public PackNoteViewModel(PackNoteModel packNoteModel)
        {
            _packNoteModel = packNoteModel;
            _packNoteModel.SmallTaskAdded += AssigmentCommands;

            DeleteTaskCommand = new Command<SmallTaskViewModel>(DeleteTaskCommandHandler);
            CheckChangedTaskCommand = new Command<SmallTaskViewModel>(TaskCheckChangedCommandHandler);

            AssigmentCommands(SmallTasks);
        }


        #region Properties

        #region Commands
        public ICommand DeleteMeCommand { get; set; }
        public ICommand EditMeCommand { get; set; }

        private ICommand DeleteTaskCommand { get; set; }
        private ICommand CheckChangedTaskCommand { get; set; }
        #endregion

        public INavigation Navigation { get; set; }

        public int Id
        {
            get => _packNoteModel.Note.Id;
            set
            {
                if (_packNoteModel.Note.Id != value)
                {
                    _packNoteModel.Note.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Header
        {
            get => _packNoteModel.Note.Header;
            set
            {
                if (_packNoteModel.Note.Header != value)
                {
                    _packNoteModel.Note.Header = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DopText
        {
            get => _packNoteModel.Note.DopText;
            set
            {
                if (_packNoteModel.Note.DopText != value)
                {
                    _packNoteModel.Note.DopText = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime CreatedDateTime
        {
            get => _packNoteModel.Note.CreatedDateTime;
            set
            {
                if (_packNoteModel.Note.CreatedDateTime != value)
                {
                    _packNoteModel.Note.CreatedDateTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime AppointmentDate
        {
            get => _packNoteModel.Note.AppointmentDate;
            set
            {
                if (_packNoteModel.Note.AppointmentDate != value)
                {
                    _packNoteModel.Note.AppointmentDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public int RepeadIdKey
        {
            get => _packNoteModel.Note.RepeadIdKey;
            set
            {
                if (_packNoteModel.Note.RepeadIdKey != value)
                {
                    _packNoteModel.Note.RepeadIdKey = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Notify
        {
            get => _packNoteModel.Note.Notify;
            set
            {
                if (_packNoteModel.Note.Notify != value)
                {
                    _packNoteModel.Note.Notify = value;
                    OnPropertyChanged();
                }
            }
        }

        #region VisebleObjectsOnView
        public bool DateTimeisVisible
        {
            get => _packNoteModel.Note.DateTimeStatus;
            set
            {
                if (_packNoteModel.Note.DateTimeStatus != value)
                {
                    _packNoteModel.Note.DateTimeStatus = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool HasRepead => RepeadIdKey != (int)RepeadType.NoRepeat;
        public bool DopTextIsVisible => !string.IsNullOrWhiteSpace(DopText);
        public bool HeaderIsVisible => !string.IsNullOrWhiteSpace(Header);
        #endregion

        public bool HasSmallTasks => SmallTasks.Count() > 0;
        public string TasksCompletedInformation => $"{SmallTasks.Count(t => t.Status)}/{SmallTasks.Count}";
        public Note Note => _packNoteModel.Note as Note;
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => _packNoteModel.SmallTasks;

        #region Colors
        public Color BackGroundColor
        {
            get => _packNoteModel.BackGroundColor;
            set
            {
                if (_packNoteModel.BackGroundColor != value)
                {
                    _packNoteModel.BackGroundColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color LineColor
        {
            get => _packNoteModel.LineColor;
            set
            {
                if (_packNoteModel.LineColor != value)
                {
                    _packNoteModel.LineColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public IPackNote Model { get => _packNoteModel; }
        #endregion

        #endregion
        private void AssigmentCommands(IEnumerable<SmallTaskViewModel> smallTaskViewModels)
        {
            foreach (SmallTaskViewModel smallTaskViewModel in smallTaskViewModels)
            {
                AssigmentCommands(smallTaskViewModel);
            }
        }
        private void AssigmentCommands(SmallTaskViewModel smallTaskViewModel)
        {
            smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
            smallTaskViewModel.CheckChangedCommand = CheckChangedTaskCommand;
        }

        private protected async void DeleteTaskCommandHandler(SmallTaskViewModel taskViewModel)
        {
            IDeleteConfirmation deleteConfirmation = new DeleteConfirmationSetting();
            if (deleteConfirmation.AskQuestion)
            {
                var answer = await Navigation.ShowQuestionForDeletion(taskViewModel.Text);
                if (answer.Value == false)
                    return;
            }
                
            _packNoteModel.DeleteSmallTask(taskViewModel);
            TaskDeletePressed?.Invoke(taskViewModel);
            OnPropertyChanged(nameof(HasSmallTasks));
            OnPropertyChanged(nameof(TasksCompletedInformation));
        }

        private protected void TaskCheckChangedCommandHandler(SmallTaskViewModel taskViewModel)
        {
            TaskCheckChanged?.Invoke(taskViewModel);
            OnPropertyChanged(nameof(TasksCompletedInformation));
        }
    }
   
}
