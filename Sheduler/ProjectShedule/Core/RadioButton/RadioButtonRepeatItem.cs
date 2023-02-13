using ProjectShedule.Core.Notify;

namespace ProjectShedule.Core.RadioButton
{
    public class RadioButtonRepeatItem : RadioButtonItemModel
    {
        public RadioButtonRepeatItem(NotifyRepeat notifyRepead)
        {
            NotifyRepeat = notifyRepead;
        }
        public NotifyRepeat NotifyRepeat { get; private set; }
        public override string Text { get => NotifyRepeat.Text; }
    }
}