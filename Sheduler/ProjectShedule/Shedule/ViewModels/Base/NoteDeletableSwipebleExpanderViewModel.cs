using ProjectShedule.Core.Swipe.Interfaces;
using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Swipe;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels.Base
{
    public abstract class NoteDeletableSwipebleExpanderViewModel<TSmallTaskViewModel> : NoteDemonstrationSwipebleExpanderViewModel<TSmallTaskViewModel>
        where TSmallTaskViewModel : SimpleSmallTaskViewModel
    {
        private DeleteNoteSwipeItemView _deleteSwipeItem;
        public NoteDeletableSwipebleExpanderViewModel(Note note) : base(note) { }

        public ISwipeItem DeleteSwipeItem => _deleteSwipeItem;

        protected override void InicializationSwipe()
        {
            //_deleteSwipeItem = new DeleteSwipeItem(commandParam: this);
            _deleteSwipeItem = new DeleteNoteSwipeItemView(commandParameter: this);
            RightItems.Add(_deleteSwipeItem);
        }
    }
}
