﻿using ProjectShedule.Other;
using ProjectShedule.Shedule.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Shedule
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShedulePage : ContentPage
    {
        private readonly BaseViewElementAnimate _animations;
        public ShedulePage()
        {
            InitializeComponent();
            BindingContext = new SheduleViewModel() { Navigation = this.Navigation };
            _animations = new BouncingAnimatedViewElement();
        }
        public bool IsAnimated { get; protected set; }
        private async void ViewAnimatedPush(object sender, EventArgs e)
        {
            if (!IsAnimated && sender is VisualElement visualElement)
            {
                await _animations.SinInElementAsync(visualElement, () => IsAnimated = false);
            }
        }
    }
}