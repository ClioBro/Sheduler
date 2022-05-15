using System;
using Xamarin.Forms;

namespace ProjectShedule.AppFlyout.Models
{
    public class MainFlyoutMenuItem
    {
        private static int _createdCount;
        public MainFlyoutMenuItem()
        {
            TargetType = typeof(MainFlyoutMenuItem);
            Id = _createdCount++;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public ImageSource DisplayedImage { get; set; }
        public Type TargetType { get; set; }
    }
}