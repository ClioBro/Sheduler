using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert.Entry
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
            HeaderText = headerText;
            EditorPlaceHolder = editorPlaceholder;
            CancelationButtonText = cancelText;
            AgreementButtonText = agreementText;
            PageSize = size;
            _answer = new ResultText();
            BindingContext = this;
        }
        public string HeaderText { get; set; }
        public string EditorText { get; set; }
        public string EditorPlaceHolder { get; set; }
        public string CancelationButtonText { get; set; }
        public string AgreementButtonText { get; set; }

        public Size PageSize { get; set; }

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
            _answer.Value = EditorText;
        }
    }
}