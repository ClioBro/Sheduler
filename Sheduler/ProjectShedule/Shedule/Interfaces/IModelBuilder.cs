namespace ProjectShedule.Shedule.Interfaces
{
    public interface IModelBuilder<out TBuilder, TData>
    {
        TBuilder SetData(TData data);
    }
}
