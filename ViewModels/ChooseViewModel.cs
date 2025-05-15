using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using NutriTrack.Desktop.Models;
using System.Windows.Input;

namespace NutriTrack.Desktop.ViewModels
{
    public partial class ChooseViewModel : ObservableObject
    {
        public ChooseViewModel()
        {
            SelectMaleCommand = new RelayCommand(() => SelectGender("Male"));
            SelectFemaleCommand = new RelayCommand(() => SelectGender("Female"));
        }

        public ICommand SelectMaleCommand { get; }
        public ICommand SelectFemaleCommand { get; }

        private void SelectGender(string gender)
        {
            NutritionEntry.SelectedGender = gender;
        }
    }
}
