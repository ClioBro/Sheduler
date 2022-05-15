using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Models
{
    public interface IReadOnlyPackNote
    {
        INote Note { get; }
        IEnumerable<ISmallTask> SmallTasks { get; }
    }
    public class ReadOnlyPackNote : IReadOnlyPackNote
    {
        private readonly INote _note;
        private readonly IEnumerable<ISmallTask> _smallTasks;
        public ReadOnlyPackNote(IPackNote packNote)
            : this(packNote.Note, packNote.SmallTasks) { }
        public ReadOnlyPackNote(INote note, IEnumerable<ISmallTask> smallTasks)
        {
            _note = note;
            _smallTasks = smallTasks;
        }
        public INote Note => _note;
        public IEnumerable<ISmallTask> SmallTasks => _smallTasks;
    }
    public abstract class BasePackNoteModel : IPackNote, IReadOnlyPackNote
    {
        public delegate void SmallTaskDelegate(BaseSmallTaskViewModel smallTaskViewModel);
        public event SmallTaskDelegate SmallTaskDeleted;
        public event SmallTaskDelegate SmallTaskAdded;

        protected BaseNote _note;
        protected ObservableCollection<BaseSmallTaskViewModel> _smallTasks;

        #region Properties
        public BaseNote Note => _note;
        public ReadOnlyObservableCollection<BaseSmallTaskViewModel> SmallTasks => new ReadOnlyObservableCollection<BaseSmallTaskViewModel>(_smallTasks);
        INote IReadOnlyPackNote.Note => _note;
        IEnumerable<ISmallTask> IReadOnlyPackNote.SmallTasks => _smallTasks;
        public Color BackGroundColor
        {
            get
            {
                var backColor = Color.FromHex(_note.BackgroundColorKey);
                return backColor == Color.Default ? DefaulBackgroundColor : backColor;
            }
            set => _note.BackgroundColorKey = value.ToHex();
        }
        public Color LineColor
        {
            get
            {
                var lineColor = Color.FromHex(_note.LineColorKey);
                return lineColor == Color.Default ? DefaulLineColor : lineColor;
            }
            set => _note.LineColorKey = value.ToHex();
        }
        public Color DefaulBackgroundColor { get; protected internal set; } = Color.FromHex("#E6F2FF");
        public Color DefaulLineColor { get; protected internal set; } = Color.FromHex("#CCCCFF");
        #endregion

        public void DeleteSmallTask(BaseSmallTaskViewModel smallTask)
        {
            if (_smallTasks.Remove(smallTask) == true)
                SmallTaskDeleted?.Invoke(smallTask);
        }
        public void AddSmallTask(BaseSmallTaskViewModel smallTask)
        {
            _smallTasks.Add(smallTask);
            SmallTaskAdded?.Invoke(smallTask);
        }

        public abstract object Clone();
    }

    public class PackNoteModel : BasePackNoteModel
    {
        public PackNoteModel(BaseNote note, IEnumerable<BaseSmallTaskViewModel> smallTaks)
        {
            _note = note;
            _smallTasks = new ObservableCollection<BaseSmallTaskViewModel>(smallTaks);

            BackGroundColor = string.IsNullOrEmpty(note.BackgroundColorKey)
                ? DefaulBackgroundColor : Color.FromHex(note.BackgroundColorKey);
            LineColor = string.IsNullOrEmpty(note.LineColorKey)
                ? DefaulLineColor : Color.FromHex(note.LineColorKey);
        }

        public override object Clone()
        {
            IEnumerable<BaseSmallTaskViewModel> newSmallTasksModels = _smallTasks.Select(GetCloneSmallTaskViewModel);
            return new PackNoteModel(_note.Clone() as BaseNote, newSmallTasksModels);

            static BaseSmallTaskViewModel GetCloneSmallTaskViewModel(BaseSmallTaskViewModel smallTaskViewModel)
            {
                return smallTaskViewModel.Clone() as BaseSmallTaskViewModel;
            }
        }
    }

    public interface IBuilderPackNote : IBuilder<BasePackNoteModel>
    {
        IBuilderPackNote SetNote(BaseNote baseNote);
        IBuilderPackNote SetSmallTasks(IEnumerable<BaseSmallTaskViewModel> baseSmallTasks);
        IBuilderPackNote SetSmallTasks(IEnumerable<BaseSmallTask> baseSmallTasks);
    }
    public class BuilderPackNoteModel : IBuilderPackNote
    {
        private BaseNote _baseNote;
        private IEnumerable<BaseSmallTaskViewModel> _baseSmallTasks;
        private readonly IBuilderSmallTaskViewModel _builderSmallTaskViewModel;
        public BuilderPackNoteModel(IBuilderSmallTaskViewModel builderSmallTaskViewModel)
        {
            _builderSmallTaskViewModel = builderSmallTaskViewModel;
        }
        public BasePackNoteModel Build()
        {
            return new PackNoteModel(_baseNote ?? new Note(), _baseSmallTasks ?? new List<SmallTaskViewModel>());
        }
        public IBuilderPackNote SetNote(BaseNote baseNote)
        {
            _baseNote = baseNote;
            return this;
        }
        public IBuilderPackNote SetSmallTasks(IEnumerable<BaseSmallTaskViewModel> baseSmallTasks)
        {
            _baseSmallTasks = baseSmallTasks;
            return this;
        }
        public IBuilderPackNote SetSmallTasks(IEnumerable<BaseSmallTask> baseSmallTasks)
        {
            _baseSmallTasks = baseSmallTasks.Select(BuildSmallTaskViewModel);
            return this;

            BaseSmallTaskViewModel BuildSmallTaskViewModel(BaseSmallTask baseSmallTask)
            {
                return _builderSmallTaskViewModel.Build(baseSmallTask);
            }
        }
    }
}
