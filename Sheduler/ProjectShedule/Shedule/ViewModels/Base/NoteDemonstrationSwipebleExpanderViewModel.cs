using ProjectShedule.Core.Expander;
using ProjectShedule.Core.Swipe.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class NoteDemonstrationSwipeViewModel<TSmallTaskViewModel> : NoteDemonstrationViewModel<TSmallTaskViewModel>, ISwipeHorizontalContent
        where TSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        #nullable enable annotations
        public NoteDemonstrationSwipeViewModel(Note note) : base(note)
        {
            InicializationSwipe();
        }

        public double Threshold { get; protected set; } = 100;
        public SwipeItems RightItems { get; protected set; } = new SwipeItems();
        public SwipeItems LeftItems { get; protected set; } = new SwipeItems();

        public ISwipeViewController? SwipeViewController { get; set; }

        protected abstract void InicializationSwipe();

        void ISwipeItemsControll.DisableSwipeItem(Type type)
        {
            SetVisible(GetByType(type), false);
        }
        void ISwipeItemsControll.EnableSwipeItem(Type type)
        {
            SetVisible(GetByType(type), true);
        }
        void ISwipeItemsControll.DisableSwipeItems()
        {
            SetVisible(RightItems, false);
            SetVisible(LeftItems, false);
        }
        void ISwipeItemsControll.EnableSwipeItems()
        {
            SetVisible(RightItems, true);
            SetVisible(LeftItems, true);
        }

        private void SetVisible(IEnumerable<ISwipeItem> swipeItems, bool isVisible)
        {
            foreach (ISwipeItem swipeItem in swipeItems)
                SetVisible(swipeItem, isVisible);
        }
        private void SetVisible(ISwipeItem swipeItem, bool isVisible)
        {
            swipeItem.IsVisible = isVisible;
        }
        private ISwipeItem GetByType(Type type)
        {
            List<ISwipeItem> tempList = new List<ISwipeItem>();
            tempList.AddRange(RightItems);
            tempList.AddRange(LeftItems);
            ISwipeItem result = tempList.FirstOrDefault(t => t.GetType() == type);
            if (result is null)
                throw new NullReferenceException($"{nameof(result)}");

            return result;
        }
    }

    public abstract class NoteDemonstrationSwipebleExpanderViewModel<TSmallTaskViewModel> : NoteDemonstrationSwipeViewModel<TSmallTaskViewModel>
        where TSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        private bool isExpanded;

        public NoteDemonstrationSwipebleExpanderViewModel(Note note) : base(note)
        {
            ExpanderInfo = new ExpanderControll(() => !SwipeViewController?.IsOpen ?? true);
        }

        public bool DopTextIsVisible => !string.IsNullOrEmpty(DopText);
        public double Opacity { get; set; } = 1;

        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                if (value == isExpanded)
                    return;
                isExpanded = value;
                OnPropertyChanged();
            }
        }
        public IExpanderInfo ExpanderInfo { get; private set; }

        public ICommand ForceUpdateSizeCommand { get; set; }

        protected override void SmallTaskViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            base.SmallTaskViewModels_CollectionChanged(sender, e);
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Reset:
                case NotifyCollectionChangedAction.Remove:
                case NotifyCollectionChangedAction.Replace:
                    ForceUpdateSizeCommand?.Execute(this);
                    break;
            }
        }
    }
}