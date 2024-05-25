using GameLauncher.UserControls;
using GameLauncher.ViewModels;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameLauncher
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            GameItemUC gameItemUC = new GameItemUC();
            gameItemUC.Width = 750;
            gameItemUC.Height = 800;
            gameItemUC.DataContext = new GameItemVM() {Name = "Application", Exe = "" }; // Input your own test for the exe path
            placehere.Children.Add(gameItemUC);
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized; // No toggle or switch needed since minimized state cannot be invoked when the app is not open
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState) // Toggles 
            {
                case WindowState.Maximized:
                    this.WindowState = WindowState.Normal;
                    break;
                case WindowState.Normal:
                    this.WindowState = WindowState.Maximized;
                    break;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Fully shutdown app correctly
        }
    }
}