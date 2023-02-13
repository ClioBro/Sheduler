using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.Models
{
    public class ColorsModel
    {
        public ColorsModel(IEnumerable<Color> colors)
        {
            Colors = colors;
        }
        public IEnumerable<Color> Colors { get; private set; }
    }
}
