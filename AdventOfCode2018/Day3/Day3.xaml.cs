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
    /// Interaction logic for Day3.xaml
    /// </summary>
    public partial class Day3 : UserControl
    {
        public Day3()
        {
            InitializeComponent();
            LoadInput();
        }

        private string[] inputLines;
        private void LoadInput()
        {
            inputLines = System.IO.File.ReadAllLines("Input/Day3.txt");
            string text = System.IO.File.ReadAllText("Input/Day3.txt");
            InputBox.Text = text;
        }

        public struct ClothOrder
        {
            public int orderNumber, x, y, width, height;
        }

        ClothOrder[] ParseOrders(string[] input)
        {
            ClothOrder[] orders = new ClothOrder[input.Length];

            char[] splitChars = { '#', '@', ':', ',', 'x' };
            for (int i = 0; i < input.Length; ++i)
            {
                ClothOrder current = new ClothOrder();
                string line = input[i];
                string[] elements = line.Split(splitChars, StringSplitOptions.RemoveEmptyEntries);
                Int32.TryParse(elements[0], out current.orderNumber);
                Int32.TryParse(elements[1], out current.x);
                Int32.TryParse(elements[2], out current.y);
                Int32.TryParse(elements[3], out current.width);
                Int32.TryParse(elements[4], out current.height);

                orders[i] = current;
            }

            return orders;
        }

        private void ApplyOrder(ClothOrder order, ref int[,] cloth)
        {
            for (int i = 0; i < order.width; ++i)
            {
                for (int j = 0; j < order.height; j++)
                {
                    cloth[order.x + i, order.y + j] += 1;
                }
            }
        }

        private void ApplyOrders(ref ClothOrder[] orders, ref int[,] clothPiece)
        {
            foreach (ClothOrder order in orders)
            {
                ApplyOrder(order, ref clothPiece);
            }
        }
        
        private int CountOverlaps(ref int[,] clothClaims)
        {
            int overlaps = 0;
            int width = clothClaims.GetLength(0);
            int height = clothClaims.GetLength(1);

            for (int i = 0; i < width; ++i)
            {
                for (int j = 0; j < height; ++j)
                {
                    if (clothClaims[i, j] > 1)
                    {
                        overlaps += 1;
                    }
                }
            }

            return overlaps;
        }

        private bool OrderOverlaps(ClothOrder order, ref int[,] cloth)
        {
            for (int i = 0; i < order.width; ++i)
            {
                for (int j = 0; j < order.height; j++)
                {
                    if (cloth[order.x + i, order.y + j] > 1)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int GetNonOverlappingOrderNumber(ref ClothOrder[] orders, ref int[,] clothClaims)
        {
            foreach (ClothOrder order in orders)
            {
                if (!OrderOverlaps(order, ref clothClaims))
                {
                    return order.orderNumber;
                }
            }
            return -1;
        }

        private void Day3_Calculate(object sender, RoutedEventArgs e)
        {
            ClothOrder[] orders = ParseOrders(inputLines);

            int[,] clothPiece = new int[1000, 1000];
            ApplyOrders(ref orders, ref clothPiece);
            int overlaps = CountOverlaps(ref clothPiece);

            OverlapText.Text = overlaps.ToString();
            int clearOrderNumber = GetNonOverlappingOrderNumber(ref orders, ref clothPiece);
            ClearOrderText.Text = clearOrderNumber.ToString();
        }
    }
}
