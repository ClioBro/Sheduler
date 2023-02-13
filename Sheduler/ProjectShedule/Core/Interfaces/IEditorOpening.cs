namespace ProjectShedule.Core.Interfaces
{
    public interface IEditorOpening<Titem>
    {
        void OpenEditorAsync(Titem item);
    }

}