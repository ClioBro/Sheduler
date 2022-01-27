using ProjectShedule.DataNote;
using ProjectShedule.Shedule.ViewModels;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace ProjectShedule.NotePages
{
    public partial class NoteReadPage : Popup
    {
        private protected CancellationTokenSource _readingCancel;
        private protected bool _readingLock = false;

        public NoteReadPage()
        {
            InitializeComponent();
        }

        private void ReadNoteTexts(object sender, EventArgs e)
        {
            Button button = sender as Button;
           
            if (!_readingLock && BindingContext is PackNoteViewModel packNoteViewModel)
            {
                _readingLock = true;
                _readingCancel = new CancellationTokenSource();
                button.Text = "CancelSpeeking";
                button.TextColor = Color.Red;
                StringBuilder readText = new StringBuilder();

                readText.Append(packNoteViewModel.AppointmentDate.ToString());
                readText.Append(packNoteViewModel.AppointmentDate.ToShortTimeString());
                readText.Append(packNoteViewModel.Header);
                readText.Append(packNoteViewModel.DopText);

                TextToSpeech.SpeakAsync(readText.ToString(),
                    cancelToken: _readingCancel.Token).ContinueWith((t) =>
                    {
                        _readingLock = false;
                        button.Text = "Read to me";
                        button.TextColor = Color.Green;
                    }, TaskScheduler.FromCurrentSynchronizationContext());
            }
            else
            {
                CancelSpeech();
                _readingLock = false;
                button.Text = "Read to me";
            }
        }
        private protected void CancelSpeech()
        {
            if (_readingCancel?.IsCancellationRequested ?? true) {
                return;
            }
            _readingCancel.Cancel();
        }
    }
}