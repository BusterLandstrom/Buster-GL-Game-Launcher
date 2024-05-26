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

        public GameItemVM()
        {
            RunGameCommand = new RelayCommand(RunGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
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
