using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class BaseExtandedNoteViewModel<TSmallTaskViewModel> : BaseNoteViewModel, INoteExtandedViewModel<TSmallTaskViewModel>
        where TSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        public delegate void SmallTaskEventHandler<TSender, TValue>(TSender sender, TValue value);
        public event EventHandler<SmallTasksCollectionChangedEventArgs<TSmallTaskViewModel>> SmallTasksCollectionChanged;

        public BaseExtandedNoteViewModel(Note note) : base(note)
        {
            InicializationCommands();
            GenerateSmallTaskViewModels();
            SmallTaskViewModels.CollectionChanged += SmallTaskViewModels_CollectionChanged;
        }

        #region SmallTasksProperties
        public bool SmallTasksContainerIsEnable { get; set; } = true;
        public abstract ISmallTaskViewModelBuilder<TSmallTaskViewModel> SmallTaskViewModelBuilder { get; protected set; }

        public ReadOnlyObservableCollection<TSmallTaskViewModel> ReadOnlySmallTaskViewModels { get; private set; }
        
        protected ObservableRangeCollection<TSmallTaskViewModel> SmallTaskViewModels { get; private set; }
        #endregion SmallTasksProperties

        protected abstract void InicializationCommands();
        protected virtual void SmallTaskViewModels_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            IEnumerable<TSmallTaskViewModel> oldItems = e.OldItems?.OfType<TSmallTaskViewModel>();
            IEnumerable<TSmallTaskViewModel> newItems = e.NewItems?.OfType<TSmallTaskViewModel>();
            var smallTasksCollectionChangedEventArgs = new SmallTasksCollectionChangedEventArgs<TSmallTaskViewModel>(oldItems, newItems, e.Action);
            SmallTasksCollectionChanged?.Invoke(this, smallTasksCollectionChangedEventArgs);
        }
        protected virtual TSmallTaskViewModel BuildViewModel(SmallTask smallTask)
        {
            TSmallTaskViewModel smallTaskViewModel = SmallTaskViewModelBuilder
                .SetData(smallTask)
                .Build();
            return smallTaskViewModel;
        }
        protected SmallTask GetSmallTask(TSmallTaskViewModel smallTaskViewModel)
        {
            IHasData<SmallTask> hasData = smallTaskViewModel;
            return hasData.GetData();
        }

        private void GenerateSmallTaskViewModels()
        {
            SmallTaskViewModels = new ObservableRangeCollection<TSmallTaskViewModel>();
            ReadOnlySmallTaskViewModels = new ReadOnlyObservableCollection<TSmallTaskViewModel>(SmallTaskViewModels);
            
            List<TSmallTaskViewModel> tempviewModels = new List<TSmallTaskViewModel>();
            foreach (SmallTask smallTask in Note.SmallTasks)
                tempviewModels.Add(BuildViewModel(smallTask));

            SmallTaskViewModels.AddRange(tempviewModels);
        }
    }
}