namespace ProjectShedule.GlobalSetting.Models
{
    public class BooleanValueElementCellModel : BaseElementCellModel<bool>
    {
        private readonly string _trueTxt;
        private readonly string _falseTxt;
        
        public BooleanValueElementCellModel(string trueText = "true", string falseText = "false")
        {
            _trueTxt = trueText;
            _falseTxt = falseText;
        }

        public string ValueText => Value ? _trueTxt : _falseTxt;
    }
}
