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

        }
    }
}
