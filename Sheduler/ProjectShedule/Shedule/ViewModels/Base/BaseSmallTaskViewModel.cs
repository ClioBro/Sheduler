using ProjectShedule.Core.Interfaces;
using ProjectShedule.Core.Swipe;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class BaseSmallTaskViewModel : IDemonstrationSmallTaskViewModel, IHasHeader
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        public event IDemonstrationSmallTaskViewModel.SmallTaskViewModelEventHandler<bool> StatusChanged;

        private readonly SmallTaskModel _baseSmallTaskModel;

        private bool _viewIsEnable = true;
        private double _opacity = 1;
        public BaseSmallTaskViewModel(SmallTaskModel baseSmallTaskModel)
        {
            _baseSmallTaskModel = baseSmallTaskModel;
            InicializationSwipe();
        }

        #region Properties
        public int Id => _baseSmallTaskModel.Id;
        public string Header
        {
            get => _baseSmallTaskModel.Header;
            set
            {
                if (_baseSmallTaskModel.Header != value)
                {
                    _baseSmallTaskModel.Header = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Status
        {
            get => _baseSmallTaskModel.Status;
            set
            {
                if (_baseSmallTaskModel.Status != value)
                {
                    _baseSmallTaskModel.Status = value;
                    StatusChanged?.Invoke(this, value);
                    OnPropertyChanged();
                }
            }
        }
        public bool IsDeleted => _baseSmallTaskModel.IsDeleted;

        public bool ViewIsEnable
        {
            get => _viewIsEnable;
            set
            {
                if (_viewIsEnable != value)
                {
                    _viewIsEnable = value;
                    OnPropertyChanged();
                }
            }
        }
        public double Opacity
        {
            get => _opacity;
            set
            {
                if (_opacity != value)
                {
                    _opacity = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand CheckChangedCommand { get; set; }

        public SwipeHorizontalContentPercentAchivments SwipeView { get; set; } = new SwipeHorizontalContentPercentAchivments(150);
        public INavigation Navigation { get; set; }

        protected SmallTaskModel SmallTaskModel => _baseSmallTaskModel;

        #endregion

        protected virtual void InicializationSwipe()
        {
            CompressedLayout.SetIsHeadless(SwipeView.RightItems, true);
            CompressedLayout.SetIsHeadless(SwipeView.LeftItems, true);
        }

        public abstract object Clone();

        SmallTask IHasData<SmallTask>.GetData()
        {
            IHasData<SmallTask> hasData = _baseSmallTaskModel;
            return hasData.GetData();
        }
    }
}
