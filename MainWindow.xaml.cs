using GameLauncher.Items;
using GameLauncher.UserControls;
using GameLauncher.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Security.Policy;
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
        public ObservableCollection<GameItemVM> Games { get; set; }

        public MainWindow()
        {
            InitializeComponent();

            Games = new ObservableCollection<GameItemVM>
            {
                new GameItemVM("Firewatch", @"F:\Games\Firewatch\Firewatch.exe"),
                new GameItemVM("Ass", @"F:\Games\Assassin's Creed IV Black Flag\AC4BFSP.exe"),
                // Add more game items as needed
            };
            DataContext = this;
            this.Loaded += MainWindow_Loaded;
            this.SizeChanged += MainWindow_SizeChanged;
        }



        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // The set starting collection of grids availible for resizing (Change this to be more modular in the future and load later during runtime to include all children of the "placehere" grid e.g. menu and gameitemvms)
            List<Grid> myGrids = new List<Grid>
            {
                TopBar
                // Add all other Grids here
            };

            ((App)Application.Current).PM.AutoScaler.ChangeResolution((int)Width, (int)Height);
            StoreInitialDimensions(myGrids);
        }

        private void MainWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // Trigger the UpdateElement method for each element
            foreach (var element in ((App)Application.Current).PM.InitialDimensions.Keys)
            {
                ((App)Application.Current).PM.AutoScaler.ChangeResolution((int)Width, (int)Height);
                UpdateElement(element);
            }
        }

        public void UpdateElement(Grid element)
        {
            if (Application.Current != null)
            {

                // Check if initial dimensions are already stored, if not, store them
                if (!((App)Application.Current).PM.InitialDimensions.ContainsKey(element))
                {
                    Debug.WriteLine("noteset");
                    ((App)Application.Current).PM.InitialDimensions[element] = new Tuple<int, int>((int)element.ActualWidth, (int)element.ActualHeight);
                }

                // Retrieve initial dimensions
                var initialSize = ((App)Application.Current).PM.InitialDimensions[element];

                // Get the size ratios
                List<double> sizeRatios = ((App)Application.Current).PM.AutoScaler.GetObjectSize((int)initialSize.Item1, (int)initialSize.Item2);

                // Scale the element size according to current resolution
                int width = (int)(((App)Application.Current).PM.AutoScaler.CurrentResolution[0] * sizeRatios[0]);
                int height = (int)element.ActualHeight;
                if (element.Name != "TopBar")
                {
                    height = (int)(((App)Application.Current).PM.AutoScaler.CurrentResolution[1] * sizeRatios[1]);
                }


                // Update the element size
                element.Width = width;
                element.Height = height;
            }
        }

        private void StoreInitialDimensions(IEnumerable<Grid> elements)
        {
            if (Application.Current != null)
            {
                foreach (var element in elements)
                {
                    if (!((App)Application.Current).PM.InitialDimensions.ContainsKey(element))
                    {
                        ((App)Application.Current).PM.InitialDimensions[element] = new Tuple<int, int>((int)element.ActualWidth, (int)element.ActualHeight);
                    }
                }
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj == null) yield return (T)Enumerable.Empty<T>();
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

        private void GitHub_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://github.com/BusterLandstrom/Buster-GL-Game-Launcher", UseShellExecute = true });
        }

        private void Wiki_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(new ProcessStartInfo { FileName = @"https://github.com/BusterLandstrom/Buster-GL-Game-Launcher/wiki", UseShellExecute = true });
        }
    }
}