using ProjectShedule.Core;
using ProjectShedule.Core.RadioButton;
using ProjectShedule.Core.Sorting;
using System.Collections.Generic;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.ViewModels
{
    public abstract class ExpandebleRadioButtonsViewModel : BaseViewModel, IFilterHead, IOrderBy
    {
        private bool _isExpanded;

        protected ObservableRangeCollection<RadioButtonsViewModel> RadioButtonsViewModels { get; set; } = new ObservableRangeCollection<RadioButtonsViewModel>();
        public IReadOnlyCollection<RadioButtonsViewModel> ReadOnlyRadioButtonsViewModels => RadioButtonsViewModels;

        public virtual bool IsExpanded 
        {
            get => _isExpanded;
            set
            {
                if (value == _isExpanded)
                    return;
                _isExpanded = value;
                OnPropertyChanged();
            } 
        }
        public abstract string Text { get; }
        public abstract bool Descending { get; set; }
    }
}