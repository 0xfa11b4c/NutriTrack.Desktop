using System;
using System.Windows;
using NutriTrack.Desktop.Models;

namespace NutriTrack.Desktop.Views
{
    public partial class ChooseWindow : Window
    {
        private string _selectedGoal = "Maintain";
        private NutritionEntry _tempEntry;

        public ChooseWindow()
        {
            InitializeComponent();
        }

        private void Male_Click(object sender, RoutedEventArgs e)
        {
            NutritionEntry.SelectedGender = "Male";
            Step1Panel.Visibility = Visibility.Collapsed;
            Step2Panel.Visibility = Visibility.Visible;
        }

        private void Female_Click(object sender, RoutedEventArgs e)
        {
            NutritionEntry.SelectedGender = "Female";
            Step1Panel.Visibility = Visibility.Collapsed;
            Step2Panel.Visibility = Visibility.Visible;
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(WeightBox.Text, out double weight) ||
                !double.TryParse(HeightBox.Text, out double height) ||
                !int.TryParse(AgeBox.Text, out int age))
            {
                MessageBox.Show("Пожалуйста, введите корректные данные");
                return;
            }

            double.TryParse(FatBox.Text, out double fat); // optional

            _tempEntry = new NutritionEntry
            {
                Gender = NutritionEntry.SelectedGender ?? "Male",
                Weight = weight,
                Height = height,
                Age = age,
                BodyFat = fat
            };

            Step2Panel.Visibility = Visibility.Collapsed;
            Step3Panel.Visibility = Visibility.Visible;
        }

        private void Goal_Click(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement button && button.Tag is string goal)
            {
                _selectedGoal = goal;
            }
        }

        private void OpenMain_Click(object sender, RoutedEventArgs e)
        {
            _tempEntry.Goal = _selectedGoal;

            // Можно сохранить в AppState или передать напрямую
            App.Current.Properties["InitialEntry"] = _tempEntry;

            var main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}
