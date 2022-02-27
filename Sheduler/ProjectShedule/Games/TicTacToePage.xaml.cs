using ProjectShedule.Calendar.Controls.ThemingDemo;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectShedule.Games
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TicTacToePage : ContentPage
    {
        private int PlayerStats { get; set; } = 0;
        private int BotStats { get; set; } = 0;
        public string ThemeTextInfo { get; set; }
        readonly List<Button> buttons = new List<Button>();
        public TicTacToePage()
        {
            try
            {
                InitializeComponent();
                PlayerStats = Convert.ToInt32(Preferences.Get("PlayerStat", "0"));
                BotStats = Convert.ToInt32(Preferences.Get("BotStat", "0"));
                DisplayGetStats();
                buttons = new List<Button> {
                    LeftDown, LeftMid, LeftUp,
                    MidDown, MidMid, MidUp,
                    RightDown, RightMid, RightUp };
                this.BindingContext = this;
            }
            catch (Exception e) { DisplayAlert("Ошибка", $"При Создании tic_tac_toe(): {e}", "Ок"); }
        }
        private async void BackButton_Click(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
        private void Press_TicTak_Button(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            button.Text = "X";
            if (LeftUp.Text != "" && MidUp.Text != "" && RightUp.Text != "" && LeftMid.Text != "" && MidMid.Text != "" && RightMid.Text != "" && LeftDown.Text != "" && MidDown.Text != "" && RightDown.Text != "")
            { DrawDialog(); ReturnInfoDisplay(", ничья"); }
            else
            {
                Razr = true;
                button.IsEnabled = false;
                bool value = WinChek("Ты");
                if (!value)
                {
                    BotStep();
                }
                else
                {
                    WinSave("Ты");
                    ReturnInfoDisplay(", Ты победил");
                }
            }
        }
        bool Razr = true;
        private void WinbleBotStep()
        {
            if (Razr) if (RightUp.Text == "") if (LeftUp.Text == "O" && MidUp.Text == "O" || LeftDown.Text == "O" && MidMid.Text == "O" || RightDown.Text == "O" && RightMid.Text == "O") { InsertsBot("logicsRU", RightUp); }
            if (Razr) if (MidUp.Text == "") if (LeftUp.Text == "O" && RightUp.Text == "O" || MidMid.Text == "O" && MidDown.Text == "O") { InsertsBot("logicsMU", MidUp); }
            if (Razr) if (LeftUp.Text == "") if (MidUp.Text == "O" && RightUp.Text == "O" || MidMid.Text == "O" && RightDown.Text == "O" || LeftDown.Text == "O" && LeftMid.Text == "O") { InsertsBot("logicsLU", LeftUp); }
            if (Razr) if (RightMid.Text == "") if (RightUp.Text == "O" && RightDown.Text == "O" || MidMid.Text == "O" && LeftMid.Text == "O") { InsertsBot("logicsRM", RightMid); }
            if (Razr) if (MidMid.Text == "") if (LeftUp.Text == "O" && RightDown.Text == "O" || RightUp.Text == "O" && LeftDown.Text == "O" || MidUp.Text == "O" && MidDown.Text == "O" || LeftMid.Text == "O" && RightMid.Text == "O") { InsertsBot("logicsMM", MidMid); }
            if (Razr) if (LeftMid.Text == "") if (LeftUp.Text == "O" && LeftDown.Text == "O" || MidMid.Text == "O" && RightMid.Text == "O") { InsertsBot("logicsLM", LeftMid); }
            if (Razr) if (RightDown.Text == "") if (MidMid.Text == "O" && LeftUp.Text == "O" || RightMid.Text == "O" && RightUp.Text == "O" || LeftDown.Text == "O" && MidDown.Text == "O") { InsertsBot("logicsRD", RightDown); }
            if (Razr) if (MidDown.Text == "") if (MidMid.Text == "O" && MidUp.Text == "O" || LeftDown.Text == "O" && RightDown.Text == "O") { InsertsBot("logicsMD", MidDown); }
            if (Razr) if (LeftDown.Text == "") if (MidMid.Text == "O" && RightUp.Text == "O" || LeftMid.Text == "O" && LeftUp.Text == "O" || MidDown.Text == "O" && RightDown.Text == "O") { InsertsBot("logicsLD", LeftDown); }
            if (Razr) if (RightUp.Text == "") if (LeftUp.Text == "X" && MidUp.Text == "X" || LeftDown.Text == "X" && MidMid.Text == "X" || RightDown.Text == "X" && RightMid.Text == "X") { InsertsBot("logicsRU", RightUp); }
            if (Razr) if (MidUp.Text == "") if (LeftUp.Text == "X" && RightUp.Text == "X" || MidMid.Text == "X" && MidDown.Text == "X") { InsertsBot("logicsMU", MidUp); }
            if (Razr) if (LeftUp.Text == "") if (MidUp.Text == "X" && RightUp.Text == "X" || MidMid.Text == "X" && RightDown.Text == "X" || LeftDown.Text == "X" && LeftMid.Text == "X") { InsertsBot("logicsLU", LeftUp); }
            if (Razr) if (RightMid.Text == "") if (RightUp.Text == "X" && RightDown.Text == "X" || MidMid.Text == "X" && LeftMid.Text == "X") { InsertsBot("logicsRM", RightMid); }
            if (Razr) if (MidMid.Text == "") if (LeftUp.Text == "X" && RightDown.Text == "X" || RightUp.Text == "X" && LeftDown.Text == "X" || MidUp.Text == "X" && MidDown.Text == "X" || LeftMid.Text == "X" && RightMid.Text == "X") { InsertsBot("logicsMM", MidMid); }
            if (Razr) if (LeftMid.Text == "") if (LeftUp.Text == "X" && LeftDown.Text == "X" || MidMid.Text == "X" && RightMid.Text == "X") { InsertsBot("logicsLM", LeftMid); }
            if (Razr) if (RightDown.Text == "") if (MidMid.Text == "X" && LeftUp.Text == "X" || RightMid.Text == "X" && RightUp.Text == "X" || LeftDown.Text == "X" && MidDown.Text == "X") { InsertsBot("logicsRD", RightDown); }
            if (Razr) if (MidDown.Text == "") if (MidMid.Text == "X" && MidUp.Text == "X" || LeftDown.Text == "X" && RightDown.Text == "X") { InsertsBot("logicsMD", MidDown); }
            if (Razr) if (LeftDown.Text == "") if (MidMid.Text == "X" && RightUp.Text == "X" || LeftMid.Text == "X" && LeftUp.Text == "X" || MidDown.Text == "X" && RightDown.Text == "X") { InsertsBot("logicsLD", LeftDown); }

        }
        private void InsertsBot(string WhatIsStep, Button button) // Можно параметры (Button button) без string
        {
            button.Text = "O"; RandomPermission = false; button.IsEnabled = false; BotStepInfo.Text = WhatIsStep; Razr = false;
        }
        private bool WinChek(string Who)
        {
            if (LeftDown.Text == MidDown.Text && MidDown.Text == RightDown.Text)
            { if (MidDown.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            else if (LeftMid.Text == MidMid.Text && MidMid.Text == RightMid.Text)
            { if (MidMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            else if (LeftUp.Text == MidUp.Text && MidUp.Text == RightUp.Text)
            { if (MidUp.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }

            else if (LeftUp.Text == MidMid.Text && MidMid.Text == RightDown.Text)
            { if (MidMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            else if (LeftDown.Text == MidMid.Text && MidMid.Text == RightUp.Text)
            { if (MidMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }

            else if (LeftUp.Text == LeftMid.Text && LeftMid.Text == LeftDown.Text)
            { if (LeftMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            else if (MidUp.Text == MidMid.Text && MidMid.Text == MidDown.Text)
            { if (MidMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            else if (RightUp.Text == RightMid.Text && RightMid.Text == RightDown.Text)
            { if (RightMid.Text != "") { TicTacNameText.Text = $"{Who} победил"; return true; } }
            return false;
        }
        bool RandomPermission = true;
        private void BotStep()
        {
            RandomPermission = true;
            WinbleBotStep();
            while (RandomPermission)
            {
                Random randomizer = new Random();
                int value = randomizer.Next(1, 10);
                switch (value)
                {
                    case 1:
                        if (LeftUp.Text == "") InsertsBot("RandomLU", LeftUp);
                        break;
                    case 2:
                        if (MidUp.Text == "") InsertsBot("RandomMU", MidUp);
                        break;
                    case 3:
                        if (RightUp.Text == "") InsertsBot("RandomRU", RightUp);
                        break;
                    case 4:
                        if (LeftMid.Text == "") InsertsBot("RandomLM", LeftMid);
                        break;
                    case 5:
                        if (MidMid.Text == "") InsertsBot("RandomMM", MidMid);
                        break;
                    case 6:
                        if (RightMid.Text == "") InsertsBot("RandomRM", RightMid);
                        break;
                    case 7:
                        if (LeftDown.Text == "") InsertsBot("RandomLD", LeftDown);
                        break;
                    case 8:
                        if (MidDown.Text == "") InsertsBot("RandomMD", MidDown);
                        break;
                    case 9:
                        if (RightDown.Text == "") InsertsBot("RandomRD", RightDown);
                        break;
                }
            }
            bool chek = WinChek("Бот");
            if (chek)
            {
                WinSave("Бот");
                ReturnInfoDisplay(", Бот победил");
            }
        }
        private void DrawDialog()
        {
            TicTacNameText.Text = "Ничья";
            ButtonsBlock(false);
        }
        private async void ReturnInfoDisplay(string How)
        {
            bool value = await DisplayAlert($"Игра закончена{How}", "Начать заного?", "Да", "Нет");
            if (!value) { await Navigation.PopAsync(); }
            else { ButtonsBlock(true); TicTacNameText.Text = "Крестики нолики"; }
        }
        private void ButtonsBlock(bool value)
        {
            if (value) // Разблокировать и отчистить 
            {
                foreach (Button button in buttons)
                {
                    button.Text = "";
                }
            }
            // Все кнопки становаятся value - true/false
            foreach (Button button in buttons)
            {
                button.IsEnabled = value;
            }
        }
        private void WinSave(string Who)
        {
            if (Who == "Ты")
            {
                PlayerStats += 1;
                Preferences.Set("PlayerStat", Convert.ToString(PlayerStats));
                DisplayGetStats();
            }
            else
            {
                BotStats += 1;
                Preferences.Set("BotStat", Convert.ToString(BotStats));
                DisplayGetStats();
            }
            ButtonsBlock(false);
        }
        private void DisplayGetStats()
        {
            try
            {
                YouTextStat.Text = $"Ты = {PlayerStats}";
                BotTextStat.Text = $"Бот = {BotStats}";
            }
            catch (Exception e) { DisplayAlert("Ошибка", $"При DisplayGetStats(): {e}", "Ок"); }
        }
    }
}