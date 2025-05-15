using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriTrack.Desktop.Models;
using NutriTrack.Desktop.Services;
using System.Windows.Input;

namespace NutriTrack.Desktop.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty] private string _weightInput = "";
        [ObservableProperty] private string _heightInput = "";
        [ObservableProperty] private string _ageInput = "";
        [ObservableProperty] private string _bodyFatInput = "";

        [ObservableProperty] private string _gender = NutritionEntry.SelectedGender;
        [ObservableProperty] private string _goal = "Maintain";
        [ObservableProperty] private string _activityLevel = "Moderate";

        [ObservableProperty] private string _caloriesText = "--";
        [ObservableProperty] private string _proteinText = "--";
        [ObservableProperty] private string _fatText = "--";
        [ObservableProperty] private string _carbsText = "--";
        [ObservableProperty] private string _normalWeightText = "--";

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

            var input = new NutritionEntry
            {
                Weight = weight,
                Height = height,
                Age = age,
                BodyFat = fatPercent,
                Gender = Gender,
                Goal = Goal,
                Activity = ActivityLevel
            };

            var service = new NutritionService();
            var result = service.Calculate(input);

            double normalWeight = Gender == "Female"
                ? (height - 100) * 0.85
                : (height - 100) * 0.9;

            CaloriesText = $"{result.Calories:F0} kcal";
            ProteinText = $"{result.Protein:F0}g";
            FatText = $"{result.Fat:F0}g";
            CarbsText = $"{result.Carbs:F0}g";
            NormalWeightText = $"{normalWeight:F1} kg";
        }

        private void SetError()
        {
            CaloriesText = ProteinText = FatText = CarbsText = NormalWeightText = "Error";
        }
    }
}
