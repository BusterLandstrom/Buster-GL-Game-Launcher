using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace GameLauncher.ViewModels
{
    class GameItemVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Variable))); This is how you handle properties being changed

        private string _name;
        public string Name 
        { 
            get { return _name; }
            set 
            {
                _name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }

        private string _exe;
        public string Exe
        {
            get { return _exe; }
            set
            {
                _exe = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Exe)));
            }
        }

        private Image _picture;
        public Image Picture
        { 
            get { return _picture; } 
            set 
            {
                _picture = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Picture)));
            }
        }



        private void RunGame(object sender, RoutedEventArgs e)
        {
            Process.Start(Exe);
        }

        private void DeleteGame(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Deleted game");
        }
    }
}
