using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using GameLauncher.Items;
using GameLauncher.VariableControllers;
using Image = System.Windows.Media.ImageSource;
using System.Runtime.InteropServices;

namespace GameLauncher.ViewModels
{
    public class GameItemVM : INotifyPropertyChanged
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

        // Start of properties

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

        private Image _gameIcon;
        public Image GameIcon
        {
            get { return _gameIcon; }
            set
            {
                _gameIcon = value;
                OnPropertyChanged(nameof(GameIcon));
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

        public GameItemVM(string name, string exe)
        {
            Name = name;
            Exe = exe;
            Icon loadedIcon = Icon.ExtractAssociatedIcon(Exe);
            GameIcon = ToImageSource(loadedIcon);
            RunGameCommand = new RelayCommand(RunGame);
            DeleteGameCommand = new RelayCommand(DeleteGame);
        }



        [DllImport("gdi32.dll", SetLastError = true)]
        private static extern bool DeleteObject(IntPtr hObject);
        public static ImageSource ToImageSource(Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            if (!DeleteObject(hBitmap))
            {
                throw new Win32Exception();
            }

            return wpfBitmap;
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
