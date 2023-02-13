using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.DataBase.Interfaces;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Interfaces
{
    public interface IDemonstrationSmallTaskViewModel : IHasData<SmallTask>, ISmallTask, ICloneable, INotifyPropertyChanged
    {
        delegate void SmallTaskViewModelEventHandler<Tvalue>(IDemonstrationSmallTaskViewModel smallTaskViewModel, Tvalue value);
        event SmallTaskViewModelEventHandler<bool> StatusChanged;
        INavigation Navigation { get; set; }
    }
}
