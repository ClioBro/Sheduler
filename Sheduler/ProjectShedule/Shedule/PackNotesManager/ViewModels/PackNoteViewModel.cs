using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.GlobalSetting;
using ProjectShedule.GlobalSetting.Settings.SheduleNotesDelete;
using ProjectShedule.Shedule.DataBase.Interfaces;
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
    public interface IPackNoteViewModel : INotifyPropertyChanged
    {
        INavigation Navigation { get; }
        ICommand DeleteMeCommand { get; }
        ICommand EditMeCommand { get; }
        ICommand SaveMeCommand { get; }
        ICommand DeleteTaskCommand { get; }
        ICommand CheckChangedTaskCommand { get; }
    }
    public abstract class BasePackNoteViewModel : IPackNoteViewModel, IHasModel<BasePackNoteModel>
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        protected BasePackNoteModel _packNoteModel;

        public delegate void ISmallTaskViewModelDelegate(BaseSmallTaskViewModel smallTaskViewModel);
        public abstract event ISmallTaskViewModelDelegate TaskCheckChanged;
        public abstract event ISmallTaskViewModelDelegate TaskDeletePressed;

        #region Properties

        #region Commands
        public ICommand DeleteMeCommand { get; set; }
        public ICommand EditMeCommand { get; set; }
        public ICommand SaveMeCommand { get; set; }

        public ICommand DeleteTaskCommand { get; protected set; }
        public ICommand CheckChangedTaskCommand { get; protected set; }
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
        public ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks => _packNoteModel.SmallTasks;
        BasePackNoteModel IHasModel<BasePackNoteModel>.Model => _packNoteModel;

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

        
        #endregion

        #endregion
    }
    public class PackNoteViewModel : BasePackNoteViewModel
    {
        public override event ISmallTaskViewModelDelegate TaskCheckChanged;
        public override event ISmallTaskViewModelDelegate TaskDeletePressed;
        
        public PackNoteViewModel(BasePackNoteModel packNoteModel)
        {
            _packNoteModel = packNoteModel;
            _packNoteModel.SmallTaskAdded += AssigmentCommands;

            DeleteTaskCommand = new Command<BaseSmallTaskViewModel>(DeleteTaskCommandHandler);
            CheckChangedTaskCommand = new Command<BaseSmallTaskViewModel>(TaskCheckChangedCommandHandler);

            AssigmentCommands(SmallTasks);
        }

        private void AssigmentCommands(IEnumerable<BaseSmallTaskViewModel> smallTaskViewModels)
        {
            foreach (BaseSmallTaskViewModel smallTaskViewModel in smallTaskViewModels)
            {
                AssigmentCommands(smallTaskViewModel);
            }
        }
        private void AssigmentCommands(BaseSmallTaskViewModel smallTaskViewModel)
        {
            smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
            smallTaskViewModel.CheckChangedCommand = CheckChangedTaskCommand;
        }

        private protected async void DeleteTaskCommandHandler(BaseSmallTaskViewModel taskViewModel)
        {
            TaskDeletePressed?.Invoke(taskViewModel);

            IDeleteConfirmation deleteConfirmation = new DeleteConfirmationSetting();
            if (deleteConfirmation.AskQuestion == true)
            {
                var answer = await Navigation.ShowQuestionForDeletion(taskViewModel.Text);
                if (answer.Value == false)
                    return;
            }
                
            _packNoteModel.DeleteSmallTask(taskViewModel);
            OnPropertyChanged(nameof(HasSmallTasks));
            OnPropertyChanged(nameof(TasksCompletedInformation));
        }
        private protected void TaskCheckChangedCommandHandler(BaseSmallTaskViewModel taskViewModel)
        {
            TaskCheckChanged?.Invoke(taskViewModel);
            OnPropertyChanged(nameof(TasksCompletedInformation));
        }
    }
    public interface IBuilder<T>
    {
        T Build();
    }
    public interface IBuilderPackNoteViewModel : IBuilder<BasePackNoteViewModel>
    {
        IBuilderPackNoteViewModel SetModel(BasePackNoteModel basePackNoteModel);
        IBuilderPackNoteViewModel SetDeleteMeCommand(ICommand deleteMeCommand);
        IBuilderPackNoteViewModel SetEditMeCommand(ICommand editMeCommand);
        IBuilderPackNoteViewModel SetSaveMeCommand(ICommand saveMeCommand);
    }
    public class BuilderPackNoteViewModel : IBuilderPackNoteViewModel
    {
        private BasePackNoteModel _basePackNoteModel;
        private ICommand _deleteMeCommand;
        private ICommand _editMeCommand;
        private ICommand _saveMeCommand;
        public BuilderPackNoteViewModel() { }
        public BuilderPackNoteViewModel(BasePackNoteModel basePackNoteModel)
        {
            _basePackNoteModel = basePackNoteModel;
        }
       
        public BasePackNoteViewModel Build()
        {
            return new PackNoteViewModel(_basePackNoteModel)
            {
                DeleteMeCommand = _deleteMeCommand,
                EditMeCommand = _editMeCommand,
                SaveMeCommand = _saveMeCommand
            };
        }
        public IBuilderPackNoteViewModel SetDeleteMeCommand(ICommand deleteMeCommand)
        {
            _deleteMeCommand = deleteMeCommand;
            return this;
        }
        public IBuilderPackNoteViewModel SetEditMeCommand(ICommand editMeCommand)
        {
            _editMeCommand = editMeCommand;
            return this;
        }
        public IBuilderPackNoteViewModel SetSaveMeCommand(ICommand saveMeCommand)
        {
            _saveMeCommand = saveMeCommand;
            return this;
        }
        public IBuilderPackNoteViewModel SetModel(BasePackNoteModel basePackNoteModel)
        {
            _basePackNoteModel = basePackNoteModel;
            return this;
        }
    }

}
