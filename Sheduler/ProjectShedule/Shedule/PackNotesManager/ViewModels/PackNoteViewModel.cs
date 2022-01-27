using ProjectShedule.DataNote;
using ProjectShedule.GlobalSetting;
using ProjectShedule.Shedule.Enum;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
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
    public interface IHasModel<T>
    {
        T Model { get; }
    }
    public class PackNoteViewModel : INotifyPropertyChanged, IHasModel<IPackNote>
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public delegate void SmallTaskViewModelDelegate(SmallTaskViewModel smallTaskViewModel);
        public event SmallTaskViewModelDelegate TaskCheckChanged;
        public event SmallTaskViewModelDelegate TaskDeletePressed;

        public PackNoteViewModel() : this(new PackNoteModel()) { }
        public PackNoteViewModel(PackNoteModel packNoteModel)
        {
            PackNoteModel = packNoteModel;
            PackNoteModel.SmallTaskAdded += AssigmentCommands;

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
        private PackNoteModel PackNoteModel { get; }
        
        public int Id
        {
            get => PackNoteModel.Note.Id;
            set
            {
                if (PackNoteModel.Note.Id != value)
                {
                    PackNoteModel.Note.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Header
        {
            get => PackNoteModel.Note.Header;
            set
            {
                if (PackNoteModel.Note.Header != value)
                {
                    PackNoteModel.Note.Header = value;
                    OnPropertyChanged();
                }
            }
        }
        public string DopText
        {
            get => PackNoteModel.Note.DopText;
            set
            {
                if (PackNoteModel.Note.DopText != value)
                {
                    PackNoteModel.Note.DopText = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime CreatedDateTime
        {
            get => PackNoteModel.Note.CreatedDateTime;
            set
            {
                if (PackNoteModel.Note.CreatedDateTime != value)
                {
                    PackNoteModel.Note.CreatedDateTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime AppointmentDate
        {
            get => PackNoteModel.Note.AppointmentDate;
            set
            {
                if (PackNoteModel.Note.AppointmentDate != value)
                {
                    PackNoteModel.Note.AppointmentDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public int RepeadIdKey
        {
            get => PackNoteModel.Note.RepeadIdKey;
            set
            {
                if (PackNoteModel.Note.RepeadIdKey != value)
                {
                    PackNoteModel.Note.RepeadIdKey = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Notify
        {
            get => PackNoteModel.Note.Notify;
            set
            {
                if (PackNoteModel.Note.Notify != value)
                {
                    PackNoteModel.Note.Notify = value;
                    OnPropertyChanged();
                }
            }
        }

        #region VisebleObjectsOnView
        public bool DateTimeisVisible
        {
            get => PackNoteModel.Note.DateTimeStatus;
            set
            {
                if (PackNoteModel.Note.DateTimeStatus != value)
                {
                    PackNoteModel.Note.DateTimeStatus = value;
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
        public Note Note => PackNoteModel.Note;
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => PackNoteModel.SmallTasks;

        #region Colors
        public Color BackGroundColor
        {
            get => PackNoteModel.BackGroundColor;
            set
            {
                if (PackNoteModel.BackGroundColor != value)
                {
                    PackNoteModel.BackGroundColor = value;
                    OnPropertyChanged();
                }
            }
        }
        public Color LineColor
        {
            get => PackNoteModel.LineColor;
            set
            {
                if (PackNoteModel.LineColor != value)
                {
                    PackNoteModel.LineColor = value;
                    OnPropertyChanged();
                }
            }
        }

        public IPackNote Model { get => PackNoteModel; }
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
                
            PackNoteModel.DeleteSmallTask(taskViewModel);
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
