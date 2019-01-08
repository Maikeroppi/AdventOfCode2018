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
        public DayViewModel(string title, string inputFile)
        {
            this.Title = title;
            this.InputFile = inputFile;
            LoadDayInput();
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

        private string _inputFile;
        public virtual string InputFile
        {
            get
            {
                return _inputFile;
            }
            set
            {
                _inputFile = value;
            }
        }

        private string[] inputLines;
        public virtual string[] InputLines
        {
            get { return inputLines; }
        }

        public void LoadDayInput()
        {
            string inputFile = InputFile;
            if (!string.IsNullOrEmpty(inputFile))
            {
                inputLines = System.IO.File.ReadAllLines(inputFile);
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
