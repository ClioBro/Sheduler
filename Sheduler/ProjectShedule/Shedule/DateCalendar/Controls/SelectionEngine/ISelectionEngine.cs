namespace ProjectShedule.Shedule.DateCalendar.Controls.SelectionEngine
{
    internal interface ISelectionEngine<T>
    {
        void SelectItem(T newItem);
        bool ItemIsSelectet(T item);
    }
}
