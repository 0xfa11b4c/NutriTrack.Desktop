using NutriTrack.Desktop.Core;
using NutriTrack.Desktop.Models;
using NutriTrack.Desktop.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NutriTrack.Desktop.Views
{
    public partial class ChooseWindow : Window
    {
        private string _selectedGoal = "Maintain";
        private NutritionEntry? _tempEntry;
        private readonly List<string> _allergies = new();
        private readonly List<string> _conditions = new();

        public ChooseWindow()
        {
            InitializeComponent();
        }

        private void Male_Click(object sender, RoutedEventArgs e)
        {
            NutritionEntry.SelectedGender = "Male";
            ShowStep(Step2Panel);
        }

        private void Female_Click(object sender, RoutedEventArgs e)
        {
            NutritionEntry.SelectedGender = "Female";
            ShowStep(Step2Panel);
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

            double.TryParse(FatBox.Text, out double fat);

            _tempEntry = new NutritionEntry
            {
                Gender = NutritionEntry.SelectedGender ?? "Male",
                Weight = weight,
                Height = height,
                Age = age,
                BodyFat = fat
            };

            ShowStep(Step3Panel);
        }

        private void Goal_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.Tag is string goal)
            {
                _selectedGoal = goal;

                foreach (var child in GoalWrapPanel.Children)
                {
                    if (child is Button b)
                        b.Opacity = 0.5;
                }

                button.Opacity = 1.0;
            }
        }

        private void AddAllergy_Click(object sender, RoutedEventArgs e) => AddAllergy();
        private void AddCondition_Click(object sender, RoutedEventArgs e) => AddCondition();

        private void AllergyInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AddAllergy();
        }

        private void ConditionInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) AddCondition();
        }

        private void AddAllergy()
        {
            var value = AllergyInput.Text.Trim();
            if (!string.IsNullOrWhiteSpace(value) && !_allergies.Contains(value))
            {
                _allergies.Add(value);
                AllergyTags.Children.Add(CreateTag(value, _allergies, AllergyTags));
                AllergyInput.Clear();
            }
        }

        private void AddCondition()
        {
            var value = ConditionInput.Text.Trim();
            if (!string.IsNullOrWhiteSpace(value) && !_conditions.Contains(value))
            {
                _conditions.Add(value);
                ConditionTags.Children.Add(CreateTag(value, _conditions, ConditionTags));
                ConditionInput.Clear();
            }
        }

        private Border CreateTag(string text, List<string> list, Panel container)
        {
            var tag = new TextBlock
            {
                Text = text,
                Foreground = Brushes.White,
                Margin = new Thickness(5, 0, 5, 0),
                VerticalAlignment = VerticalAlignment.Center
            };

            var remove = new Button
            {
                Content = "✕",
                Width = 20,
                Height = 20,
                Padding = new Thickness(0),
                Margin = new Thickness(5, 0, 0, 0),
                Background = Brushes.Transparent,
                BorderThickness = new Thickness(0),
                Foreground = Brushes.White,
                Cursor = Cursors.Hand
            };

            var border = new Border
            {
                Background = new SolidColorBrush(Color.FromRgb(85, 0, 130)), // Фиолетовый фон
                BorderThickness = new Thickness(1),
                BorderBrush = Brushes.White,
                CornerRadius = new CornerRadius(10),
                Margin = new Thickness(5),
                Padding = new Thickness(10, 5, 10, 5)
            };

            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(tag);
            panel.Children.Add(remove);
            border.Child = panel;

            remove.Click += (_, _) =>
            {
                list.Remove(text);
                container.Children.Remove(border);
            };

            return border;
        }

        private void ContinueToAllergy_Click(object sender, RoutedEventArgs e)
        {
            ShowStep(Step4Panel);
        }

        private void ContinueToActivity_Click(object sender, RoutedEventArgs e)
        {
            if (_tempEntry == null) return;

            _tempEntry.Allergies = _allergies;
            _tempEntry.Conditions = _conditions;

            ShowStep(Step5Panel);
        }

        private void ShowStep(StackPanel panelToShow)
        {
            Step1Panel.Visibility = Visibility.Collapsed;
            Step2Panel.Visibility = Visibility.Collapsed;
            Step3Panel.Visibility = Visibility.Collapsed;
            Step4Panel.Visibility = Visibility.Collapsed;
            Step5Panel.Visibility = Visibility.Collapsed;

            panelToShow.Visibility = Visibility.Visible;
        }

        private void OpenMain_Click(object sender, RoutedEventArgs e)
        {
            if (_tempEntry is null) return;

            _tempEntry.Goal = _selectedGoal;
            _tempEntry.Allergies = _allergies;
            _tempEntry.Conditions = _conditions;

            if (ActivityComboBox.SelectedItem is ComboBoxItem selectedItem &&
                selectedItem.Tag is string tag)
            {
                _tempEntry.Activity = tag;
            }

            AppState.CurrentEntry = _tempEntry;
            PersistenceService.Save(_tempEntry);

            new MainWindow().Show();
            this.Close();
        }
    }
}
