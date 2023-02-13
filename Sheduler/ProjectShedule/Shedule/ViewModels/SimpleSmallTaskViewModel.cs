
using ProjectShedule.Shedule.Models;
using ProjectShedule.Shedule.ViewModels.Base;

namespace ProjectShedule.Shedule.ViewModels
{
    public class SimpleSmallTaskViewModel : BaseSmallTaskViewModel
    {
        public SimpleSmallTaskViewModel(SmallTaskModel smallTaskModel) : base(smallTaskModel)
        {

        }
        public override object Clone()
        {
            return new SimpleSmallTaskViewModel(SmallTaskModel.Clone() as SmallTaskModel);
        }
    }
}

