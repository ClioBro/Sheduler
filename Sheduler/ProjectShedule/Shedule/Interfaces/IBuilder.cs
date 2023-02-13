namespace ProjectShedule.Shedule.Interfaces
{
    public interface IBuilder<out T>
    {
        T Build();
    }
}
