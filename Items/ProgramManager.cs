using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GameLauncher.Items
{
    public class ProgramManager : INotifyPropertyChanged
    {
        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        // Start of variables

        private Theme _currentTheme;
        public Theme CurrentTheme 
        {
            get {  return _currentTheme; }
            set 
            { 
                _currentTheme = value;
                OnPropertyChanged(nameof(CurrentTheme));
            }
        }

        private AutoScaler _autoScaler;
        public AutoScaler AutoScaler
        {
            get { return _autoScaler; }
            set
            {
                _autoScaler = value;
                OnPropertyChanged(nameof(AutoScaler));
            }
        }

        private MainWindow _mw;
        public MainWindow MW 
        { 
            get { return _mw; } 
            set 
            { 
                _mw = value;
                OnPropertyChanged(nameof(MW));
            }
        
        }


        private Dictionary<Grid, Tuple<int, int>> _initialDimensions;
        public Dictionary<Grid, Tuple<int, int>> InitialDimensions 
        {
            get { return _initialDimensions; }
            set
            {
                _initialDimensions = value;
                OnPropertyChanged(nameof(InitialDimensions));
            }
        }

        public ProgramManager()
        {
            InitialDimensions = new Dictionary<Grid, Tuple<int, int>>();
            AutoScaler = new AutoScaler();
            CurrentTheme = new Theme() { BackgroundColor = "#424B54", ForegroundColor = "#FFEDDF", SelectableLinkColor = "#AFE0CE", SelectablePrimaryColor = "#C5D86D", SelectableSecondaryColor = "#EF6351" }; // Change in the future to load theme in a theme manager instead of program manager (for fonts etc also)
            

        }
    }
}
