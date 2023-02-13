namespace ProjectShedule.Core.Search
{
    internal abstract class SearchBarTitleViewModel : SerchBarViewModel
    {
        private string _title;

        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }
    }
}