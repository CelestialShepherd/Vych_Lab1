using System;
using System.Collections.Generic;
using System.Windows;

namespace Vych_Lab_1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public void IsEightNum(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] >= '8')
                {
                    throw new Exception("Ввод сделан не в 8-ричной системе счисления");
                }
            }
        }
        public void IsSixteenNum(string str)
        {
            str = str.ToUpper();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] < '0' || str[i] > '9')
                {
                    if (str[i] != 'A' && str[i] != 'B' && str[i] != 'C' && str[i] != 'D' && str[i] != 'E' && str[i] != 'F')
                    {
                        throw new Exception("Ввод сделан не в 16-ричной системе счисления");
                    }
                }
            }
        }
        public string ConvertWithDictionary(string num, Dictionary<string,string> dict, int size)
        {
            if(num.Length != size)
            {
                do
                {
                    num = num.Insert(0,"0");
                }
                while (num.Length == size);
            }
            return dict[num];
        }
        public string ConvertFrom8To16(string str)
        {
            string[] From8To2 = new string[8] { "000", "001", "010", "011", "100", "101", "110", "111" };
            Dictionary<string, string> From2to16 = new Dictionary<string, string>
            {
                {"0000", "0"},
                {"0001", "1"},
                {"0010", "2"},
                {"0011", "3"},
                {"0100", "4"},
                {"0101", "5"},
                {"0110", "6"},
                {"0111", "7"},
                {"1000", "8"},
                {"1001", "9"},
                {"1010", "A"},
                {"1011", "B"},
                {"1100", "C"},
                {"1101", "D"},
                {"1110", "E"},
                {"1111", "F"},
            };
            string ConvertedTo2Rev = "", ConvertedTo2 = "", ConvertedNum = "", ConvertedNumRev = "";
            int num = Convert.ToInt32(str);
            //Переводим в 2ую
            for (int i = 0; i < str.Length; i++)
            { 
                ConvertedTo2Rev += From8To2[num%10];
                num /= 10;
            }
            //TODO: Сделать нормальный переворот числа
            for (int i = 0; i < ConvertedTo2Rev.Length; i++)
            {
                ConvertedTo2 += ConvertedTo2Rev[ConvertedTo2Rev.Length - 1 - i];
            }
            //Переводим в 16ую
            int PosCount = ConvertedTo2.Length - 5;
            //TODO: Учесть последнее число
            do
            {
                ConvertedNumRev += ConvertWithDictionary(ConvertedTo2.Substring(PosCount, PosCount + 3), From2to16, 4);
                PosCount -= 4;
            }
            while (PosCount > 0);
            for (int i = 0; i < ConvertedNumRev.Length; i++)
            {
                ConvertedNum += ConvertedNumRev[ConvertedNumRev.Length - 1 - i];
            }
            return ConvertedNum;
        }

        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (From8To16RB.IsChecked == true)
                {
                    IsEightNum(InputTB.Text);
                    OutputTB.Text = ConvertFrom8To16(InputTB.Text);
                }
                else if (From16To8RB.IsChecked == true)
                {
                    IsSixteenNum(InputTB.Text);
                }
                else
                {
                    throw new Exception("Не был выбран вид операции");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Тип данных не совпадает с предполагаемыми.", "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Текст ошибки:\r\n" + ex.Message, "Ошибка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
