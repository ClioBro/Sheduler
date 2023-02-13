using ProjectShedule.Core.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.ViewModels;
using ProjectShedule.Shedule.ViewModels.Interfaces;
using System;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.Service
{
    public class ServiceToOpenNoteEditorPage : INoteViewModelEditorOpening
    {
        public ServiceToOpenNoteEditorPage(INavigation navigation)
        {
            Navigation = navigation;
        }

        public INavigation Navigation { get; private set; }

        public void OpenEditorAsync()
        {
            if (Editor.EditorNotePage.IsPageOpened)
                return;

            var editorPageViewModel = new EditorNotePageViewModel();
            OpenEditorPage(editorPageViewModel);
        }
        public void OpenEditorAsync(DateTime dateTime)
        {
            if (Editor.EditorNotePage.IsPageOpened)
                return;

            var editorPageViewModel = new EditorNotePageViewModel();
            editorPageViewModel.EditNoteViewModel.AppointmentDate = dateTime;
            OpenEditorPage(editorPageViewModel);
        }
        public void OpenEditorAsync(IHasData<Note> item)
        {
            if (Editor.EditorNotePage.IsPageOpened)
                return;

            Note note = item.GetData().Clone() as Note;

            var editorPageViewModel = new EditorNotePageViewModel(note);
            OpenEditorPage(editorPageViewModel);
        }

        protected virtual async void OpenEditorPage(EditorNotePageViewModel editorPageViewModel)
        {
            Page page = new Editor.EditorNotePage(editorPageViewModel);
            await Navigation.PushModalAsync(page);
        }
        
    }
}