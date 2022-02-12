using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection
{
    public interface IColorSelection
    {
        event EventHandler<Target> TargetSelected;
        ITarget CurrentTarget { get; }
        ITarget LineTarget { get; }
        ITarget BackGroundTarget { get; }
    }
    public class ColorSelectionModel : IColorSelection
    {
        private Target _currentTarget;
        private readonly Target _lineTarget;
        private readonly Target _backGroundTarget;

        public event EventHandler<Target> TargetSelected;
        public ColorSelectionModel() : this(new PackNoteViewModel(), headerText: "", lineTargetText: "lineTarget", backGroundText: "BackGroundTarget") { }
        public ColorSelectionModel(PackNoteViewModel packNoteViewModel, string headerText, string lineTargetText, string backGroundText)
        {
            PackNoteViewModel = packNoteViewModel;
            _lineTarget = new Target() { Text = lineTargetText };
            _backGroundTarget = new Target() { Text = backGroundText };
            HeaderText = headerText;

            _currentTarget = _backGroundTarget;
            _currentTarget.IsSelected = true;

            _lineTarget.ColorSelected += OnLineTargetColorChanged;
            _backGroundTarget.ColorSelected += OnBackGroundTargetChanged;
        }

        public PackNoteViewModel PackNoteViewModel { get; set; }
        public string HeaderText { get; }
        public ITarget CurrentTarget => _currentTarget;

        public ITarget LineTarget => _lineTarget;
        public ITarget BackGroundTarget => _backGroundTarget;
        public void SetColorInCurrentTarget(Color color) => _currentTarget.TiedColor = color;
        public void SetLineTarget() => SetCurrentTarget(_lineTarget);
        public void SetBackGroundTarget() => SetCurrentTarget(_backGroundTarget);
        private void SetCurrentTarget(Target target)
        {
            if (_currentTarget == target)
                return;
            _currentTarget.IsSelected = false;
            _currentTarget = target;
            _currentTarget.IsSelected = true;
            TargetSelected?.Invoke(this, target);
        }

        private void OnBackGroundTargetChanged(object sender, Color e)
        {
            PackNoteViewModel.BackGroundColor = e;
        }
        private void OnLineTargetColorChanged(object sender, Color e)
        {
            PackNoteViewModel.LineColor = e;
        }
    }
}
