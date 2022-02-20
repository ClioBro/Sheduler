namespace ProjectShedule.GlobalSetting.Models
{
    public class BooleanValueElementCell : ElementCell<bool>
    {
        private readonly string _tru;
        private readonly string _fals;
        public BooleanValueElementCell(string tru = "true", string fals = "false")
        {
            _tru = tru;
            _fals = fals;
            ValueChanged += (object sender, bool e) => ValueText = e ? _tru : _fals; ;
        }
        public string ValueText { get; set; }
    }
}
