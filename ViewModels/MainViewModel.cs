using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Windows.Input;

namespace NutriTrack.Desktop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string weightInput = "";
        [ObservableProperty] private string heightInput = "";
        [ObservableProperty] private string ageInput = "";
        [ObservableProperty] private string bodyFatInput = "";

        [ObservableProperty] private string gender = "Male";
        [ObservableProperty] private string goal = "Maintain";
        [ObservableProperty] private string activityLevel = "Moderate";

        [ObservableProperty] private string caloriesText = "--";
        [ObservableProperty] private string proteinText = "--";
        [ObservableProperty] private string fatText = "--";
        [ObservableProperty] private string carbsText = "--";

        public ICommand CalculateCommand { get; }

        public MainViewModel()
        {
            CalculateCommand = new RelayCommand(Calculate);
        }

        private void Calculate()
        {
            if (!double.TryParse(WeightInput, out double weight) ||
                !double.TryParse(HeightInput, out double height) ||
                !int.TryParse(AgeInput, out int age) ||
                !double.TryParse(BodyFatInput, out double fatPercent))
            {
                SetError();
                return;
            }

            double leanMass = weight * (1 - fatPercent / 100);

            double bmr = Gender == "Female"
                ? 10 * weight + 6.25 * height - 5 * age - 161
                : 10 * weight + 6.25 * height - 5 * age + 5;

            double activity = ActivityLevel switch
            {
                "Low" => 1.2,
                "Light" => 1.375,
                "Moderate" => 1.55,
                "High" => 1.725,
                "Very High" => 1.9,
                _ => 1.55
            };

            double tdee = bmr * activity;

            tdee *= Goal switch
            {
                "Cut" => 0.8,
                "Bulk" => 1.15,
                _ => 1.0
            };

            double protein = leanMass * 2.2;
            double fat = leanMass * 0.8;
            double proteinKcal = protein * 4;
            double fatKcal = fat * 9;
            double carbsKcal = tdee - (proteinKcal + fatKcal);
            double carbs = carbsKcal > 0 ? carbsKcal / 4 : 0;

            CaloriesText = $"{tdee:F0} kcal";
            ProteinText = $"{protein:F0} g";
            FatText = $"{fat:F0} g";
            CarbsText = $"{carbs:F0} g";
        }
        private void SetError()
        {
            CaloriesText = ProteinText = FatText = CarbsText = "Ошибка";
        }
    }
}
