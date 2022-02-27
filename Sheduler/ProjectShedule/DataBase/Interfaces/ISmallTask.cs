using ProjectShedule.DataNote.Interfaces;

namespace ProjectShedule.DataNote
{
    public interface ISmallTask : ITable
    {
        public int IdNote { get; }
        public string Name { get; }
        public bool Status { get; }
    }
}