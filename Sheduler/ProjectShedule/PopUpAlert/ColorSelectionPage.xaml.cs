using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.PopUpAlert
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ColorSelectionPage : Rg.Plugins.Popup.Pages.PopupPage, IColorSelection
    {
        private Target _currentTarget;

        public static bool IsPageOpened { get; private set; }
        public delegate void TargetDelegate(Target target);

        public event TargetDelegate TargetSelected;

        public ColorSelectionPage(PackNoteViewModel packNoteViewModel)
        {
            InitializeComponent();
            packNoteView.BindingContext = packNoteViewModel;

            BackGround = new BackGround(packNoteViewModel) { DesignatingButton = buttonResponsibleBackGroundColor };
            Line = new Line(packNoteViewModel) { DesignatingButton = buttonResponsibleLineColor };

            InicializateSelectTarget();
        }

        public Target CurrentTarget
        {
            get => _currentTarget;
            set
            {
                if (_currentTarget == value)
                    return;

                _currentTarget.DesignatingButton.BorderColor = Color.Default;
                _currentTarget = value;
                _currentTarget.DesignatingButton.BorderColor = Color.Black;
                TargetSelected?.Invoke(value);
            }
        }
        public Target Line { get; set; }
        public Target BackGround { get; set; }
        private void InicializateSelectTarget()
        {
            _currentTarget = BackGround;
            _currentTarget.DesignatingButton.BorderColor = Color.Black;
        }
        private void PressOnColoredButton(object sender, EventArgs e)
        {
            if (sender is Button button)
                CurrentTarget.Color = button.BackgroundColor;
        }

        private void LineSelectButtonClicked(object sender, EventArgs e) => CurrentTarget = Line;
        private void BackGroundSelectButtonClicked(object sender, EventArgs e) => CurrentTarget = BackGround;

        protected override void OnAppearing()
        {
            base.OnAppearing();

            IsPageOpened = true;
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            IsPageOpened = false;
        }
    }
    public interface IColorSelection
    {
        public Target Line { get; set; }
        public Target BackGround { get; set; }
    }
    public abstract class Target
    {
        private Color _color;

        public delegate void ColorDelegate(Color color);
        public event ColorDelegate ColorSelected;
        protected PackNoteViewModel PackNoteViewModel { get; set; }
        public Button DesignatingButton { get; set; }
        public virtual Color Color
        {
            get => _color;
            set
            {
                _color = value;
                ColorSelected?.Invoke(value);
            }
        }
    }
    public class BackGround : Target
    {
        public BackGround(PackNoteViewModel packNoteViewModel)
        {
            PackNoteViewModel = packNoteViewModel;
        }
        public override Color Color
        {
            get => base.Color;
            set
            {
                PackNoteViewModel.BackGroundColor = value;
                base.Color = value;
            }
        }
    }
    public class Line : Target
    {
        public Line(PackNoteViewModel packNoteViewModel)
        {
            PackNoteViewModel = packNoteViewModel;
        }
        public override Color Color 
        { 
            get => base.Color;
            set 
            {
                PackNoteViewModel.LineColor = value;
                base.Color = value;
            }  
        }
    }
}