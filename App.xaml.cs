using System.Windows;
using NutriTrack.Desktop.Views;
using NutriTrack.Desktop.Services;
using NutriTrack.Desktop.Core;

namespace NutriTrack.Desktop
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            PersistenceService.Load();

            Window startWindow = AppState.CurrentEntry == null
                ? new ChooseWindow()
                : new MainWindow();

            startWindow.Show();
        }
    }
}