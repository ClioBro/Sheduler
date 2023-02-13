namespace ProjectShedule.GlobalSetting.Base
{
    public interface IElementCell<T>
    {
        public string MainText { get; }
        public T Value { get; set; }
    }
}
