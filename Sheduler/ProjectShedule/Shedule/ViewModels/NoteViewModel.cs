using ProjectShedule.DataBase.BusinessLayer.Entities;
using ProjectShedule.Shedule.Builder;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.Swipe;
using ProjectShedule.Shedule.ViewModels.Base;
using Xamarin.Forms;

namespace ProjectShedule.Shedule.ViewModels
{
    public class NoteViewModel : NoteDeletableSwipebleExpanderCanDeleteSmallTaskViewModel<DeletableSmallTaskViewModel>
    {
        private EditNiteSwipeItemView _editSwipeItem;

        public NoteViewModel(Note note) : base(note) { }
        public override ISmallTaskViewModelBuilder<DeletableSmallTaskViewModel> SmallTaskViewModelBuilder { get; protected set; } = new DeletableSmallTaskViewModelBuilder();

        public ISwipeItem EditSwipeItem => _editSwipeItem;

        public override object Clone() => new NoteViewModel(Note.Clone() as Note);

        protected override void InicializationSwipe()
        {
            base.InicializationSwipe();
            _editSwipeItem = new EditNiteSwipeItemView(this);
            RightItems.Add(_editSwipeItem);
        }
    }
}
