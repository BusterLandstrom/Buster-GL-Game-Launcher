using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GameLauncher.Items
{
    public class AutoScaler : INotifyPropertyChanged
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


        private List<int> _baseResolution = [ 2560, 1440 ];

        private List<int> _currentResolution;
        public List<int> CurrentResolution 
        {
            get { return _currentResolution; }
            set 
            { 
                _currentResolution = value;
                OnPropertyChanged(nameof(CurrentResolution));
            }
        }

        public void ChangeResolution(int width, int height) 
        {
            CurrentResolution = [width, height];
        }

        public List<int> GetObjectSize(int startWidth, int startHeight) 
        {
            List<int> size = [ 0, 0 ];

            size[0] = startWidth / _baseResolution[0];
            size[1] = startHeight / _baseResolution[1];




            return size; // this returns the relative percentage size of the element this can then be used to automatically scale the size of an element (like this: CurrentResolution[0] * GetObjectSize()[0] = Correct element width for current resolution)
        }

    }
}
