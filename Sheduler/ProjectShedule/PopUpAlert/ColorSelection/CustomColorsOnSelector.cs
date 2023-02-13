using System.Collections.Generic;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    internal static class CustomColorsOnSelector
    {
        public static IEnumerable<Color> Colors { get; set; } = new List<Color>()
        {
            Color.FromHex("#DE3163"),Color.FromHex("#FF7F50"),Color.FromHex("#FFBF00"),
            Color.FromHex("#DFFF00"),Color.FromHex("#ECF0F1"),Color.FromHex("#D7DBDD"),
            Color.FromHex("#CCCCFF"),Color.FromHex("#5C6E8D"),Color.FromHex("#6495ED"),
            Color.FromHex("#40E0D0"),Color.FromHex("#9FE2BF"),Color.FromHex("#85929E"),
            Color.FromHex("#715C8D"),Color.FromHex("#B08CAF"),
        };
    }
}
