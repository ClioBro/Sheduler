using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Interfaces;

namespace ProjectShedule.Shedule.Builder.Base
{
    public abstract class BaseNoteViewModelBuilder<TNoteViewModel> : INoteViewModelBuilder<TNoteViewModel>
    {
        private Note _note;
        public virtual TNoteViewModel Build()
        {
            TNoteViewModel noteViewModel = BuildViewModel();
            ClearFolders();
            return noteViewModel;
        }
        protected Note Note => _note;
        public INoteViewModelBuilder<TNoteViewModel> SetData(Note note)
        {
            _note = note;
            return this;
        }
        protected abstract TNoteViewModel BuildViewModel();
        protected virtual void ClearFolders()
        {
            _note = null;
        }
    }
}
