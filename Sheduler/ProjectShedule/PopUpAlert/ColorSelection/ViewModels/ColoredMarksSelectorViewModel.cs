using ProjectShedule.Core;
using ProjectShedule.PopUpAlert.ColorSelection.Models;
using System.Collections.Generic;
using System.Windows.Input;
using System;
using Xamarin.Forms;

namespace ProjectShedule.PopUpAlert.ColorSelection.ViewModels
{
    public class ColoredMarksSelectorViewModel : FlexLayoutViewModel
    {
        private readonly Dictionary<Color, ColoredMarkViewModel> keyValuePairs = new Dictionary<Color, ColoredMarkViewModel>();
        private readonly ColorsModel _colorsModel;
        private ColoredMarkViewModel _selectedColoredMarkViewModel;

        public ColoredMarksSelectorViewModel(ColorsModel colorsModel)
        {
            _colorsModel = colorsModel;
            PressedCommand = new Command<ColoredMarkViewModel>(OnPressedCommandHandler);
            Inicialization();

            Direction = FlexDirection.Row;
            Wrap = FlexWrap.Wrap;
            AlignContent = FlexAlignContent.SpaceAround;
            AlignItems = FlexAlignItems.Center;
            JustifyContent = FlexJustify.SpaceAround;
        }

        public IEnumerable<IColoredMark> ColoredMarks => keyValuePairs.Values;
        public Color SelectedColor
        {
            get => SelectedColoredBoxViewModel.ValueColor;
            set
            {
                if (keyValuePairs.ContainsKey(value) == false)
                    throw new ArgumentException($"No have this color in collection. Color: {value}");
                SelectedColoredBoxViewModel = keyValuePairs[value];
            }
        }

        private ICommand PressedCommand { get; set; }
        private ColoredMarkViewModel SelectedColoredBoxViewModel
        {
            get => _selectedColoredMarkViewModel;
            set
            {
                if (value == _selectedColoredMarkViewModel)
                    return;
                _selectedColoredMarkViewModel?.SwichState();
                value.SwichState();
                _selectedColoredMarkViewModel = value;
                NotifyProperty(nameof(SelectedColor));
            }
        }
        private void Inicialization()
        {
            foreach (Color color in _colorsModel.Colors)
            {
                if (keyValuePairs.ContainsKey(color))
                    continue;
                keyValuePairs.Add(color, BuildColoredBoxViewModel(color));
            }
        }
        private ColoredMarkViewModel BuildColoredBoxViewModel(Color color)
        {
            return new ColoredMarkViewModel(new ColoredMarkModel(color))
            {
                PressedCommand = PressedCommand,
            };
        }
        private void OnPressedCommandHandler(ColoredMarkViewModel coloredBoxViewModel)
        {
            SelectedColoredBoxViewModel = coloredBoxViewModel;
        }
    }
}
