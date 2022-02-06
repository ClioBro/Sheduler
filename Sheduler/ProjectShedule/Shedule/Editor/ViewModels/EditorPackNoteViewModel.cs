using ProjectShedule.PopUpAlert;
using ProjectShedule.Shedule.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Forms;
using ProjectShedule.PopUpAlert.Resources;

namespace ProjectShedule.Shedule.ViewModels
{
    public class EditorPackNoteViewModel : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Commands
        public ICommand SavePackNoteCommand { get; private protected set; }
        public ICommand AddNewTaskCommand { get; private protected set; }
        public ICommand DeleteTaskCommand { get; private protected set; }
        public ICommand ShowPackNoteViewCommand { get; private protected set; }
        public ICommand ShowColorSelectionPageCommand { get; private protected set; }
        public ICommand AddTaskCommand { get; private protected set; }

        public ICommand ShowAvailableRepeadTypesCommand { get; private protected set; }
        #endregion
        public event Action SavePressed;
        public INavigation Navigation { get; set; }

        private RepeadItem _selectedRepead;
        private readonly PackNoteModel PackNoteModel;

        public EditorPackNoteViewModel(): this (new PackNoteModel()) {}
        public EditorPackNoteViewModel(PackNoteModel packNoteModel)
        {
            PackNoteModel = packNoteModel;
            SavePackNoteCommand = new Command(SaveCommanHandling);
            AddNewTaskCommand = new Command(AddNewTask);
            ShowPackNoteViewCommand = new Command(ShowViewPackNote);
            ShowColorSelectionPageCommand = new Command(ShowColorSelectionPage);
            ShowAvailableRepeadTypesCommand = new Command(ShowAvailableRepeads);
            DeleteTaskCommand = new Command<SmallTaskViewModel>(DeleteTask);

            AddTaskCommand = new Command(AddTask);

            AssigmentCommands(PackNoteModel.SmallTasks);

            InicializateSelectedRepead();
        }

        #region Properties
        public int Id
        {
            get => PackNoteModel.Note.Id;
            set
            {
                PackNoteModel.Note.Id = value;
                OnPropertyChanged();
            }
        }
        public string Header
        {
            get => PackNoteModel.Note.Header;
            set
            {
                PackNoteModel.Note.Header = value;
                OnPropertyChanged();
            }
        }
        public string DopText
        {
            get => PackNoteModel.Note.DopText;
            set
            {
                PackNoteModel.Note.DopText = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedDateTime
        {
            get => PackNoteModel.Note.CreatedDateTime;
            set
            {
                PackNoteModel.Note.CreatedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime ReminderDateTime
        {
            get => PackNoteModel.Note.AppointmentDate;
            set
            {
                PackNoteModel.Note.AppointmentDate = value;
                OnPropertyChanged();
            }
        }
        public DateTime Date
        {
            get => ReminderDateTime.Date;
            set
            {
                var time = ReminderDateTime.TimeOfDay;
                ReminderDateTime = value += time;
            }
        }
        public TimeSpan Time 
        {
            get => ReminderDateTime.TimeOfDay;
            set 
            {
                var date = ReminderDateTime.Date;
                ReminderDateTime = date += value;
            }
        }
        public bool OnTheDate
        {
            get => PackNoteModel.Note.DateTimeStatus;
            set
            {
                PackNoteModel.Note.DateTimeStatus = value;
                OnPropertyChanged();
            }
        }
        public Color BackGroundColor
        {
            get => PackNoteModel.BackGroundColor;
            set
            {
                PackNoteModel.BackGroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color LineColor
        {
            get => PackNoteModel.LineColor;
            set
            {
                PackNoteModel.LineColor = value;
                OnPropertyChanged();
            }
        }
        public bool HasSmallTasks => PackNoteModel.SmallTasks.Count() > 0;
        public bool Notify
        {
            get => PackNoteModel.Note.Notify;
            set
            {
                if (PackNoteModel.Note.Notify != value)
                {
                    PackNoteModel.Note.Notify = value;
                    if (!value)
                    {
                        SelectedRepead = CustomRepeads.RepeadsItems[0];
                        ExpanderExpanded = value;
                    }
                    OnPropertyChanged();
                }
            }
        }
        private bool _expanderExpanded;
        public bool ExpanderExpanded
        {
            get => _expanderExpanded;
            set
            {
                if (_expanderExpanded != value)
                {
                    _expanderExpanded = value;
                    OnPropertyChanged();
                }
            }
        }
        public RepeadItem SelectedRepead
        {
            get => _selectedRepead;
            set 
            {
                if (_selectedRepead != value)
                {
                    _selectedRepead = value;
                    PackNoteModel.Note.RepeadIdKey = (int)value.RepeadType;
                    OnPropertyChanged();
                }
            }
        }
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => PackNoteModel.SmallTasks;

        public string TaskAddingEntryText { get; set; }
        public string TaskAddingPlaceHolder { get; set; } = "Add little task...";
        #endregion

        private void InicializateSelectedRepead()
        {
            SelectedRepead = CustomRepeads.RepeadsItems[PackNoteModel.Note.RepeadIdKey];
        }

        private void AssigmentCommands(IEnumerable<SmallTaskViewModel> smallTaskViewModels)
        {
            foreach (SmallTaskViewModel smallTaskViewModel in smallTaskViewModels)
                AssigmentCommands(smallTaskViewModel);
        }
        private void AssigmentCommands(SmallTaskViewModel smallTaskViewModel)
        {
            smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
        }

        private void SaveCommanHandling()
        {
            if (string.IsNullOrWhiteSpace(Header) && string.IsNullOrWhiteSpace(DopText))
                return;

            CreatedDateTime = DateTime.Now;
            if (OnTheDate == false)
                ReminderDateTime = CreatedDateTime;

            PackNoteDBManager packNoteManager = new PackNoteDBManager();
            packNoteManager.Save(PackNoteModel, correct: true);

            if (OnTheDate && Notify)
            {
                NotifyOnAppManager notyfyManager = new NotifyOnAppManager();
                notyfyManager.SendNotify(PackNoteModel);
            }

            SavePressed?.Invoke();

            ReturnNavigationPageAsync();
        }
        private void AddTask()
        {
            if (string.IsNullOrWhiteSpace(TaskAddingEntryText))
            {
                return;
            }
            string taskName = TaskAddingEntryText;
            TaskAddingEntryText = string.Empty;
            var smallTaskViewModel = new SmallTaskViewModel { Text = taskName.Trim() };
            PackNoteModel.AddSmallTask(smallTaskViewModel);
            //Нужно один объект
            AssigmentCommands(smallTaskViewModel);
            OnPropertyChanged(nameof(HasSmallTasks));
            OnPropertyChanged(nameof(SmallTasks));
            OnPropertyChanged(nameof(TaskAddingEntryText));
        }
        private async void AddNewTask()
        {
            EntryView.ResultText result = await Navigation.ShowPopupAsync(
                new EntryView(headerText: EntryViewResource.HeaderLabel,
                              editorPlaceholder: EntryViewResource.EditorPlaceHolder,
                              cancelText: EntryViewResource.CancelButtonText,
                              agreementText: EntryViewResource.AddButtonText,
                              size: new Size(300, 220)));
            if (result is EntryView.ResultText resulText && !resulText.IsCanceled && !string.IsNullOrEmpty(resulText.Value))
            {
                var smallTaskViewModel = new SmallTaskViewModel { Text = result.Value.Trim() };
                PackNoteModel.AddSmallTask(smallTaskViewModel);
                //Нужно один объект
                AssigmentCommands(smallTaskViewModel);
                OnPropertyChanged(nameof(HasSmallTasks));
                OnPropertyChanged(nameof(SmallTasks));
            }
        }
        private void DeleteTask(SmallTaskViewModel task)
        {
            PackNoteModel.DeleteSmallTask(task);
            OnPropertyChanged(nameof(SmallTasks));
            OnPropertyChanged(nameof(HasSmallTasks));
        }

        private void ShowColorSelectionPage()
        {
            if (ColorSelectionPage.IsPageOpened)
                return;

            IColorSelection colorSelectionPage = new ColorSelectionPage(new PackNoteViewModel(PackNoteModel));
            colorSelectionPage.BackGround.ColorSelected += (Color color) => BackGroundColor = color;
            colorSelectionPage.Line.ColorSelected += (Color color) => LineColor = color;
            Navigation.ShowColorSelection((ColorSelectionPage)colorSelectionPage);
        }
        private async void ShowViewPackNote()
        {
            if (DemonstrationViewPackNote.isPageOpened == false)
                await Navigation.PushPopupAsync(new DemonstrationViewPackNote(new PackNoteViewModel(PackNoteModel)));
        }
        private async void ShowAvailableRepeads()
        {
            await Navigation.ShowAvailableRepeadsAsync(OnSelectedItemChangedAction, SelectedRepead);

            void OnSelectedItemChangedAction(object sender, RadioButtonItem selectedItem)
            {
                SelectedRepead = (RepeadItem)selectedItem;
            }
        }
        private async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();
    }
}
