using ProjectShedule.DataBase.Entities;
using ProjectShedule.DataBase.Entities.Base;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Shedule.DataBase.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ProjectShedule.Shedule.ViewModels
{
    public abstract class BaseSmallTaskViewModel : INotifyPropertyChanged, ISmallTask, IHasModel<BaseSmallTask>
    {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        protected readonly BaseSmallTask _smallTask;
        public BaseSmallTaskViewModel(BaseSmallTask smallTask)
        {
            _smallTask = smallTask;
        }
        public int Id
        {
            get => _smallTask.Id;
            protected set
            {
                if (_smallTask.Id != value)
                {
                    _smallTask.Id = value;
                    OnPropertyChanged();
                }
            }
        }
        public int IdNote
        {
            get => _smallTask.IdNote;
            protected set
            {
                if (_smallTask.IdNote != value)
                {
                    _smallTask.IdNote = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Text
        {
            get => _smallTask.Text;
            set
            {
                if (_smallTask.Text != value)
                {
                    _smallTask.Text = value;
                    OnPropertyChanged();
                }
            }
        }
        public bool Status
        {
            get => _smallTask.Status;
            set
            {
                if (_smallTask.Status != value)
                {
                    _smallTask.Status = value;
                    OnPropertyChanged();
                }
            }
        }

        BaseSmallTask IHasModel<BaseSmallTask>.Model => _smallTask;
        public ICommand DeleteMeCommand { get; set; }
        public ICommand CheckChangedCommand { get; set; }

        public abstract object Clone();
    }
    public class SmallTaskViewModel : BaseSmallTaskViewModel
    {
        public SmallTaskViewModel(BaseSmallTask smallTask) : base(smallTask) 
        {

        }
        public override object Clone()
        {
            return new SmallTaskViewModel(_smallTask.Clone() as BaseSmallTask);
        }
    }

    public interface IBuilderSmallTaskViewModel : IBuilder<BaseSmallTaskViewModel>
    {
        BaseSmallTaskViewModel Build(BaseSmallTask smallTask);
        IBuilderSmallTaskViewModel SetText(string text);
        IBuilderSmallTaskViewModel SetDeleteCommand(ICommand deleteCommand);
        IBuilderSmallTaskViewModel SetChkChangedCommand(ICommand chkChangedCommand);
    }
    public class BuilderSmallTaskViewModel : IBuilderSmallTaskViewModel
    {
        private string _text;
        private ICommand _deleteCommand;
        private ICommand _chkChangedCommand;
        public BaseSmallTaskViewModel Build() => Build(new SmallTask());
        public BaseSmallTaskViewModel Build(BaseSmallTask smallTask)
        {
            return new SmallTaskViewModel(smallTask)
            {
                Text = _text ?? smallTask.Text,
                DeleteMeCommand = _deleteCommand,
                CheckChangedCommand = _chkChangedCommand
            };
        }
        public IBuilderSmallTaskViewModel SetText(string text)
        {
            _text = text;
            return this;
        }
        public IBuilderSmallTaskViewModel SetDeleteCommand(ICommand deleteCommand)
        {
            _deleteCommand = deleteCommand;
            return this;
        }
        public IBuilderSmallTaskViewModel SetChkChangedCommand(ICommand chkChangedCommand)
        {
            _chkChangedCommand = chkChangedCommand;
            return this;
        }
    }
}
