using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdventOfCode2018
{
    public class DayViewModel : INotifyPropertyChanged
    {
        public DayViewModel(string title)
        {
            this.Title = title;
        }

        private string _title;
        public virtual string Title
        {
            get
            {
                return _title;
            }

            set
            {
                _title = value;
            }
        }

        // Notify property changed stuff
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                var e = new PropertyChangedEventArgs(propertyName);
                handler(this, e);
            }
        }
    }
}
