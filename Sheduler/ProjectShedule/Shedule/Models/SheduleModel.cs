using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.Calendar.Models;
using ProjectShedule.Shedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.PackNotesManager;
using ProjectShedule.Shedule.PackNotesManager.FilterManager;
using ProjectShedule.Shedule.ShapeEvents;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.Models
{
    public abstract class BaseFilterViewModel : INotifyPropertyChanged
    {
        public abstract event PropertyChangedEventHandler PropertyChanged;
        public abstract SortInDate SelectedFlter { get; set; }
        public abstract PutInOrderNote SelectedOrder { get; set; }
        public SortInDate[] FilterTypes { get; set; }
        public PutInOrderNote[] OrderTypes { get; set; }
        public abstract void SetBySelectedDate(DateTime dateTime);
        public abstract void SetByToday();
        public abstract IEnumerable<IPackNote> GetFiltered();
    }
    public class NoteFilterViewModel : BaseFilterViewModel
    {
        private readonly FilterPackNoteModel _filterPackNote;
        public override event PropertyChangedEventHandler PropertyChanged;
        public NoteFilterViewModel(IGetQuereblyItems<IPackNote> getItems)
        {
            FilterTypes = new SortInDate[]
            {
                new SelectedSortInDate(getItems) { Text = Filters.BySelectedDate},
                new ToDaySortInDate(getItems) { Text = Filters.ByToday},
                new AllSortInDate(getItems) { Text = Filters.AllItems},
            };

            OrderTypes = new PutInOrderNote[]
            {
                new PutInOrderNoteByDate{ Text = Filters.ByDate},
                new PutInOrderNoteByAlphabet{ Text = Filters.ByAlphabetically},
            };

            _filterPackNote = new FilterPackNoteModel(OrderTypes[0], FilterTypes[0]);

            SelectedFlter.IsChecked = true;
            SelectedOrder.IsChecked = true;
        }
        public override SortInDate SelectedFlter 
        {
            get => _filterPackNote.SortInDate;
            set
            {
                if (_filterPackNote.SortInDate != value)
                {
                    _filterPackNote.SortInDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public override PutInOrderNote SelectedOrder 
        {
            get => _filterPackNote.PutInOrder;
            set
            {
                if (_filterPackNote.PutInOrder != value)
                {
                    _filterPackNote.PutInOrder = value;
                    OnPropertyChanged();
                }
            }
        }
        public override void SetBySelectedDate(DateTime dateTime)
        {
            SortInDate tempSelectedFilter = SelectedFlter;

            if ((tempSelectedFilter is SelectedSortInDate) == false)
                tempSelectedFilter = FilterTypes.FirstOrDefault(sortInDate => sortInDate is SelectedSortInDate);

            ((SelectedSortInDate)tempSelectedFilter).Date = dateTime;
            SelectedFlter = tempSelectedFilter;
        }
        public override void SetByToday()
        {
            SelectedFlter = FilterTypes.FirstOrDefault(sortInDate => sortInDate is ToDaySortInDate);
        }
        public override IEnumerable<IPackNote> GetFiltered() => _filterPackNote.GetFiltered();
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
    public abstract class BaseSheduleModel
    {
        #region Delegates
        public delegate void BasePackNoteViewModelDelegate(BasePackNoteViewModel packNoteViewModel);
        #endregion

        #region Events
        public event Action SelectedPackNotesChanged;
        #endregion

        #region Fields
        protected IPackNoteDataBaseController _packNoteDataBaseController;

        protected ObservableRangeCollection<BasePackNoteViewModel> _packNotes;
        protected ReadOnlyObservableCollection<BasePackNoteViewModel> _readOnlyPackNotes;

        protected ObservableRangeCollection<CircleEventModel> _calendarCircleEvents;
        protected ReadOnlyObservableCollection<CircleEventModel> _readOnlyCircleEvents;

        protected ObservableRangeCollection<BasePackNoteViewModel> _selectedPackNotes;
        protected ObservableCollection<DateTime> _selectedDates;

        protected DateTime _displayedDateOnCarousel;

        protected BaseFilterViewModel _filterPackNotes;
        protected IBuilderCalendarCircleEvent _builderCircleEvent;
        protected IBuilderPackNoteViewModel _builderPackNoteViewModel;
        #endregion

        #region Properties
        public ICommand DeletePackNoteCommand { get; set; }
        public ICommand EditPackNoteCommand { get; set; }

        public ICommand DeleteTaskCommand { get; set; }
        public ICommand TaskCheckCommand { get; set; }
        public BaseFilterViewModel FilterPackNotes => _filterPackNotes;
        public ReadOnlyObservableCollection<BasePackNoteViewModel> PackNotes => _readOnlyPackNotes;
        public ReadOnlyObservableCollection<CircleEventModel> CalendarCircleEvents => _readOnlyCircleEvents;
        public ObservableRangeCollection<BasePackNoteViewModel> SelectedPackNotes => _selectedPackNotes;
        public ObservableCollection<DateTime> SelectedDates => _selectedDates;
        public abstract DateTime DisplayedDateOnCarousel { get; set; }
        #endregion
        public abstract void UpdatePackNotesAsync();
        public abstract void UpdatePackNotes();
        public abstract void UpdateEvents();
        public abstract void DeletePackNote(BasePackNoteViewModel packNoteViewModel);
        public abstract void SavePackNote(BasePackNoteViewModel packNote);
    }
    public partial class SheduleModel : BaseSheduleModel
    {
        public SheduleModel(IPackNoteDataBaseController packNoteDataBaseController, IBuilderPackNoteViewModel builderPackNoteViewModel)
        {
            _packNoteDataBaseController = packNoteDataBaseController;
            _builderPackNoteViewModel = builderPackNoteViewModel;
            FieldsInicialization();

            var noteFilterVM = new NoteFilterViewModel(_packNoteDataBaseController); 
            noteFilterVM.PropertyChanged += (sender, e) => UpdatePackNotesAsync();
            _filterPackNotes = noteFilterVM;

            DisplayedDateOnCarousel = DateTime.Today;
        }
        public override DateTime DisplayedDateOnCarousel
        {
            get => _displayedDateOnCarousel;
            set
            {
                _displayedDateOnCarousel = value;
                if (value != DateTime.Today)
                    FilterPackNotes.SetBySelectedDate(value);
                else
                    FilterPackNotes.SetByToday();
            }
        }
        
        public override void UpdatePackNotesAsync()
        {
            Task.Run(() => 
            {
                UpdatePackNotes();
                UpdateEvents();
            });
        }
        public override void UpdatePackNotes()
        {
            List<BasePackNoteViewModel> tempNotes = new List<BasePackNoteViewModel>();
            foreach (BasePackNoteModel packNoteModel in _filterPackNotes.GetFiltered())
            {
                BasePackNoteViewModel basePackNoteVM = _builderPackNoteViewModel
                    .SetDeleteMeCommand(DeletePackNoteCommand)
                    .SetEditMeCommand(EditPackNoteCommand)
                    .SetModel(packNoteModel)
                    .Build();

                basePackNoteVM.TaskCheckChanged += SmallTaskCheckChangedEventHandler;
                basePackNoteVM.TaskDeletePressed += SmallTaskDeletedEventHandler;

                tempNotes.Add(basePackNoteVM);
            }
            _packNotes.ReplaceRange(tempNotes);
        }
        public override void UpdateEvents()
        {
            _calendarCircleEvents.ReplaceRange(BuildCircleEvents());
        }
        public override void DeletePackNote(BasePackNoteViewModel packNoteViewModel)
        {
            if (_packNotes.Remove(packNoteViewModel))
            {
                IHasModel<BasePackNoteModel> hasModel = packNoteViewModel;
                _packNoteDataBaseController.Delete(hasModel.Model);
                UpdateEvents();
            }
        }
        public override void SavePackNote(BasePackNoteViewModel packNoteViewModel)
        {
            IHasModel<BasePackNoteModel> hasModel = packNoteViewModel;
            _packNoteDataBaseController.Save(hasModel.Model);
        }

        private void SmallTaskCheckChangedEventHandler(BaseSmallTaskViewModel hasSmallTask)
        {
            _packNoteDataBaseController.PartsControl.SaveInDataBase(((IHasModel<BaseSmallTask>)hasSmallTask).Model);
        }
        private void SmallTaskDeletedEventHandler(BaseSmallTaskViewModel hasSmallTask)
        {
            _packNoteDataBaseController.PartsControl.DeleteInDataBase(((IHasModel<BaseSmallTask>)hasSmallTask).Model);
        }
        private void FieldsInicialization()
        {
            _displayedDateOnCarousel = DateTime.Today;
            _packNotes = new ObservableRangeCollection<BasePackNoteViewModel>();
            _readOnlyPackNotes = new ReadOnlyObservableCollection<BasePackNoteViewModel>(_packNotes);
            _calendarCircleEvents = new ObservableRangeCollection<CircleEventModel>();
            _readOnlyCircleEvents = new ReadOnlyObservableCollection<CircleEventModel>(_calendarCircleEvents);
            _selectedPackNotes = new ObservableRangeCollection<BasePackNoteViewModel>();
            _selectedDates = new ObservableCollection<DateTime>();
            _builderCircleEvent = new BuilderCalendarCircleEvents(_packNoteDataBaseController.PartsControl.NoteRepository);
        }
        private IEnumerable<CircleEventModel> BuildCircleEvents()
        {
            DateTime minDate = new DateTime(_displayedDateOnCarousel.Year, _displayedDateOnCarousel.Month, _displayedDateOnCarousel.Day).AddMonths(-1).AddDays(-5);
            DateTime maxDate = new DateTime(_displayedDateOnCarousel.Year, _displayedDateOnCarousel.Month, _displayedDateOnCarousel.Day).AddMonths(1).AddDays(12);

            return _builderCircleEvent.Build(minDate, maxDate);
        }
    }
}
