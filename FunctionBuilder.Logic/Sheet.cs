using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionBuilder.Logic
{
    public class Sheet
    {
        public static List<string> StartSheet(string str, double steps, double xMin, double xMax)
        {
            CaculateFunction(steps, xMin, xMax, str, out List<string> listX, out List<string> listY);
            return WriteSheet(listX, listY);
        }

        public static void OutputNumbers(double steps, double xMin,
                                        double xMax, string str, out List<string> listX, out List<string> listY)
        {
            CaculateFunction(steps, xMin, xMax, str, out listX, out listY);
        }

        private static string CreatSheet(List<string> y, List<string> x, int maxLength, int i)
        {
            string str = "";
            //создаем тело таблицы
            str += "│" + y[i];
            for (int k = 0; k <= maxLength - y[i].Length; k++)
                str += " ";

            str += "│" + x[i];
            for (int j = 0; j <= maxLength - x[i].Length; j++)
                str += " ";
            str += "│";
            return str;
        }

        private static int SearchMaxLength(List<string> y, List<string> x)
        {
            int maxLength = 0, len_list = x.Count;

            //поиск максимальной длины значения
            for (int i = 0; i < len_list; i++)
            {
                if (x[i].Length > maxLength)
                    maxLength = x[i].Length;
                if (y[i].Length > maxLength)
                    maxLength = y[i].Length;
            }
            return maxLength;
        }

        private static string CreateSheet(string start, string line, string center, string end, int maxLength)
        {
            //шапка таблицы
            string str = "";
            str += start;
            for (int i = 0; i <= maxLength * 2 + 1; i++)
            {
                str += line;
                if (i == maxLength) str += center;
            }
            str += end;
            return str;
        }

        //построчно записываем таблицу в список
        private static List<string> WriteSheet(List<string> list_x, List<string> list_y)
        {
            List<string> sheet = new List<string>();
            int maxLength = SearchMaxLength(list_y, list_x);
            sheet.Add(CreateSheet("╔", "═", "╤", "╗", maxLength));
            sheet.Add(CreateSheet("║y", " ", "│x", "║", maxLength - 1));
            sheet.Add(CreateSheet("╚", "═", "╪", "╝", maxLength));
            for (int i = 0; i < list_x.Count; i++)
                sheet.Add(CreatSheet(list_y, list_x, maxLength, i));
            sheet.Add(CreateSheet("└", "─", "┴", "┘", maxLength));
            return sheet;
        }



        private static double Calculate(string str)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                {
                    string num = "";
                    while (char.IsDigit(str[i]))
                    {
                        num += str[i];
                        i++;
                    }
                    result.Add(Convert.ToDouble(num));
                }
                else if (Rpn.IsSign(str[i]))
                {
                    double num1 = result[^1];
                    result.RemoveAt(result.Count - 1);
                    double num2 = result[result.Count - 1];
                    result.RemoveAt(result.Count - 1);

                    switch (str[i])
                    {
                        case '+': result.Add(num1 + num2); break;
                        case '-': result.Add(num2 - num1); break;
                        case '*': result.Add(num1 * num2); break;
                        case '/': result.Add(num2 / num1); break;
                        case '^': result.Add(Math.Pow(num2, num1)); break;
                    }
                }
            }
            return result[0];
        }

        private static void CaculateFunction(double steps, double xMin, double xMax, string str,
                                    out List<string> listX, out List<string> listY)
        {
            string str1;
            double xValue, yValue;
            listX = new List<string>();
            listY = new List<string>();
            for (xValue = xMin; xValue < xMax; xValue += steps)
            {
                str1 = str;
                str1 = str1.Replace("x", " " + xValue.ToString());
                yValue = Calculate(str1);

                string xstr = xValue.ToString();
                listX.Add(xstr);
                string ystr = yValue.ToString();
                listY.Add(ystr);
            }
        }
    }
}
