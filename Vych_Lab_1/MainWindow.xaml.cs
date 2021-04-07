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
            string ConvertedNum = "", ConvertedTo2 = "";
            List<string> ConvertedTo16 = new List<string>();
            int num = Convert.ToInt32(str);
            //Переводим в 2ую
            for (int i = 0; i < str.Length; i++)
            {
                ConvertedTo2 += From8To2[(num / Convert.ToInt32(Math.Pow(10, (str.Length - 1 - i))))%10];
            }
            //Добавляем необходимое для кратности длины четырём количество нулей в начало полученного числа в двоичной системе
            do
            {
                ConvertedTo2 = "0" + ConvertedTo2;
            }
            while (ConvertedTo2.Length % 4 != 0);
            //Переводим в 16ую
            for (int i = 0; i < ConvertedTo2.Length; i += 4)
            {
                ConvertedTo16.Add(ConvertedTo2.Substring(i,4));
            }
            foreach (string c in ConvertedTo16)
            {
                From2to16.TryGetValue(c, out string StrOneNum);
                ConvertedNum += StrOneNum;
            }
            while (ConvertedNum[0] == '0')
            {
                ConvertedNum = ConvertedNum.Substring(1, ConvertedNum.Length - 1);
            }
            return ConvertedNum;
        }
        public string ConvertFrom16To8(string str)
        {
            Dictionary<string, string> From16to2 = new Dictionary<string, string>
            {
                {"0","0000"},
                {"1","0001"},
                {"2","0010"},
                {"3","0011"},
                {"4","0100"},
                {"5","0101"},
                {"6","0110"},
                {"7","0111"},
                {"8","1000"},
                {"9","1001"},
                {"A","1010"},
                {"B","1011"},
                {"C","1100"},
                {"D","1101"},
                {"E","1110"},
                {"F","1111"},
            };
            Dictionary<string, string> From2to8 = new Dictionary<string, string>
            {
                {"000", "0"},
                {"001", "1"},
                {"010", "2"},
                {"011", "3"},
                {"100", "4"},
                {"101", "5"},
                {"110", "6"},
                {"111", "7"},
            };
            string ConvertedNum = "", ConvertedTo2 = ""; 
            List<string> ConvertedTo8 = new List<string>();
            //Переводим в 2ую
            for (int i = 0; i < str.Length; i++)
            {
                From16to2.TryGetValue(str.Substring(i,1), out string StrOneNum);
                ConvertedTo2 += StrOneNum;
            }
            //Добавляем необходимое для кратности длины трём количество нулей в начало полученного числа в двоичной системе
            do
            {
                ConvertedTo2 = "0" + ConvertedTo2;
            }
            while (ConvertedTo2.Length % 3 != 0);
            //Переводим в 8ую
            for (int i = 0; i < ConvertedTo2.Length; i += 3)
            {
                ConvertedTo8.Add(ConvertedTo2.Substring(i, 3));
            }
            foreach (string c in ConvertedTo8)
            {
                From2to8.TryGetValue(c, out string StrOneNum);
                ConvertedNum += StrOneNum;
            }
            while (ConvertedNum[0] == '0')
            {
                ConvertedNum = ConvertedNum.Substring(1, ConvertedNum.Length - 1);
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
                    OutputTB.Text = ConvertFrom16To8(InputTB.Text);
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
