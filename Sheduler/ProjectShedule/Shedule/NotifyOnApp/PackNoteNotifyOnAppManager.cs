using ProjectShedule.Core.Enum;
using ProjectShedule.Core.Notify;
using ProjectShedule.DataBase.Interfaces;
using ProjectShedule.Escaping;
using Xamarin.Forms;

namespace ProjectShedule.Shedule
{
    public class NoteNotifyOnAppManager : INotifyManager<INote>
    {
        private readonly INotificationManager _notificationManager;
        
        public NoteNotifyOnAppManager()
        {
            _notificationManager = DependencyService.Get<INotificationManager>();
        }
        
        public void SendNotify(INote note)
        {
            INotification notification = CreateNotify(note);
            _notificationManager.Send(notification);
        }
        public void RemoveNotify(INote note)
        {
            INotification notification = CreateNotify(note);
            _notificationManager.Cancel(notification);
        }

        private INotification CreateNotify(INote note)
        {
            return new Notification
            {
                ID = note.Id,
                Title = note.Header,
                Message = note.DopText,
                RepeatType = (RepeatType)note.RepeatIdKey,
                AlertTime = note.AppointmentDate
            };
        }
    }
}
