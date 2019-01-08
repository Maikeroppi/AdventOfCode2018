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
    /// Interaction logic for Day5.xaml
    /// </summary>
    public partial class Day5 : UserControl
    {
        DayViewModel _viewModel;
        public Day5()
        {
            InitializeComponent();
            _viewModel = Util.GetDayViewModel();
            Util.LoadTextBoxWithInput(_viewModel, InputBox);
        }

        private int ReactPolymer(string text)
        {
            int index = 0;
            int reactions = 0;
            while (index < text.Length - 1)
            {
                char unit1 = text[index];
                char unit2 = text[index + 1];
                if (char.ToLower(unit1) == char.ToLower(unit2)
                    && unit1 != unit2)
                {
                    // Remove this for reactions
                    text = text.Remove(index, 2);

                    if (index > 0)
                    {
                        // Offset back one character, because it might create a new reaction
                        index -= 1;
                    }
                    reactions += 1;
                }
                else
                {
                    index += 1;
                }
            }

            return text.Length;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            string text = String.Copy(InputBox.Text.Trim());
            AnswerText.Text = ReactPolymer(text).ToString();

            // Second answer
            int minValue = text.Length;

            for (int i = 0; i < 26; ++i)
            {
                int ch = 'a' + i;
                int CH = 'A' + i;
                string testString = new string((from c in text
                                                where c != ch && c != CH
                                                select c).ToArray());

                minValue = Math.Min(ReactPolymer(testString), minValue);
            }

            MinPolymerText.Text = minValue.ToString();
        }
    }
}
