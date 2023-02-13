using Xamarin.Forms;

namespace ProjectShedule.Core
{
    public class FlexLayoutViewModel : BindableBase<FlexLayoutViewModel>
    {
        public FlexWrap Wrap { get => GetProperty<FlexWrap>(); set => SetProperty(value); }
        public FlexDirection Direction { get => GetProperty<FlexDirection>(); set => SetProperty(value); }
        public FlexAlignContent AlignContent { get => GetProperty<FlexAlignContent>(); set => SetProperty(value); }
        public FlexAlignItems AlignItems { get => GetProperty<FlexAlignItems>(); set => SetProperty(value); }
        public FlexJustify JustifyContent { get => GetProperty<FlexJustify>(); set => SetProperty(value); }
    }
}