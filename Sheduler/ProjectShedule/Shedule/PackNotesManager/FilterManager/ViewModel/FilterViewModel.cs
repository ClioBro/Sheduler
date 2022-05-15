using ProjectShedule.Language.Resources.OtherElements;
using ProjectShedule.Shedule.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectShedule.Shedule.PackNotesManager.FilterManager.ViewModel
{
    //public class BaseFilterViewModel : BindableBase<BaseFilterViewModel>
    //{
    //    private readonly FilterPackNoteModel _filterPackNotes;
    //    public BaseFilterViewModel()
    //    {
    //        _filterPackNotes = new FilterPackNoteModel();

    //        FilterTypes = new SortInDate<IPackNote>[]
    //        {
    //            new SelectedSortInDate<IPackNote> { Text = Filters.BySelectedDate},
    //            new ToDaySortInDate<IPackNote> { Text = Filters.ByToday},
    //            new AllSortInDate<IPackNote> { Text = Filters.AllItems},
    //        };

    //        OrderTypes = new PutInOrderNote[]
    //        {
    //            new PutInOrderNoteByDate{ Text = Filters.ByDate},
    //            new PutInOrderNoteByAlphabet{ Text = Filters.ByAlphabetically},
    //        };

    //        PropertyChanged += OnFilterControlPropertyChanged;

    //        SelectedFlter = FilterTypes[0];
    //        SelectedOrder = OrderTypes[0];

    //        SelectedFlter.IsChecked = true;
    //        SelectedOrder.IsChecked = true;
    //    }
    //    public SortInDate SelectedFlter
    //    {
    //        get => GetProperty<SortInDate>();
    //        set => SetProperty(value);
    //    }
    //    public PutInOrderNote SelectedOrder
    //    {
    //        get => GetProperty<PutInOrderNote>();
    //        set => SetProperty(value);
    //    }

    //    public SortInDate[] FilterTypes
    //    {
    //        get => GetProperty<SortInDate[]>();
    //        set => SetProperty(value);
    //    }
    //    public PutInOrderNote[] OrderTypes
    //    {
    //        get => GetProperty<PutInOrderNote[]>();
    //        set => SetProperty(value);
    //    }

    //    public void SetBySelectedDate(DateTime dateTime)
    //    {
    //        if ((SelectedFlter is SelectedSortInDate) == false)
    //            SelectedFlter = FilterTypes.FirstOrDefault(t => t.This is SelectedSortInDate);

    //        var selectedSortInDate = SelectedFlter as SelectedSortInDate;
    //        selectedSortInDate.Date = dateTime;
    //        Notify(nameof(SelectedFlter));
    //    } 
    //    public IEnumerable<IPackNote> GetFiltered() => _filterPackNotes.GetFiltered();

    //    private void OnFilterControlPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    //    {
    //        switch (e.PropertyName)
    //        {
    //            case nameof(SelectedFlter):
    //                _filterPackNotes.SortInDate = (SortInDate)SelectedFlter.This;
    //                break;
    //            case nameof(SelectedOrder):
    //                _filterPackNotes.PutInOrder = (PutInOrderNote)SelectedOrder.This;
    //                break;
    //            default:
    //                break;
    //        }
    //    }
    //    private enum Types
    //    {
    //        Selected, Today, Tomorrow, All
    //    }
    //}
}
