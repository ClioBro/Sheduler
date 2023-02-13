using System;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert.Exception
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExceptionView : Popup
    {
        private string _message;
        private string _stackTrace;
        
        public ExceptionView(System.Exception exception) 
        {
            _stackTrace = exception.StackTrace;
            _message = exception.Message;
            BindingContext = this;
            InitializeComponent();
        }

        public string Message => _message;
        public string StackTrace => _stackTrace;
    }
}