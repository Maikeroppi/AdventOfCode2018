using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class Day2 : UserControl
    {
        DayViewModel _viewModel;

        public Day2()
        {
            InitializeComponent();
            _viewModel = Util.GetDayViewModel();
            Util.LoadTextBoxWithInput(_viewModel, InputBox);
        }

        private int CalculateChecksum(string[] boxIds)
        {
            int doubleCount = 0;
            int tripleCount = 0;

            HashSet<char> seenLetters = new HashSet<char>();
            HashSet<char> doubleLetters = new HashSet<char>();
            HashSet<char> tripleLetters = new HashSet<char>();

            foreach (string id in boxIds)
            {
                foreach(char ch in id)
                {
                    if (seenLetters.Contains(ch))
                    {
                        seenLetters.Remove(ch);
                        doubleLetters.Add(ch);
                    }
                    else if (doubleLetters.Contains(ch))
                    {
                        doubleLetters.Remove(ch);
                        tripleLetters.Add(ch);
                    }
                    else
                    {
                        seenLetters.Add(ch);
                    }
                }

                if (doubleLetters.Count > 0)
                {
                    doubleCount += 1;
                }

                if (tripleLetters.Count > 0)
                {
                    tripleCount += 1;
                }

                seenLetters.Clear();
                doubleLetters.Clear();
                tripleLetters.Clear();
            }

            return doubleCount * tripleCount;
        }

        // Adapted from Wikipedia: https://en.wikipedia.org/wiki/Levenshtein_distance#Recursive
        static int CalculateLevenshteinDistance(string first, string second)
        {
            int cost;

            /* base case: empty strings */
            if (first.Length == 0) return second.Length;
            if (second.Length == 0) return first.Length;

            /* test if last characters of the strings match */
            if (first[first.Length - 1] == second[second.Length - 1])
            {
                cost = 0;
            }
            else
            {
                cost = 1;
            }

            string firstSub = first.Substring(0, first.Length - 1);
            string secondSub = second.Substring(0, second.Length - 1);
            /* return minimum of delete char from s, delete char from t, and delete char from both */
            return Math.Min(Math.Min(CalculateLevenshteinDistance(firstSub, second) + 1,
                           CalculateLevenshteinDistance(first, secondSub) + 1),
                           CalculateLevenshteinDistance(firstSub, secondSub) + cost);
        }

        private static bool StringsAreSimilar(ref string first, ref string second)
        {
            bool differsByOne = false;

            // Strings are the same length in this problem
            for (int i = 0; i < first.Length; ++i)
            {
                if (first[i] != second[i])
                {
                    if (differsByOne)
                    {
                        //  second difference; not similar
                        return false;
                    }
                    else
                    {
                        differsByOne = true;
                    }
                }
            }

            return differsByOne;
        }

        private void FindSimilarStrings(string[] inputLines)
        {
            OutputTextBox.Text = "";

            for(int i = 0; i < inputLines.Length; ++i)
            {
                for (int j = 0; j < inputLines.Length; ++j)
                {
                    if (i == j) continue;

                    if (StringsAreSimilar(ref inputLines[i], ref inputLines[j]))
                    {
                        OutputTextBox.AppendText(inputLines[i] + Environment.NewLine);
                    }
                }
            }
        }

        private void Day2_Calculate(object sender, RoutedEventArgs e)
        {
            int checksum = CalculateChecksum(_viewModel.InputLines);
            ChecksumText.Text = checksum.ToString();

            FindSimilarStrings(_viewModel.InputLines);
        }
    }
}
