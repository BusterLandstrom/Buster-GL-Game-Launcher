using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Items
{
    internal class ProgramManager : INotifyPropertyChanged
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
    }
}
