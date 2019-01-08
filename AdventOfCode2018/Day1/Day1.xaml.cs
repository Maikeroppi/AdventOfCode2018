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
    /// Interaction logic for Day1View.xaml
    /// </summary>
    public partial class Day1 : UserControl
    {
        private DayViewModel _viewModel;

        public Day1()
        {
            InitializeComponent();

            _viewModel = Util.GetDayViewModel();
            Util.LoadTextBoxWithInput(_viewModel, InputBox);            
        }

        private int[] ConvertTextToFrequencies()
        {
            int totalLines = InputBox.LineCount;

            int[] returnArray = new int[totalLines];
            for (int lineNumber = 0; lineNumber < totalLines; ++lineNumber)
            {
                string line = InputBox.GetLineText(lineNumber);

                int number = 0;
                if (Int32.TryParse(line, out number))
                {
                    returnArray[lineNumber] = number;
                }
            }

            return returnArray;
        }

        private int CalculateFrequency(ref int[] frequencies)
        {
            int currentFrequency = 0;
            foreach(int frequency in frequencies)
            {
                currentFrequency += frequency;
            }

            return currentFrequency;
        }

        private int FindFirstDuplicate(ref int[] frequencies)
        {
            int frequencyCount = frequencies.Length;
            HashSet<int> seenFrequencies = new HashSet<int>();
            seenFrequencies.Add(0);

            bool foundDuplicate = false;
            int currentValue = 0;

            for(int i = 0; !foundDuplicate; ++i)
            {
                currentValue += frequencies[i % frequencyCount];

                if (seenFrequencies.Contains(currentValue))
                {
                    foundDuplicate = true;
                }
                else
                {
                    seenFrequencies.Add(currentValue);
                }
            }

            return currentValue;
        }

        private void Day1_Calculate(object sender, RoutedEventArgs e)
        {
            int[] frequencies = ConvertTextToFrequencies();
            int finalFrequency = CalculateFrequency(ref frequencies);

            OutFrequency.Text = finalFrequency.ToString();

            int duplicateFrequency = FindFirstDuplicate(ref frequencies);
            DoubleFrequency.Text = duplicateFrequency.ToString();
        }
    }
}
