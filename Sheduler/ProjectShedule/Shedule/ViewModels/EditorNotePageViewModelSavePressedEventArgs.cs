using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using System;
using System.Collections.Generic;

namespace ProjectShedule.Shedule.ViewModels
{
    public class NoteSavedEventArgs : EventArgs
    {
        private readonly IEnumerable<IHasData<SmallTask>> _oldRemovedsmallTasks;
        private readonly IHasData<Note> _note;

        public NoteSavedEventArgs(IHasData<Note> note, IEnumerable<IHasData<SmallTask>> oldRemovedSmallTasks)
        {
            _oldRemovedsmallTasks = oldRemovedSmallTasks;
            _note = note;
        }

        public IEnumerable<IHasData<SmallTask>> OldRemovedSmallTaskContainers => _oldRemovedsmallTasks;
        public IHasData<Note> NoteContainer => _note;
    }
}
