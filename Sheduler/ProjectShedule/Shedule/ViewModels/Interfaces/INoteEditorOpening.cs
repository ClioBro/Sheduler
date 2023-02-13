
using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using System;

namespace ProjectShedule.Shedule.ViewModels.Interfaces
{
    public interface INoteViewModelEditorOpening : IEditorOpening<IHasData<Note>>
    {
        void OpenEditorAsync(DateTime dateTime);
        void OpenEditorAsync();
    }
}