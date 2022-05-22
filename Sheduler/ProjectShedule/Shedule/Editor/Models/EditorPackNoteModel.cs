using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Editor.Models
{
    public interface IEditorPackNote
    {
        void AddNewSmallTask(string text);
        void RemoveTask(BaseSmallTaskViewModel smallTaskViewModel);
        void Save();
    }
    public abstract class BaseEditorPackNoteModel : IEditorPackNote
    {
        public event EventHandler<Color> LineColorChanged;
        public event EventHandler<Color> BackGroundColorChanged;
        public abstract event EventHandler<ReadOnlyPackNote> PackNoteSaved;
        public event EventHandler<RepeadItem> SelectedRepeadChanged;
        public abstract event EventHandler<BaseSmallTaskViewModel> SmallTasksAdded;
        public abstract event EventHandler<BaseSmallTaskViewModel> SmallTasksDeleted;

        protected BasePackNoteModel _basePackNoteModel;
        public BaseEditorPackNoteModel(BasePackNoteModel basePackNoteModel)
        {
            _basePackNoteModel = basePackNoteModel;
        }
        public BasePackNoteModel BasePackNoteModel => _basePackNoteModel;
        public abstract ICommand DeleteTaskCommand { get; set; }
        public string Header
        {
            get => _basePackNoteModel.Note.Header;
            set => _basePackNoteModel.Note.Header = value;
        }
        public string DopText
        {
            get => _basePackNoteModel.Note.DopText;
            set => _basePackNoteModel.Note.DopText = value;
        }
        public DateTime CreatedDateTime
        {
            get => _basePackNoteModel.Note.CreatedDateTime;
            set => _basePackNoteModel.Note.CreatedDateTime = value;
        }
        public bool OnTheDate
        {
            get => _basePackNoteModel.Note.DateTimeStatus;
            set => _basePackNoteModel.Note.DateTimeStatus = value;
        }
        public DateTime AppointmentDate
        {
            get => _basePackNoteModel.Note.AppointmentDate;
            set => _basePackNoteModel.Note.AppointmentDate = value;
        }
        public bool Notify
        {
            get => _basePackNoteModel.Note.Notify;
            set
            {
                _basePackNoteModel.Note.Notify = value;

                if (value == false)
                {
                    SelectedRepead = CustomRepeads.RepeadsItems[0];
                }
            }
        }
        public Color LineColor
        {
            get => _basePackNoteModel.LineColor;
            set
            {
                _basePackNoteModel.LineColor = value;
                LineColorChanged?.Invoke(this, value);
            }
        }
        public Color BackGroundColor
        {
            get => _basePackNoteModel.BackGroundColor;
            set
            {
                _basePackNoteModel.BackGroundColor = value;
                BackGroundColorChanged?.Invoke(this, value);
            }
        }
        public RepeadItem SelectedRepead
        {
            get
            {
                return CustomRepeads.RepeadsItems[_basePackNoteModel.Note.RepeadIdKey];
            }
            set
            {
                _basePackNoteModel.Note.RepeadIdKey = (int)value.RepeadType;
                SelectedRepeadChanged?.Invoke(this, value);
            }
        }

        public ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks => _basePackNoteModel.SmallTasks;
        public abstract void AddNewSmallTask(string text);
        public virtual void RemoveTask(BaseSmallTaskViewModel smallTaskViewModel)
        {
            _basePackNoteModel.DeleteSmallTask(smallTaskViewModel);
        }
        public abstract void Save();
    }
    public class EditorPackNoteModel : BaseEditorPackNoteModel
    {
        public override event EventHandler<ReadOnlyPackNote> PackNoteSaved;
        public override event EventHandler<BaseSmallTaskViewModel> SmallTasksAdded;
        public override event EventHandler<BaseSmallTaskViewModel> SmallTasksDeleted;

        private ICommand _commandDelete;

        private IBuilderSmallTaskViewModel _builderSmallTaskViewModel;
        private readonly IPackNoteDataBaseController _packNoteDBController;
        public EditorPackNoteModel(BasePackNoteModel packNoteModel, IPackNoteDataBaseController dataBaseController) : base(packNoteModel)
        {
            _builderSmallTaskViewModel = new BuilderSmallTaskViewModel();
            _packNoteDBController = dataBaseController;

            _basePackNoteModel.SmallTaskAdded += (BaseSmallTaskViewModel smallTaskViewModel) 
                => SmallTasksAdded?.Invoke(this, smallTaskViewModel);

            _basePackNoteModel.SmallTaskDeleted += (BaseSmallTaskViewModel smallTaskViewModel) 
                => SmallTasksDeleted?.Invoke(this, smallTaskViewModel);
        }
        public IBuilderSmallTaskViewModel BuilderSmallTaskViewModel 
        {
            get => _builderSmallTaskViewModel;
            set => _builderSmallTaskViewModel = value; 
        }
        public override ICommand DeleteTaskCommand
        {
            get => _commandDelete;
            set
            {
                _commandDelete = value;
                AssigmentCommand(SmallTasks.ToArray());
            } 
        }
        public override void AddNewSmallTask(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return;

            BaseSmallTaskViewModel smallTask = BuildNewSmallTask(text);

            _basePackNoteModel.AddSmallTask(smallTask);
        }
        public override void Save()
        {
            if (string.IsNullOrWhiteSpace(Header) && string.IsNullOrWhiteSpace(DopText))
                return;

            CreatedDateTime = DateTime.Now;
            if (OnTheDate == false)
                AppointmentDate = CreatedDateTime;

            _packNoteDBController.Save(_basePackNoteModel);

            bool notifyIsValid = OnTheDate && Notify;

            if (notifyIsValid)
            {
                NotifyOnAppManager notyfyManager = new NotifyOnAppManager();
                notyfyManager.SendNotify(_basePackNoteModel);
            }

            PackNoteSaved.Invoke(this, new ReadOnlyPackNote(_basePackNoteModel));
        }
        private void AssigmentCommand(params BaseSmallTaskViewModel[] smallTaskViewModel)
        {
            foreach (BaseSmallTaskViewModel smallTask in smallTaskViewModel)
            {
                AssigmentCommand(smallTask);
            }
        }
        private void AssigmentCommand(BaseSmallTaskViewModel smallTaskViewModel)
        {
            smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
        }

        private BaseSmallTaskViewModel BuildNewSmallTask(string text)
        {
            return _builderSmallTaskViewModel
                .SetText(text.Trim())
                .SetDeleteCommand(DeleteTaskCommand)
                .Build();
        }
    }
}
