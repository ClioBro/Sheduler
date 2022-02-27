using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.Shedule.Editor.Models;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Editor.ViewModels
{
    public class EditorPackNoteVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private readonly EditorPackNoteModel _editorModel;
        private RepeadItem _selectedRepead;
        public Action SavedActionCallBack;
        public EditorPackNoteVM() : this(new EditorPackNoteModel()) { }
        public EditorPackNoteVM(EditorPackNoteModel editorPackNoteModel)
        {
            _editorModel = editorPackNoteModel;

            SavePackNoteCommand = new Command(Save);
            AddTaskCommand = new Command(AddTask);
            ShowPackNoteViewCommand = new Command(ShowViewPackNote);
            ShowColorSelectionPageCommand = new Command(ShowColorSelectionPageAcync);
            ShowAvailableRepeadTypesCommand = new Command(ShowAvailableRepeads);
            DeleteTaskCommand = new Command<SmallTaskViewModel>(DeleteTask);

            _editorModel.SelectedRepeadChanged += OnSelectedChanged;
            _editorModel.SmallTasksChanged += OnSmallTasksChangedHandler;

            _editorModel.PackNoteSaved += SavedHandler;
        }

       
        #region Commands
        public ICommand SavePackNoteCommand { get; private protected set; }
        public ICommand AddTaskCommand { get; private protected set; }
        public ICommand DeleteTaskCommand { get; private protected set; }
        public ICommand ShowPackNoteViewCommand { get; private protected set; }
        public ICommand ShowColorSelectionPageCommand { get; private protected set; }
        public ICommand ShowAvailableRepeadTypesCommand { get; private protected set; }
        #endregion

        #region Properties
        public string Header
        {
            get => _editorModel.Header;
            set
            {
                _editorModel.Header = value;
                OnPropertyChanged();
            }
        }
        public string DopText
        {
            get => _editorModel.DopText;
            set
            {
                _editorModel.DopText = value;
                OnPropertyChanged();
            }
        }
        public DateTime CreatedDateTime
        {
            get => _editorModel.CreatedDateTime;
            set
            {
                _editorModel.CreatedDateTime = value;
                OnPropertyChanged();
            }
        }
        public DateTime ReminderDateTime
        {
            get => _editorModel.AppointmentDate;
            set
            {
                _editorModel.AppointmentDate = value;
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
            get => _editorModel.OnTheDate;
            set
            {
                _editorModel.OnTheDate = value;
                OnPropertyChanged();
            }
        }
        public Color BackGroundColor
        {
            get => _editorModel.BackGroundColor;
            set
            {
                _editorModel.BackGroundColor = value;
                OnPropertyChanged();
            }
        }
        public Color LineColor
        {
            get => _editorModel.LineColor;
            set
            {
                _editorModel.LineColor = value;
                OnPropertyChanged();
            }
        }
        public bool HasSmallTasks => _editorModel.SmallTasks.Count() > 0;
        public bool Notify
        {
            get => _editorModel.Notify;
            set
            {
                if (_editorModel.Notify != value)
                {
                    _editorModel.Notify = value;
                    if (!value)
                    {
                        SelectedRepead = CustomRepeads.RepeadsItems[0];
                        //ExpanderExpanded = value;
                    }
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
                    _editorModel.SelectedRepead = value;
                    OnPropertyChanged();
                }
            }
        }
        public string SelectedRepeadText => _editorModel.SelectedRepead.Text;
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => _editorModel.SmallTasks;

        public string TaskAddingEntryText { get => _editorModel.TaskAddingEntryText; set => _editorModel.TaskAddingEntryText = value; }
        #endregion
        public INavigation Navigation { get; set; }

        private void AddTask()
        {
            _editorModel.AddTask();
        }
        private void DeleteTask(SmallTaskViewModel smallTask)
        {
            _editorModel.RemoveTask(smallTask);
        }
        private async void ShowViewPackNote()
        {
            if (DemonstrationViewPackNote.isPageOpened)
                return;
            
            await Navigation.PushPopupAsync(_editorModel.GetDemonstrationPage());
        }
        private async void ShowColorSelectionPageAcync()
        {
            if (ColorSelectionPage.IsPageOpened)
                return;
            
            await Navigation.ShowColorSelection(_editorModel.GetColorSelectionPage());
        }
        private async void ShowAvailableRepeads()
        {
            if (RadioButtonsSelecterPage.IsPageOpened)
                return;

            await Navigation.ShowAvailableRepeadsAsync(_editorModel.GetRadioButtonSelectedPage());
        }
        private void Save()
        {
            _editorModel.Save();
        }
        private async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();

        private void SavedHandler(object sender, bool saved)
        {
            if (saved) 
            {
                ReturnNavigationPageAsync();
                SavedActionCallBack?.Invoke();
            }
        }
        private void OnSmallTasksChangedHandler(object sender, ReadOnlyObservableCollection<SmallTaskViewModel> e)
        {
            OnPropertyChanged(nameof(SmallTasks));
            OnPropertyChanged(nameof(TaskAddingEntryText));
        }
        private void OnSelectedChanged(object sender, RepeadItem e)
        {
            OnPropertyChanged(nameof(SelectedRepeadText));
        }


    }
    //public partial class EditorPackNoteViewModel : INotifyPropertyChanged
    //{
    //    #region INotifyPropertyChanged
    //    public event PropertyChangedEventHandler PropertyChanged;
    //    protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
    //    {
    //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    //    }
    //    #endregion

    //    #region Commands
    //    public ICommand SavePackNoteCommand { get; private protected set; }
    //    public ICommand AddTaskCommand { get; private protected set; }
    //    public ICommand DeleteTaskCommand { get; private protected set; }
    //    public ICommand ShowPackNoteViewCommand { get; private protected set; }
    //    public ICommand ShowColorSelectionPageCommand { get; private protected set; }
    //    public ICommand ShowAvailableRepeadTypesCommand { get; private protected set; }
    //    #endregion

    //    public event Action SavePressed;
    //    public INavigation Navigation { get; set; }

    //    private RepeadItem _selectedRepead;
    //    private readonly PackNoteModel PackNoteModel;

    //    public EditorPackNoteViewModel(): this (new PackNoteModel()) {}
    //    public EditorPackNoteViewModel(PackNoteModel packNoteModel)
    //    {
    //        PackNoteModel = packNoteModel;

    //        SavePackNoteCommand = new Command(SaveCommanHandling);
    //        AddTaskCommand = new Command(AddTask);
    //        ShowPackNoteViewCommand = new Command(ShowViewPackNote);
    //        ShowColorSelectionPageCommand = new Command(ShowColorSelectionPageAcync);
    //        ShowAvailableRepeadTypesCommand = new Command(ShowAvailableRepeads);
    //        DeleteTaskCommand = new Command<SmallTaskViewModel>(DeleteTask);

    //        AssigmentCommands(PackNoteModel.SmallTasks);

    //        InicializateSelectedRepead();
    //    }

    //    #region Properties
    //    public int Id
    //    {
    //        get => PackNoteModel.Note.Id;
    //        set
    //        {
    //            PackNoteModel.Note.Id = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public string Header
    //    {
    //        get => PackNoteModel.Note.Header;
    //        set
    //        {
    //            PackNoteModel.Note.Header = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public string DopText
    //    {
    //        get => PackNoteModel.Note.DopText;
    //        set
    //        {
    //            PackNoteModel.Note.DopText = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public DateTime CreatedDateTime
    //    {
    //        get => PackNoteModel.Note.CreatedDateTime;
    //        set
    //        {
    //            PackNoteModel.Note.CreatedDateTime = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public DateTime ReminderDateTime
    //    {
    //        get => PackNoteModel.Note.AppointmentDate;
    //        set
    //        {
    //            PackNoteModel.Note.AppointmentDate = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public DateTime Date
    //    {
    //        get => ReminderDateTime.Date;
    //        set
    //        {
    //            var time = ReminderDateTime.TimeOfDay;
    //            ReminderDateTime = value += time;
    //        }
    //    }
    //    public TimeSpan Time 
    //    {
    //        get => ReminderDateTime.TimeOfDay;
    //        set 
    //        {
    //            var date = ReminderDateTime.Date;
    //            ReminderDateTime = date += value;
    //        }
    //    }
    //    public bool OnTheDate
    //    {
    //        get => PackNoteModel.Note.DateTimeStatus;
    //        set
    //        {
    //            PackNoteModel.Note.DateTimeStatus = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public Color BackGroundColor
    //    {
    //        get => PackNoteModel.BackGroundColor;
    //        set
    //        {
    //            PackNoteModel.BackGroundColor = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public Color LineColor
    //    {
    //        get => PackNoteModel.LineColor;
    //        set
    //        {
    //            PackNoteModel.LineColor = value;
    //            OnPropertyChanged();
    //        }
    //    }
    //    public bool HasSmallTasks => PackNoteModel.SmallTasks.Count() > 0;
    //    public bool Notify
    //    {
    //        get => PackNoteModel.Note.Notify;
    //        set
    //        {
    //            if (PackNoteModel.Note.Notify != value)
    //            {
    //                PackNoteModel.Note.Notify = value;
    //                if (!value)
    //                {
    //                    SelectedRepead = CustomRepeads.RepeadsItems[0];
    //                    //ExpanderExpanded = value;
    //                }
    //                OnPropertyChanged();
    //            }
    //        }
    //    }

    //    public RepeadItem SelectedRepead
    //    {
    //        get => _selectedRepead;
    //        set 
    //        {
    //            if (_selectedRepead != value)
    //            {
    //                _selectedRepead = value;
    //                PackNoteModel.Note.RepeadIdKey = (int)value.RepeadType;
    //                OnPropertyChanged();
    //            }
    //        }
    //    }
    //    public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => PackNoteModel.SmallTasks;

    //    public string TaskAddingEntryText { get; set; }
    //    public string TaskAddingPlaceHolder { get; set; } 
    //    #endregion

    //    private void InicializateSelectedRepead()
    //    {
    //        SelectedRepead = CustomRepeads.RepeadsItems[PackNoteModel.Note.RepeadIdKey];
    //    }

    //    private void AssigmentCommands(IEnumerable<SmallTaskViewModel> smallTaskViewModels)
    //    {
    //        foreach (SmallTaskViewModel smallTaskViewModel in smallTaskViewModels)
    //            AssigmentCommands(smallTaskViewModel);
    //    }
    //    private void AssigmentCommands(SmallTaskViewModel smallTaskViewModel)
    //    {
    //        smallTaskViewModel.DeleteMeCommand = DeleteTaskCommand;
    //    }

    //    private void SaveCommanHandling()
    //    {
    //        if (string.IsNullOrWhiteSpace(Header) && string.IsNullOrWhiteSpace(DopText))
    //            return;

    //        CreatedDateTime = DateTime.Now;
    //        if (OnTheDate == false)
    //            ReminderDateTime = CreatedDateTime;

    //        //PackNoteDBManager packNoteManager = new PackNoteDBManager();
    //        //packNoteManager.Save(PackNoteModel, correct: true);

    //        //if (OnTheDate && Notify)
    //        //{
    //        //    NotifyOnAppManager notyfyManager = new NotifyOnAppManager();
    //        //    notyfyManager.SendNotify(PackNoteModel);
    //        //}

    //        SavePressed?.Invoke();

    //        ReturnNavigationPageAsync();
    //    }
    //    private void AddTask()
    //    {
    //        if (string.IsNullOrWhiteSpace(TaskAddingEntryText))
    //        {
    //            return;
    //        }
    //        string taskName = TaskAddingEntryText;
    //        TaskAddingEntryText = string.Empty;
    //        var smallTaskViewModel = new SmallTaskViewModel { Text = taskName.Trim() };
    //        PackNoteModel.AddSmallTask(smallTaskViewModel);

    //        AssigmentCommands(smallTaskViewModel);

    //        OnPropertyChanged(nameof(HasSmallTasks));
    //        OnPropertyChanged(nameof(SmallTasks));
    //        OnPropertyChanged(nameof(TaskAddingEntryText));
    //    }
    //    private void DeleteTask(SmallTaskViewModel task)
    //    {
    //        PackNoteModel.DeleteSmallTask(task);
    //        OnPropertyChanged(nameof(SmallTasks));
    //        OnPropertyChanged(nameof(HasSmallTasks));
    //    }

    //    private async void ShowColorSelectionPageAcync()
    //    {
    //        if (ColorSelectionPage.IsPageOpened)
    //            return;

    //        ColorSelectionPageCreation colorSelectionPageCreation = new ColorSelectionPageCreation(PackNoteModel);

    //        colorSelectionPageCreation.ColorSelection.LineTarget.ColorSelected += (object sender, Color color) => LineColor = color;
    //        colorSelectionPageCreation.ColorSelection.BackGroundTarget.ColorSelected += (object sender, Color color) => BackGroundColor = color;
            
    //        ColorSelectionPage colorSelectionPage = colorSelectionPageCreation.Create();

    //        await Navigation.ShowColorSelection(colorSelectionPage);
    //    }
    //    private async void ShowViewPackNote()
    //    {
    //        if (DemonstrationViewPackNote.isPageOpened == false)
    //            await Navigation.PushPopupAsync(new DemonstrationViewPackNote(new PackNoteViewModel(PackNoteModel)));
    //    }
    //    private void ShowAvailableRepeads()
    //    {
    //        //if (RadioButtonsSelecterPage.IsPageOpened)
    //        //    return;
    //        //await Navigation.ShowAvailableRepeadsAsync(OnSelectedItemChangedAction, CustomRepeads.RepeadsItems, SelectedRepead);

    //        //void OnSelectedItemChangedAction(object sender, RadioButtonItem selectedItem)
    //        //{
    //        //    SelectedRepead = (RepeadItem)selectedItem;
    //        //}
    //    }
    //    private async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();
    //}
}
