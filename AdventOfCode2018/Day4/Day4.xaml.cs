using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for Day4.xaml
    /// </summary>
    public partial class Day4 : UserControl
    {
        public Day4()
        {
            InitializeComponent();
            LoadInput();
        }

        private string[] inputLines;
        private void LoadInput()
        {
            inputLines = System.IO.File.ReadAllLines("Input/Day4.txt");
            string text = System.IO.File.ReadAllText("Input/Day4.txt");
            InputBox.Text = text;
        }

        enum GuardAction
        {
            StartShift,
            FallsAsleep,
            WakesUp
        };

        struct TimedAction
        {
            public DateTime time;
            public int guardNumber;
            public GuardAction action;
        }        

        class GuardRecord
        {
            public int totalMinutesAsleep = 0;
            public List<int> minutesAsleep = new List<int>();
        }

        TimedAction[] ParseGuardActions(string[] input)
        {
            TimedAction[] actions = new TimedAction[input.Length];

            char[] splitChars = { '[', ']' };
            int activeGuardNumber = 0;
            for (int i = 0; i < input.Length; ++i)
            {
                activeGuardNumber = 0;
                string[] parts = input[i].Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                DateTime time;
                DateTime.TryParseExact(parts[0], "yyyy-MM-dd HH:mm",
                    System.Globalization.CultureInfo.InvariantCulture, DateTimeStyles.None, out time);
                GuardAction action = GuardAction.StartShift;

                if (parts[1].Contains("asleep"))
                {
                    action = GuardAction.FallsAsleep;
                }
                else if(parts[1].Contains("wakes"))
                {
                    action = GuardAction.WakesUp;
                }
                else if (parts[1].Contains("Guard"))
                {
                    int location = parts[1].IndexOf("#") + 1;
                    int length = parts[1].IndexOf(' ', location);
                    string intString = parts[1].Substring(location, length - location);
                    Int32.TryParse(intString, out activeGuardNumber);
                }

                actions[i].action = action;
                actions[i].time = time;
                actions[i].guardNumber = activeGuardNumber;
            }

            Array.Sort<TimedAction>(actions, (lhs, rhs) => lhs.time.CompareTo(rhs.time));
            for (int i = 0; i < actions.Length; ++i)
            {
                if (actions[i].action == GuardAction.StartShift)
                {
                    activeGuardNumber = actions[i].guardNumber;
                }
                else
                {
                    actions[i].guardNumber = activeGuardNumber;
                }
            }

            return actions;
        }

        private void UpdateGuardRecord(ref Dictionary<int, GuardRecord> records, int guardNum, int startTime, int endTime)
        {
            GuardRecord record = new GuardRecord();

            if (records.ContainsKey(guardNum))
            {
                record = records[guardNum];
            }
            record.totalMinutesAsleep += endTime - startTime;

            for (int minute = startTime; minute < endTime; ++minute)
            {
                record.minutesAsleep.Add(minute);
            }

            records[guardNum] = record;
        }

        private void BuildGuardRecords(ref Dictionary<int, GuardRecord> records, ref TimedAction[] actions)
        {
            TimedAction lastAction = new TimedAction();
            int activeGuard = 0;

            foreach (TimedAction action in actions)
            {
                switch (action.action)
                {
                    case GuardAction.FallsAsleep: break;
                    case GuardAction.WakesUp:
                        UpdateGuardRecord(ref records, activeGuard, lastAction.time.Minute, action.time.Minute);
                    break;
                    case GuardAction.StartShift: activeGuard = action.guardNumber; break;
                }

                lastAction = action;
            }
        }

        int GetAnswer(ref Dictionary<int, GuardRecord> records)
        {
            var item = records.Aggregate((lhs, rhs) => 
                lhs.Value.totalMinutesAsleep > rhs.Value.totalMinutesAsleep ? lhs : rhs);

            int max = item.Value.totalMinutesAsleep;
            int guardNum = item.Key;
            List<int> minutesAsleep = item.Value.minutesAsleep;
            HashSet<int> seenValues = new HashSet<int>();

            var repeatedMinutes = minutesAsleep.GroupBy(x => x)
              .Where(g => g.Count() > 1)
              .Select(y => new { Element = y.Key, Counter = y.Count() })
              .ToList();

            var maxRepeat = repeatedMinutes.Aggregate((lhs, rhs) => lhs.Counter > rhs.Counter ? lhs : rhs);
            int duplicate = maxRepeat.Element;

            return guardNum * duplicate;
        }

        private void CalculateButton_Click(object sender, RoutedEventArgs e)
        {
            TimedAction[] actions = ParseGuardActions(inputLines);

            var records = new Dictionary<int, GuardRecord>();
            BuildGuardRecords(ref records, ref actions);

            AnswerText.Text = GetAnswer(ref records).ToString();
        }
    }
}
