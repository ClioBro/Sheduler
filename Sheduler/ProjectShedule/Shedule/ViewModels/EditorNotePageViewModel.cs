using ProjectShedule.Core;
using ProjectShedule.Core.Interfaces;
using ProjectShedule.Core.Notify;
using ProjectShedule.Core.RadioButton;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Language.Resources.PopUp.Repeads;
using ProjectShedule.PopUpAlert;
using ProjectShedule.PopUpAlert.ColorSelection;
using ProjectShedule.PopUpAlert.ColorSelection.Models;
using ProjectShedule.PopUpAlert.ColorSelection.ViewModels;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ProjectShedule.Shedule.ViewModels
{
    public class EditorNotePageViewModel : BaseViewModel
    {
        public delegate void NoteEventHandler(EditorNotePageViewModel sender, NoteSavedEventArgs editorNoteSavePressedEventArgs);
        public event NoteEventHandler SavePressed;

        private string _taskAddingEntryText;
        private readonly EditNoteViewModel _editNoteViewModel;
        private readonly bool isNewNote;

        public EditorNotePageViewModel() : this(new Note())
        {
            isNewNote = true;
        }
        public EditorNotePageViewModel(Note note)
        {
            _editNoteViewModel = new EditNoteViewModel(note);
            _editNoteViewModel.DeletionConfirmationSmallTask += Confirmation;
            SelectedNotifyRepeat = NotifyRepeats[_editNoteViewModel.RepeatIdKey];
            InicializationCommands();
        }

        #region Properties
        public EditNoteViewModel EditNoteViewModel => _editNoteViewModel;
        public NotifyRepeat[] NotifyRepeats => NotifyRepeatCollections.NotifyRepeats;
        public NotifyRepeat SelectedNotifyRepeat
        {
            get => NotifyRepeats[_editNoteViewModel.RepeatIdKey];
            set
            {
                if (value == NotifyRepeats[_editNoteViewModel.RepeatIdKey])
                    return;
                _editNoteViewModel.RepeatIdKey = NotifyRepeats.IndexOf(value);
                OnPropertyChanged();
            }
        }
        public DateTime? Date
        {
            get => EditNoteViewModel.AppointmentDate.Value;
            set
            {
                var time = EditNoteViewModel.AppointmentDate.Value.TimeOfDay;
                EditNoteViewModel.AppointmentDate = value += time;
            }
        }
        public TimeSpan Time
        {
            get => EditNoteViewModel.AppointmentDate.Value.TimeOfDay;
            set
            {
                var date = EditNoteViewModel.AppointmentDate.Value.Date;
                EditNoteViewModel.AppointmentDate = date += value;
            }
        }
        public virtual string TaskAddingEntryText
        {
            get => _taskAddingEntryText;
            set
            {
                _taskAddingEntryText = value;
                OnPropertyChanged();
            }
        }
        public virtual bool EnableNotify
        {
            get => _editNoteViewModel.Notify;
            set
            {
                if (_editNoteViewModel.Notify == value)
                    return;
                _editNoteViewModel.Notify = value;
                OnPropertyChanged();
            }
        }
        public INavigation Navigation { get; set; }
        public bool IsNewNote => isNewNote;

        public Color BorderBoxColor => EditNoteViewModel.LineColor;
        public Color BackGroundBoxColor => EditNoteViewModel.BackGroundColor;
        #endregion

        #region Commands
        public ICommand SavePackNoteCommand { get; protected set; }
        public ICommand AddTaskCommand { get; protected set; }
        public ICommand DeleteTaskCommand { get; protected set; }
        public ICommand ShowPackNoteViewCommand { get; protected set; }
        public ICommand ShowColorSelectionPageCommand { get; protected set; }
        public ICommand ShowAvailableRepeatTypesCommand { get; protected set; }
        #endregion

        #region CommandsHandlers
        private void AddTask()
        {
            if (string.IsNullOrWhiteSpace(TaskAddingEntryText))
                return;
            _editNoteViewModel.CreateSmallTaskCommand.Execute(TaskAddingEntryText);
            TaskAddingEntryText = string.Empty;
            OnPropertyChanged(nameof(TaskAddingEntryText));
        }
        private async void ShowPackNoteView()
        {
            if (DemonstrationViewPackNote.isPageOpened)
                return;

            INoteViewModel noteViewModel = _editNoteViewModel;
            Note note = noteViewModel.GetData();

            Rg.Plugins.Popup.Pages.PopupPage popUpPage =
                new DemonstrationViewPackNote(new NoteViewModel(note));

            await Navigation.PushPopupAsync(popUpPage);
        }
        private async void ShowColorSelectionPageAcync()
        {
            if (NoteColorSelectionPage.IsPageOpened)
                return;

            INoteViewModel noteViewModel = _editNoteViewModel;
            Note note = noteViewModel.GetData();

            var noteVM = new NoteViewModel(note);
            // ---
            List<Color> colors = new List<Color>(CustomColorsOnSelector.Colors)
            {
                noteVM.DefaulBackgroundColor, noteVM.DefaulLineColor
            };
            var colorsSelectingBoxViewModel = new ColoredMarksSelectorViewModel(new ColorsModel(colors))
            {
                SelectedColor = noteVM.LineColor
            };
            NoteColorSelectionPageViewModel colorSelectionViewModel = new NoteColorSelectionPageViewModel(noteVM, colorsSelectingBoxViewModel);
            noteVM.PropertyChanged += (sender, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(NoteViewModel.LineColor):
                    case nameof(NoteViewModel.BackGroundColor):
                        OnPropertyChanged(nameof(BorderBoxColor), nameof(BackGroundBoxColor));
                        break;
                }
            };
            var page = new NoteColorSelectionPage(colorSelectionViewModel);

            await Navigation.ShowColorSelection(page);
        }
        private async void ShowAvailableRepeats()
        {
            if (RadioButtonsSelecterPage.IsPageOpened)
                return;

            RadioButtonRepeatItem[] repeatItems = BuildRepeatNotifyButtons();
            var selectedItemIndex = repeatItems.IndexOf(repeatItems.First(rb => rb.NotifyRepeat.RepeatType == SelectedNotifyRepeat.RepeatType));

            var radioButtonPage = new RadioButtonsSelecterPage(new RadioButtonsSelecterPageViewModel(
                title: Repeads.HeaderLabel,
                items: repeatItems,
                selectedItemIndex: selectedItemIndex,
                itemSelectedCommand: new Command<IRadioButtonItem>(SelectedCallBack)));

            await Navigation.ShowAvailableRepeatsAsync(radioButtonPage);
        }
        private void SaveCommandHandler()
        {
            _editNoteViewModel.CreatedDateTime = DateTime.Now;

            IEnumerable<IHasData<SmallTask>> oldRemovedSmallTasks = _editNoteViewModel.RemovedOldSmallTasks;

            SavePressed?.Invoke(this, new NoteSavedEventArgs(_editNoteViewModel, oldRemovedSmallTasks));
            ReturnNavigationPageAsync();
        }
        private void SelectedCallBack(IRadioButtonItem radioButtonItem)
        {
            RadioButtonRepeatItem result = ((RadioButtonRepeatItem)radioButtonItem);
            SelectedNotifyRepeat = result.NotifyRepeat;
        }
        #endregion

        private async Task<bool> Confirmation(DeletableSmallTaskViewModel deletableSmallTaskViewModel)
        {
            bool result = await Navigation.ShowQuestionForDeletionAsync(deletableSmallTaskViewModel.Header);
            return result;
        }
        private void InicializationCommands()
        {
            AddTaskCommand = new Command(AddTask);
            ShowColorSelectionPageCommand = new Command(ShowColorSelectionPageAcync);
            ShowPackNoteViewCommand = new Command(ShowPackNoteView);
            ShowAvailableRepeatTypesCommand = new Command(ShowAvailableRepeats);
            SavePackNoteCommand = new Command(SaveCommandHandler);
        }
        private void OnSmallTaskCollectionChanged(object sender, NotifyCollectionChangedEventArgs eventArgs)
        {
            if (eventArgs.Action != NotifyCollectionChangedAction.Add)
                return;
            TaskAddingEntryText = string.Empty;
        }
        private RadioButtonRepeatItem[] BuildRepeatNotifyButtons()
        {
            return NotifyRepeats
                .Select(notifyRepeat => new RadioButtonRepeatItem(notifyRepeat))
                .ToArray();
        }
        protected async void ReturnNavigationPageAsync() => await Navigation.PopModalAsync();
    }
}