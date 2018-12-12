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

            HashSet<int> seenFrequencies = new HashSet<int>();
            seenFrequencies.Add(0);

            bool lookingForDuplicate = true;

            char[] trimChars = { '+', '-' };

            bool keepGoing = true;
            bool completedSum = false;
            for (int lineNumber = 0; keepGoing;)
            {
                string line = InputBox.GetLineText(lineNumber % lineCount);
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
                    
                    if (lookingForDuplicate)
                    {
                        if (seenFrequencies.Contains(currentFrequency))
                        {
                            DoubleFrequency.Text = currentFrequency.ToString();
                            lookingForDuplicate = false;
                        }
                        else
                        {
                            seenFrequencies.Add(currentFrequency);
                        }
                    }
               }

                lineNumber++;
                if (lineNumber >= lineCount)
                {
                    if (!completedSum)
                    {
                        OutFrequency.Text = currentFrequency.ToString();
                        completedSum = true;
                    }
                    lineNumber = 0;
                }

                keepGoing = !completedSum || lookingForDuplicate;
            }
        }
    }
}
