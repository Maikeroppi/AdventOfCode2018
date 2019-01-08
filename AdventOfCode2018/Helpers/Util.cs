using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace AdventOfCode2018
{
    public static class Util
    {
        public static DayViewModel GetDayViewModel()
        {
            AdventViewModel adventModel = App.Current.MainWindow.DataContext as AdventViewModel;
            return adventModel.CurrentDayViewModel;
        }

        public static void LoadTextBoxWithInput(DayViewModel viewModel, TextBox inputBox)
        {
            inputBox.Clear();
            int numberOfLines = viewModel.InputLines.Length;
            for (int lineIndex = 0; lineIndex < numberOfLines; ++lineIndex)
            {
                string line = viewModel.InputLines[lineIndex];

                if (lineIndex < numberOfLines - 1)
                {
                    line += Environment.NewLine;
                }

                inputBox.AppendText(line);
            }
        }
    }
}
