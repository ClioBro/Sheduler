namespace ProjectShedule.Core.Sorting
{
    public interface IHasOrderByState
    {
        public OrderState OrderByState { get; set; }
    }
}