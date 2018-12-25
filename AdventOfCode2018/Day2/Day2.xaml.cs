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
        private string[] inputLines;
        public Day2()
        {
            InitializeComponent();
            LoadInput();
        }

        private void LoadInput()
        {
            inputLines = System.IO.File.ReadAllLines("Input/Day2.txt");
            string text = System.IO.File.ReadAllText("Input/Day2.txt");
            InputBox.Text = text;
        }

        private int CalculateChecksum(ref string[] boxIds)
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

        private void Day2_Calculate(object sender, RoutedEventArgs e)
        {
            int checksum = CalculateChecksum(ref inputLines);
            ChecksumText.Text = checksum.ToString();
        }
    }
}
