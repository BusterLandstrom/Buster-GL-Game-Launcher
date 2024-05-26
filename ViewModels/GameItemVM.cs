using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using GameLauncher.Items;
using GameLauncher.VariableControllers;

namespace GameLauncher.ViewModels
{
    class GameItemVM : INotifyPropertyChanged
    {

        private event PropertyChangedEventHandler _propertyChanged;

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _propertyChanged += value; }
            remove { _propertyChanged -= value; }
        }
        // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Variable))); This is how you handle properties being changed



        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            _propertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _name;
        public string Name 
        { 
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _exe; // Path to exe launcher
        public string Exe
        {
            get { return _exe; }
            set
            {
                _exe = value;
                OnPropertyChanged(nameof(Exe));
            }
        }

        private Image _picture;
        public Image Picture
        { 
            get { return _picture; } 
            set 
            {
                _picture = value;
                OnPropertyChanged(nameof(Picture));
            }
        }

        private string _platform; // E.g. Steam, Origin, Local, etc.
        public string Platform 
        {
            get { return _platform; }
            set 
            {
                _platform = value;
                OnPropertyChanged(nameof(Platform));
            }
        }



        private ICommand _runGame;
        public ICommand RunGameCommand
        {
            get { return _runGame; }
            set
            {
                _runGame = value;
                OnPropertyChanged(nameof(RunGameCommand));
            }
        }

        private ICommand _deleteGame;
        public ICommand DeleteGameCommand
        {
            get { return _deleteGame; }
            set
            {
                _deleteGame = value;
                OnPropertyChanged(nameof(DeleteGameCommand));
            }
        }

        private ICommand _updateElement;
        public ICommand UpdateElementCommand
        {
            get { return _deleteGame; }
            set
            {
                _updateElement = value;
                OnPropertyChanged(nameof(UpdateElementCommand));
            }
        }

        public GameItemVM()
        {
            RunGameCommand = new RelayCommand(RunGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
            UpdateElementCommand = new RelayTypeCommand<Grid>(UpdateElement);
        }

        public void UpdateElement(Grid element) 
        {
            var app = Application.Current as App;
            if ( app != null)
            {
                ProgramManager pm = app.PM;
                int Width = pm.AutoScaler.CurrentResolution[0] * pm.AutoScaler.GetObjectSize((int)element.Width, (int)element.Height)[0]; // Fix to work more than once, make the width and height actually change correctly because now it is not working the way it should because it is only compared to the correct width, height on the first call of this function
                int Height = pm.AutoScaler.CurrentResolution[1] * pm.AutoScaler.GetObjectSize((int)element.Width, (int)element.Height)[1];
                element.Width = Width; element.Height = Height;
                Debug.WriteLine(element.Width + ", " + element.Height);
            }
        }

        public void RunGame()
        {
            Process.Start(Exe); // This needs admin to run (Try to prompt admin instead or run as admin upon launch)
        }

        public void DeleteGame()
        {
            Debug.WriteLine("Deleted game: " + Name);
        }
    }
}
