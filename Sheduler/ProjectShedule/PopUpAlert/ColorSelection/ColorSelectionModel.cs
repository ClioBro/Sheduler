using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public interface IColorSelection
    {
        event EventHandler<ITarget> TargetSelected;
        event EventHandler<Color> ColorSelected;
        string HeaderText { get; }
        ITarget CurrentTarget { get; }
        ITarget LineTarget { get; }
        ITarget BackGroundTarget { get; }
        void SetColorInCurrentTarget(Color color);
        void SetCurrentTarget(ITarget target);
    }
    public class ColorSelectionModel : IColorSelection
    {
        private Target _currentTarget;
        private readonly Target _lineTarget;
        private readonly Target _backGroundTarget;

        public event EventHandler<ITarget> TargetSelected;
        public event EventHandler<Color> ColorSelected;
        public ColorSelectionModel() 
            : this(headerText: "", lineTargetText: "lineTarget", backGroundText: "BackGroundTarget") { }
        public ColorSelectionModel(string headerText, string lineTargetText, string backGroundText)
        {
            _lineTarget = new Target() { Text = lineTargetText };
            _backGroundTarget = new Target() { Text = backGroundText };
            HeaderText = headerText;

            _currentTarget = _backGroundTarget;
            _currentTarget.IsSelected = true;
        }
        public string HeaderText { get; set; }
        public ITarget CurrentTarget => _currentTarget;
        public ITarget LineTarget => _lineTarget;
        public ITarget BackGroundTarget => _backGroundTarget;
        public void SetColorInCurrentTarget(Color color)
        {
            _currentTarget.TiedColor = color;
            ColorSelected?.Invoke(this, color);
        }
        public void SetCurrentTarget(ITarget target)
        {
            if (target is Target isTarget && _currentTarget != isTarget)
            {
                _currentTarget.IsSelected = false;
                _currentTarget = isTarget;
                _currentTarget.IsSelected = true;
                TargetSelected?.Invoke(this, target);
            }
        }
    }

    public class ColorSelectionPackNoteModel : ColorSelectionModel
    {
        public BasePackNoteViewModel PackNoteViewModel { get; set; }
        public ColorSelectionPackNoteModel(BasePackNoteViewModel packNoteViewModel, string headerText, string lineTargetText, string backGroundText)
            : base(headerText, lineTargetText, backGroundText)
        {
            PackNoteViewModel = packNoteViewModel;
            BackGroundTarget.ColorSelected += OnBackGroundTargetColorChanged;
            LineTarget.ColorSelected += OnLineTargetColorChanged;
        }
        private void OnLineTargetColorChanged(object sender, Color color)
        {
            PackNoteViewModel.LineColor = color;
        }
        private void OnBackGroundTargetColorChanged(object sender, Color color)
        {
            PackNoteViewModel.BackGroundColor = color;
        }
    }
}
