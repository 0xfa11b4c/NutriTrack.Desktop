using System.Windows;

namespace NutriTrack.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void OpenMealPlan_Click(object sender, RoutedEventArgs e)
        {
            /*var mealWindow = new MealPlanWindow();
            mealWindow.Show();
            this.Close();*/
        }

        private void GoToDashboard_Click(object sender, RoutedEventArgs e)
        {
            // Уже на главной — ничего не делаем
        }

        private void OpenMealPlan_Click(object sender, RoutedEventArgs e)
        {
            var mealWindow = new MealPlanWindow();
            mealWindow.Show();
            this.Close();
        }

        private void OpenExercise_Click(object sender, RoutedEventArgs e)
        {
            var exerciseWindow = new ExerciseWindow();
            exerciseWindow.Show();
            this.Close();
        }

        private void OpenHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryWindow();
            historyWindow.Show();
            this.Close();
        }

        private void OpenProducts_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.Show();
            this.Close();
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            var settingsWindow = new SettingsWindow();
            settingsWindow.Show();
            this.Close();
        }
    }
}
