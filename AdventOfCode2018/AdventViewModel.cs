using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace AdventOfCode2018
{
    public class AdventViewModel : INotifyPropertyChanged
    {
        // Most of this is copied and slightly modified from https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/
        private ICommand _changeDayCommand;
        private DayViewModel _currentDayViewModel;
        private List<DayViewModel> _dayViewModels;

        public AdventViewModel()
        {
            DayViewModels.Add(new Day1ViewModel());
            DayViewModels.Add(new Day2ViewModel());

            CurrentDayViewModel = DayViewModels.Last();
        }


        public ICommand ChangeDayCommand
        {
            get
            {
                if (_changeDayCommand == null)
                {
                    _changeDayCommand = new RelayCommand(
                        p => ChangeViewModel((DayViewModel)p),
                        p => p is DayViewModel);
                }

                return _changeDayCommand;
            }
        }

        public List<DayViewModel> DayViewModels
        {
            get
            {
                if (_dayViewModels == null)
                    _dayViewModels = new List<DayViewModel>();

                return _dayViewModels;
            }
        }

        public DayViewModel CurrentDayViewModel
        {
            get
            {
                return _currentDayViewModel;
            }
            set
            {
                if (_currentDayViewModel != value)
                {
                    _currentDayViewModel = value;
                    OnPropertyChanged("CurrentDayViewModel");
                }
            }
        }

        private void ChangeViewModel(DayViewModel viewModel)
        {
            if (!DayViewModels.Contains(viewModel))
                DayViewModels.Add(viewModel);

            CurrentDayViewModel = DayViewModels
                .FirstOrDefault(vm => vm == viewModel);
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
