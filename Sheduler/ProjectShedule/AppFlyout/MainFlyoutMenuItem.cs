using System;
using Xamarin.Forms;

namespace ProjectShedule.AppFlyout
{
    public class MainFlyoutMenuItem
    {
        public MainFlyoutMenuItem()
        {
            TargetType = typeof(MainFlyoutMenuItem);
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public ImageSource DisplayedImage { get; set; }
        public Type TargetType { get; set; }
    }
}