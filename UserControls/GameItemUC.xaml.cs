using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GameLauncher.UserControls
{
    /// <summary>
    /// Interaction logic for GameItemUC.xaml
    /// </summary>
    public partial class GameItemUC : UserControl
    {
        public GameItemUC()
        {
            InitializeComponent();
            this.Loaded += GameItemUC_Loaded;
        }


        private void GameItemUC_Loaded(object sender, RoutedEventArgs e)
        {
            // The set starting collection of grids availible for resizing (Change this to be more modular in the future and load later during runtime to include all children of the "placehere" grid e.g. menu and gameitemvms)
            /*List<Grid> myGrids = new List<Grid>
            {
                TopBar,
                placehere
                // Add all other Grids here
            };*/

            ((App)Application.Current).PM.AutoScaler.ChangeResolution((int)Width, (int)Height);
            //StoreInitialDimensions(myGrids);
        }


        public void UpdateElement(Grid element)
        {
            if (Application.Current != null)
            {

                // Check if initial dimensions are already stored, if not, store them
                if (!((App)Application.Current).PM.InitialDimensions.ContainsKey(element))
                {
                    Debug.WriteLine("noteset");
                    ((App)Application.Current).PM.InitialDimensions[element] = new Tuple<int, int>((int)element.Width, (int)element.Height);
                }

                // Retrieve initial dimensions
                var initialSize = ((App)Application.Current).PM.InitialDimensions[element];

                Debug.WriteLine($"Initial size for element: Width={initialSize.Item1}, Height={initialSize.Item2}");
                Debug.WriteLine($"Current resolution: Width={((App)Application.Current).PM.AutoScaler.CurrentResolution[0]}, Height={((App)Application.Current).PM.AutoScaler.CurrentResolution[1]}");

                // Get the size ratios
                List<double> sizeRatios = ((App)Application.Current).PM.AutoScaler.GetObjectSize((int)initialSize.Item1, (int)initialSize.Item2);
                Debug.WriteLine($"Size ratios: Width={sizeRatios[0]}, Height={sizeRatios[1]}");

                // Scale the element size according to current resolution
                int width = (int)(((App)Application.Current).PM.AutoScaler.CurrentResolution[0] * sizeRatios[0]);
                int height = (int)(((App)Application.Current).PM.AutoScaler.CurrentResolution[1] * sizeRatios[1]);


                Debug.WriteLine($"Calculated size for element: Width={width}, Height={height}");


                // Update the element size
                element.Width = width;
                element.Height = height;

                Debug.WriteLine(element.Width + ", " + element.Height);
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
    }
}
