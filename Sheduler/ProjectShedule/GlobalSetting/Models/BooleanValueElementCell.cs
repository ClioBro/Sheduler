namespace ProjectShedule.GlobalSetting.Models
{
    public class BooleanValueElementCell : ElementCell<bool>
    {
        private readonly string _tru;
        private readonly string _fals;
        public BooleanValueElementCell(string trueText = "true", string falseText = "false")
        {
            _tru = trueText;
            _fals = falseText;
            ValueChanged += (object sender, bool e) => ValueText = e ? _tru : _fals;
        }
        public string ValueText { get; set; }
    }
}
