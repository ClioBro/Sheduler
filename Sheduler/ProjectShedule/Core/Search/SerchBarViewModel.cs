using System.Windows.Input;

namespace ProjectShedule.Core.Search
{
    internal abstract class SerchBarViewModel : BaseViewModel, ISearchBarAnimationControll, ISerchBar
    {
        private double _widthRequest;
        private string _placeholder;
        private string _text;

        public abstract ISearchBarAnimation SearchBarAnimation { get; }
        public abstract ICommand SerchCommand { get; set; }
        public virtual double WidthRequest
        {
            get => _widthRequest;
            set
            {
                if (_widthRequest == value)
                    return;
                _widthRequest = value;
                OnPropertyChanged();
            }
        }
        public virtual string Placeholder
        {
            get => _placeholder;
            set
            {
                _placeholder = value;
                OnPropertyChanged();
            }
        }
        public virtual string Text
        {
            get => _text;
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }
    }
}