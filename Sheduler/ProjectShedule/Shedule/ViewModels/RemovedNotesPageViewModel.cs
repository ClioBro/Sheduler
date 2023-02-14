using ProjectShedule.Core.Search;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder;
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    internal partial class RemovedNotesPageViewModel
    {
        private readonly ObservableRangeCollection<ThrashNoteViewModel> _notes;
        private readonly ThrashNoteViewModelBuilder _thrashNoteViewModelBuilder;
        private readonly RemovedContentModel _removedContentModel;
        private readonly SimpleNoteFilterViewModel _notesFilterViewModel;
        private readonly SearchBarTitleViewModel _serchBarViewModel;

        private ICommand _removePackNoteCommand;
        private ICommand _revivePackNoteCommand;

        public RemovedNotesPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            var db = App.ApplicationContext.UtilityExtendedDeadNoteRepository;
            _removedContentModel = new RemovedContentModel(db);
            _notesFilterViewModel = new SimpleNoteFilterViewModel();
            _notesFilterViewModel.PropertyChanged += OnNotesFilterViewModel_PropertyChanged;
            _notes = new ObservableRangeCollection<ThrashNoteViewModel>();
            _thrashNoteViewModelBuilder = new ThrashNoteViewModelBuilder();

            _serchBarViewModel = new RecycleBinSearchBarTitleViewModel
            {
                SerchCommand = new Command<string>(SearchCommandHandler)
            };

            InitializationCommands();
            SetCommandsInBuilder();
            InicializationNotes();
        }

        public INavigation Navigation { get; }
        public ObservableRangeCollection<ThrashNoteViewModel> PackNotes => _notes;
        public SimpleNoteFilterViewModel NotesFilterViewModel => _notesFilterViewModel;
        public SearchBarTitleViewModel SerchBarTitleViewModel => _serchBarViewModel;
        public string Title => Language.Resources.Pages.AppFlyout.Lobby.RecycleBinTitle;

        private void SearchCommandHandler(string text)
        {
            IEnumerable<Note> notes = _removedContentModel.GetAllContent().Where(n => n.Header.Contains(text));
            UpdateBy(notes);
        }
        private void OnNotesFilterViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(NotesFilterViewModel.Descending):
                case nameof(NotesFilterViewModel.CurrentSortInOrderNote):
                    UpdateBy(_removedContentModel.GetAllContent());
                    break;
            }
        }

        private void InitializationCommands()
        {
            _removePackNoteCommand = new Command<ThrashNoteViewModel>(TryDeleteNoteCommandHandler);
            _revivePackNoteCommand = new Command<ThrashNoteViewModel>(ReviveNoteCommandHanlder);
        }
        private void SetCommandsInBuilder()
        {
            _thrashNoteViewModelBuilder.SetReviveCommand(_revivePackNoteCommand);
            _thrashNoteViewModelBuilder.SetDeleteCommand(_removePackNoteCommand);
        }

        private void InicializationNotes()
        {
            #region TestInicialization

            //IEnumerable<Note> notes = new List<Note>()
            //{
            //    new Note()
            //    {
            //        Header = "NotDeleted",
            //        SmallTasks = new List<SmallTask>
            //        {
            //            new SmallTask() { Header = "1.1d", DeletedDateTime = DateTime.Now },
            //            new SmallTask() { Header = "1.2d", DeletedDateTime = DateTime.Now },
            //            new SmallTask() { Header = "1.3d", DeletedDateTime = DateTime.Now },
            //        }
            //    },
            //    new Note()
            //    {
            //        Header = "Deleted1",
            //        DeletedDateTime = DateTime.Now,
            //        SmallTasks = new List<SmallTask>
            //        {
            //            new SmallTask() { Header = "2.1n" },
            //            new SmallTask() { Header = "2.2d", DeletedDateTime = DateTime.Now },
            //            new SmallTask() { Header = "2.3d", DeletedDateTime = DateTime.Now }
            //        }
            //    },
            //    new Note()
            //    {
            //        Header = "Deleted2",
            //        DeletedDateTime = DateTime.Now,
            //        SmallTasks = new List<SmallTask>
            //        {
            //            new SmallTask() { Header = "3.1n" },
            //            new SmallTask() { Header = "3.2n" },
            //            new SmallTask() { Header = "3.3d", DeletedDateTime = DateTime.Now }
            //        }
            //    }
            //};
            //UpdateBy(notes);
            #endregion TestInicialization

            UpdateBy(_removedContentModel.GetAllContent());
        }
        private void UpdateBy(IEnumerable<Note> notes)
        {
            IEnumerable<Note> filtredNotes = _notesFilterViewModel.SortNoteInOrder(notes);
            List<ThrashNoteViewModel> thrashNoteViewModels = new List<ThrashNoteViewModel>();

            foreach (Note note in filtredNotes)
            {
                ThrashNoteViewModel thrashNoteViewModel = _thrashNoteViewModelBuilder
                    .SetData(note)
                    .Build();
                thrashNoteViewModels.Add(thrashNoteViewModel);
            }
            PackNotes.ReplaceRange(thrashNoteViewModels);
            AssigmentEvents(PackNotes);
        }
        private void AssigmentEvents(IEnumerable<ThrashNoteViewModel> ThrashNoteViewModels)
        {
            foreach (ThrashNoteViewModel ThrashNoteViewModel in ThrashNoteViewModels)
                AssigmentEvents(ThrashNoteViewModel);
        }
        private void AssigmentEvents(ThrashNoteViewModel thrashNoteViewModel)
        {
            thrashNoteViewModel.SmallTaskRevivePressed += OnSmallTaskRevivePressed;
            thrashNoteViewModel.SmallTaskDeletePressed += OnSmallTaskDeletePressed;
        }

        #region EventHanlders
        private void OnSmallTaskDeletePressed(
            NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel<ThrashSmallTaskViewModel> thrashNoteViewModel,
            ThrashSmallTaskViewModel thrashSmallTaskViewModel)
        {
            _removedContentModel.Delete(thrashSmallTaskViewModel);
            TryDeleteThrashNote((ThrashNoteViewModel)thrashNoteViewModel);
        }
        private void OnSmallTaskRevivePressed(ThrashNoteViewModel sender, ThrashSmallTaskViewModel thrashSmallTask)
        {
            _removedContentModel.Revive(thrashSmallTask);
            TryDeleteThrashNote(sender);
        }
        #endregion EventHanlders

        private void TryDeleteThrashNote(ThrashNoteViewModel thrashNoteViewModel)
        {
            if (thrashNoteViewModel.CanDeleteMe)
                RemoveInCollection(thrashNoteViewModel);
        }

        #region CommandsHandlers
        private void TryDeleteNoteCommandHandler(ThrashNoteViewModel thrashNoteViewModel)
        {
            _removedContentModel.Delete(thrashNoteViewModel);
            RemoveInCollection(thrashNoteViewModel);
        }
        private void ReviveNoteCommandHanlder(ThrashNoteViewModel thrashNoteViewModel)
        {
            _removedContentModel.Revive(thrashNoteViewModel);
            RemoveInCollection(thrashNoteViewModel);
        }
        #endregion CommandsHandlers

        private void RemoveInCollection(ThrashNoteViewModel thrashNoteViewModel)
        {
            PackNotes.Remove(thrashNoteViewModel);
        }
    }
}