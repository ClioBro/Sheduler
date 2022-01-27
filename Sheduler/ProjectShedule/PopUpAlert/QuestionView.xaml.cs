
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;


namespace ProjectShedule.PopUpAlert
{
    
    public partial class QuestionView : Popup<QuestionView.Answer>
    {
        public class Answer
        {
            public bool Value;
        }
        private readonly Answer _answer = new Answer();
        public QuestionView(string headerText = null, string secondaryText = null, string cancelText = "Cancel", string agreementText = "Ok", Size sizePopUp = new Size())
        {
            InitializeComponent();
            InicializateTexts(headerText, secondaryText, cancelText, agreementText);
            SetPopUpViewSize(sizePopUp);
        }

        private void InicializateTexts(string header, string secondary, string cancel, string agreement)
        {
            headerLabel.Text = header;
            secondaryLabel.Text = secondary;
            cancelationButton.Text = cancel;
            agreementButton.Text = agreement;
        }
        private void SetPopUpViewSize(Size size)
        {
            popUpView.Size = size;
        }
        protected override Answer GetLightDismissResult()
        {
            _answer.Value = false;
            return _answer;
        }
        private void CancelationButton_Clicked(object sender, System.EventArgs e)
        {
            _answer.Value = false;
            Dismiss(_answer);
        }
        private void AgreementButton_Clicked(object sender, System.EventArgs e)
        {
            _answer.Value = true;
            Dismiss(_answer);
        }
        
    }
}