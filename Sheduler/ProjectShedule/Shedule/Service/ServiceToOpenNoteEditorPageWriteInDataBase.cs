using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Linq;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Service
{
    public class ServiceToOpenNoteEditorPageWriteInDataBase : ServiceToOpenNoteEditorPage
    {
        private readonly IShedulePageDataWrite _shedulePageDataWriteModel;
        public event EventHandler Saved;

        public ServiceToOpenNoteEditorPageWriteInDataBase(INavigation navigation, IShedulePageDataWrite shedulePageDataWrite)
            :base(navigation)
        {
            _shedulePageDataWriteModel = shedulePageDataWrite;
        }
        
        protected override void OpenEditorPage(EditorNotePageViewModel editorPageViewModel)
        {
            editorPageViewModel.SavePressed += OnSavePressedEventHandler;
            base.OpenEditorPage(editorPageViewModel);
        }

        protected virtual void OnSavePressedEventHandler(EditorNotePageViewModel sender, NoteSavedEventArgs eArgs)
        {
            if (NoteIsNotNew(sender) && HasOldRemovedSmllTasks(eArgs))
                RemoveOldSmallTasksInDataBase(eArgs);

            SaveInDataBase(eArgs.NoteContainer);
            NotifySaved();
        }

        private bool NoteIsNotNew(EditorNotePageViewModel sender) => sender.IsNewNote == false;
        private bool HasOldRemovedSmllTasks(NoteSavedEventArgs eventArgs) => eventArgs.OldRemovedSmallTaskContainers?.Count() > 0;
        private void RemoveOldSmallTasksInDataBase(NoteSavedEventArgs eventArgs)
        {
            foreach (var smallTask in eventArgs.OldRemovedSmallTaskContainers)
                _shedulePageDataWriteModel.Delete(smallTask);
        }
        private void SaveInDataBase(IHasData<Note> hasData)
        {
            _shedulePageDataWriteModel.Save(hasData);
        }
        private void NotifySaved()
        {
            Saved?.Invoke(this, EventArgs.Empty);
        }
    }
}