using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriTrack.Desktop.Models;
using NutriTrack.Desktop.Services;
using System;
using System.Windows.Input;

namespace NutriTrack.Desktop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        private readonly NutritionService _service = new();

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
            if (!double.TryParse(WeightInput, out var weight) ||
                !double.TryParse(HeightInput, out var height) ||
                !int.TryParse(AgeInput, out var age) ||
                !double.TryParse(BodyFatInput, out var bodyFat))
            {
                SetError(); return;
            }

            var entry = new NutritionEntry
            {
                Weight = weight,
                Height = height,
                Age = age,
                BodyFat = bodyFat,
                Gender = Gender,
                Goal = Goal,
                Activity = ActivityLevel
            };

            var result = _service.Calculate(entry);

            CaloriesText = $"{result.Calories:F0} kcal";
            ProteinText = $"{result.Protein:F0} g";
            FatText = $"{result.Fat:F0} g";
            CarbsText = $"{result.Carbs:F0} g";
        }

        private void SetError()
        {
            CaloriesText = ProteinText = FatText = CarbsText = "Ошибка";
        }
    }
}
