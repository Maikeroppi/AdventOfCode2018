using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AdventOfCode2018
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Day1_Calculate(object sender, RoutedEventArgs e)
        {
            int currentFrequency = 0;
            int lineCount = InputBox.LineCount;

            char[] trimChars = { '+', '-' };
            for (int lineNumber = 0; lineNumber < lineCount; lineNumber++)
            {
                string line = InputBox.GetLineText(lineNumber);
                string numberText = line.TrimStart(trimChars);
                int number = 0;
                if (Int32.TryParse(numberText, out number))
                {
                    if (line[0] == '+')
                    {
                        currentFrequency += number;
                    }
                    else
                    {
                        currentFrequency -= number;
                    }                        
                }
            }

            OutFrequency.Text = currentFrequency.ToString();
        }  
    }
}
