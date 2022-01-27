using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryView : Popup<EntryView.ResultText>
    {
        private readonly ResultText _answer;
        public class ResultText
        {
            public string Value;
            public bool IsCanceled = false;
        }
        public EntryView(
            string headerText = null, 
            string editorPlaceholder = null, 
            string cancelText = "Cancel", 
            string agreementText = "Ok", 
            Size size = new Size())
        {
            InitializeComponent();
            headerLabel.Text = headerText;
            editor.Placeholder = editorPlaceholder;
            cancelationButton.Text = cancelText;
            agreementButton.Text = agreementText;
            InicializatePopUpViewSize(size);
            _answer = new ResultText();
        }
        private void InicializatePopUpViewSize(Size size)
        {
            popUpView.Size = size;
            editor.Focus();
        }

        private void CancelationButton_Clicked(object sender, System.EventArgs e)
        {
            AssigningEditorTextToResult();
            _answer.IsCanceled = true;
            Dismiss(_answer);
        }
        private void AgreementButton_Clicked(object sender, System.EventArgs e)
        {
            AssigningEditorTextToResult();
            Dismiss(_answer);
        }

        protected override ResultText GetLightDismissResult()
        {
            AssigningEditorTextToResult();
            _answer.IsCanceled = true;
            return _answer;
        }
        private protected void AssigningEditorTextToResult()
        {
            _answer.Value = editor.Text;
        }
    }
}