using ProjectShedule.DataNote;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Models
{
    //В интерфейс IPackNote добавить INote и IHasSmallTasks
    public class PackNoteModel : IPackNote
    {
        public delegate void SmallTaskDelegate(SmallTaskViewModel smallTaskViewModel);
        public event SmallTaskDelegate SmallTaskDeleted;
        public event SmallTaskDelegate SmallTaskAdded;

        private readonly ObservableCollection<SmallTaskViewModel> _smallTasks;
        public PackNoteModel() : this(new Note() {AppointmentDate = DateTime.Today }, new ObservableCollection<SmallTaskViewModel>()) { }
        public PackNoteModel(Note note, IEnumerable<SmallTaskViewModel> smallTaks)
        {
            Note = note;
            _smallTasks = new ObservableCollection<SmallTaskViewModel>(smallTaks);

            BackGroundColor = string.IsNullOrEmpty(note.BackgroundColorKey)
                ? DefaulBackgroundColor : Color.FromHex(note.BackgroundColorKey);
            LineColor = string.IsNullOrEmpty(note.LineColorKey)
                ? DefaulLineColor : Color.FromHex(note.LineColorKey);
        }

        #region Properties
        public Note Note { get; }
        public ReadOnlyObservableCollection<SmallTaskViewModel> SmallTasks => new ReadOnlyObservableCollection<SmallTaskViewModel>(_smallTasks);
        public Color BackGroundColor
        {
            get 
            {
                var backColor = Color.FromHex(Note.BackgroundColorKey);
                return backColor == Color.Default ? DefaulBackgroundColor : backColor;
            }
            set => Note.BackgroundColorKey = value.ToHex();
        }
        public Color LineColor
        {
            get
            {
                var lineColor = Color.FromHex(Note.LineColorKey);
                return lineColor == Color.Default ? DefaulLineColor : lineColor;
            }
            set => Note.LineColorKey = value.ToHex();
        }
        public Color DefaulBackgroundColor { get; protected internal set; } = Color.FromHex("#E6F2FF");
        public Color DefaulLineColor { get; protected internal set; } = Color.FromHex("#CCCCFF");
        #endregion

        public void DeleteSmallTask(SmallTaskViewModel smallTask)
        {
            if (_smallTasks.Remove(smallTask) == true)
                SmallTaskDeleted?.Invoke(smallTask);
        }
        public void AddSmallTask(SmallTaskViewModel smallTask)
        {
            _smallTasks.Add(smallTask);
            SmallTaskAdded?.Invoke(smallTask);
        }
    }
}
