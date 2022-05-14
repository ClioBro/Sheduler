using ProjectShedule.Escaping;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.NotifyOnApp.Enum;
using Xamarin.Forms;

namespace ProjectShedule.Shedule
{
    public class NotifyOnAppManager
    {
        public void SendNotify(IHasNote ihasNote)
        {
            INotificationManager notificationManager = DependencyService.Get<INotificationManager>();
            Notification notify = new Notification
            {
                Title = ihasNote.Note.Header,
                Message = ihasNote.Note.DopText,
                RepeadType = (RepeadType)ihasNote.Note.RepeadIdKey,
                AlertTime = ihasNote.Note.AppointmentDate
            };
            notificationManager.SendNotification(notify);
        }
    }
}
