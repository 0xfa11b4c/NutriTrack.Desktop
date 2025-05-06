using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace NutriTrack.Desktop.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && (tb.Text == "Enter weight" || tb.Text == "Enter body fat"))
            {
                tb.Text = "";
                tb.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null && string.IsNullOrWhiteSpace(tb.Text))
            {
                if (tb.Name == "WeightTextBox")
                    tb.Text = "Enter weight";
                else if (tb.Name == "FatTextBox")
                    tb.Text = "Enter body fat";

                tb.Foreground = Brushes.Gray;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Entry added (placeholder)");
        }
    }
}