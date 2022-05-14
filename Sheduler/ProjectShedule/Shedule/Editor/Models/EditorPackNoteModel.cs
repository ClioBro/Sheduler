using ProjectShedule.Language.Resources.PopUp.Repeads;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.PackNotesManager;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProjectShedule.Shedule.Editor.Models
{
    public class EditorPackNoteModel
    {
        public event EventHandler<bool> PackNoteSaved;
        public event EventHandler<ReadOnlyObservableCollection<SmallTaskViewModel>> SmallTasksChanged;
        public event EventHandler<RepeadItem> SelectedRepeadChanged;

        private readonly PackNoteModel _packNoteModel;
        private string _taskName;

        public EditorPackNoteModel() : this(new PackNoteModel())
        {

        }
        public EditorPackNoteModel(PackNoteModel packNoteModel)
        {
            _packNoteModel = packNoteModel;

            DeleteTaskCommand = new Command<SmallTaskViewModel>(RemoveTask);
            AssigmentCommand(SmallTasks.ToArray());
            _packNoteModel.SmallTaskAdded += OnSmallTasksChanged;
            _packNoteModel.SmallTaskDeleted += OnSmallTasksChanged;
        }
        private ICommand DeleteTaskCommand { get; set; }
        public string Header
        {
            get => _packNoteModel.Note.Header;
            set => _packNoteModel.Note.Header = value;
        }
        public string DopText
        {
            get => _packNoteModel.Note.DopText;
            set => _packNoteModel.Note.DopText = value;
        }
        public Color LineColor
        {
            get => _packNoteModel.LineColor;
            set => _packNoteModel.LineColor = value;
        }
        public Color BackGroundColor
        {
            get => _packNoteModel.BackGroundColor;
            set => _packNoteModel.BackGroundColor = value;
        }
        public RepeadItem SelectedRepead
        {
            get
            {
                return CustomRepeads.RepeadsItems[_packNoteModel.Note.RepeadIdKey];
            }
            set
            {
                _packNoteModel.Note.RepeadIdKey = (int)value.RepeadType;
                SelectedRepeadChanged?.Invoke(this, value);
            }
        }
        public DateTime CreatedDateTime
        {
            get => _packNoteModel.Note.CreatedDateTime;
            set => _packNoteModel.Note.CreatedDateTime = value;
        }
        public bool OnTheDate
        {
            get => _packNoteModel.Note.DateTimeStatus;
            set => _packNoteModel.Note.DateTimeStatus = value;
        }
        public DateTime AppointmentDate
        {
            get => _packNoteModel.Note.AppointmentDate;
            set => _packNoteModel.Note.AppointmentDate = value;
        }
        public bool Notify
        {
            get => _packNoteModel.Note.Notify;
            set
            {
                _packNoteModel.Note.Notify = value;
                if (!value)
                {
                    SelectedRepead = CustomRepeads.RepeadsItems[0];
                }
            }
        }
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => _packNoteModel.SmallTasks;

        public string TaskAddingEntryText { get => _taskName; set => _taskName = value; }

        public void AddTask()
        {
            if (string.IsNullOrWhiteSpace(TaskAddingEntryText))
                return;
            string taskName = TaskAddingEntryText;
            TaskAddingEntryText = string.Empty;
            var smallTaskViewModel = new SmallTaskViewModel { Text = taskName.Trim() };
            AssigmentCommand(smallTaskViewModel);
            _packNoteModel.AddSmallTask(smallTaskViewModel);
        }
        public void RemoveTask(SmallTaskViewModel smallTaskViewModel)
        {
            _packNoteModel.DeleteSmallTask(smallTaskViewModel);
        }
        public void Save()
        {
            if (string.IsNullOrWhiteSpace(Header) && string.IsNullOrWhiteSpace(DopText))
                return;

            CreatedDateTime = DateTime.Now;
            if (OnTheDate == false)
                AppointmentDate = CreatedDateTime;

            PackNoteDataBaseController packNoteManager = new PackNoteDataBaseController();
            packNoteManager.Save(_packNoteModel, correct: true);

            bool NotifyIsValid = OnTheDate && Notify;
            if (NotifyIsValid)
            {
                NotifyOnAppManager notyfyManager = new NotifyOnAppManager();
                notyfyManager.SendNotify(_packNoteModel);
            }

            PackNoteSaved.Invoke(this, true);
        }
        public DemonstrationViewPackNote GetDemonstrationPage()
        {
            return new DemonstrationViewPackNote(_packNoteModel);
        }
        public ColorSelectionPage GetColorSelectionPage()
        {
            ColorSelectionPageCreation colorSelectionPageCreation = new ColorSelectionPageCreation(_packNoteModel);

            colorSelectionPageCreation.ColorSelection.LineTarget.ColorSelected += (sender, color) => LineColor = color;
            colorSelectionPageCreation.ColorSelection.BackGroundTarget.ColorSelected += (sender, color) => BackGroundColor = color;

            return colorSelectionPageCreation.Create();
        }
        public RadioButtonsSelecterPage GetRadioButtonSelectedPage()
        {
            RepeadItem[] repeadItems = CustomRepeads.RepeadsItems;

            var radioButtonPage = new RadioButtonsSelecterPage(repeadItems,
                                                repeadItems.IndexOf(SelectedRepead),
                                                Repeads.HeaderLabel);

            radioButtonPage.SelectedItemChanged
                += (sender, selectedItem)
                => SelectedRepead = (RepeadItem)selectedItem;
            return radioButtonPage;
        }
        private void AssigmentCommand(params SmallTaskViewModel[] smallTaskViewModel)
        {
            foreach (SmallTaskViewModel smallTask in smallTaskViewModel)
            {
                smallTask.DeleteMeCommand = new Command<SmallTaskViewModel>(RemoveTask);
            }
        }
        private void AssigmentCommand(SmallTaskViewModel smallTaskViewModel)
        {
            smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
        }
        private void OnSmallTasksChanged(SmallTaskViewModel smallTaskViewModel)
        {
            SmallTasksChanged?.Invoke(this, SmallTasks);
        }
    }
}
