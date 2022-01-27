namespace ProjectShedule.DataNote
{
    public interface ISmallTask
    {
        public int IdNote { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
    }
}