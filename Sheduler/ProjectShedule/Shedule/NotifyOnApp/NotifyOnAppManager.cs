using ProjectShedule.Escaping;
using ProjectShedule.Shedule.Interfaces;
using ProjectShedule.Shedule.NotifyOnApp.Enum;
using Xamarin.Forms;

namespace ProjectShedule.Shedule
{
    public class NotifyOnAppManager
    {
        public void SendNotify(IHasNote note)
        {
            INotificationManager notificationManager = DependencyService.Get<INotificationManager>();
            Notification notify = new Notification
            {
                Title = note.Note.Header,
                Message = note.Note.DopText,
                RepeadType = (RepeadType)note.Note.RepeadIdKey,
                AlertTime = note.Note.AppointmentDate
            };
            notificationManager.SendNotification(notify);
        }
    }
}
