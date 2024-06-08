using GameLauncher.Items;
using GameLauncher.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace GameLauncher
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<GameItemVM> Games { get; set; }
        public MainWindow()
        {
            InitializeComponent();

            Games = new ObservableCollection<GameItemVM>
            {
                new GameItemVM("Firewatch", @"F:\Games\Firewatch\Firewatch.exe"),
                new GameItemVM("Assassin's Creed IV Black Flag", @"F:\Games\Assassin's Creed IV Black Flag\AC4BFSP.exe"),
                // Add more game items as needed
            };
            DataContext = this;
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield break;
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject ithChild = VisualTreeHelper.GetChild(depObj, i);
                if (ithChild == null) continue;
                if (ithChild is T t) yield return t;
                foreach (T childOfChild in FindVisualChildren<T>(ithChild)) yield return childOfChild;
            }
        }

        private void TopBar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            switch (this.WindowState)
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
            Application.Current.Shutdown();
        }

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://github.com/BusterLandstrom/Buster-GL-Game-Launcher", UseShellExecute = true });
        }

        private void Wiki_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://github.com/BusterLandstrom/Buster-GL-Game-Launcher/wiki", UseShellExecute = true });
        }

        private void Preferences_Click(object sender, RoutedEventArgs e)
        {
            SettingsGrid.Visibility = Visibility.Visible;
        }
    }
}
