using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AdventOfCode2018
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            AdventDayView dayView = new AdventDayView();
            AdventViewModel context = new AdventViewModel();
            dayView.DataContext = context;
            dayView.Show();
        }
    }
}
