using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FunctionBuilder.Logic
{
    public static class Rpn
    {
        public static string StartRpn(string str)
        {
            return ParseExpression(ParseText(str));
        }

        private static int SearchPriority(char x)
        {
            switch (x)
            {
                case '(': return 0;
                case ')': return 1;
                case '+': return 2;
                case '-': return 2;
                case '*': return 4;
                case '/': return 4;
                case '^': return 5;
                default: return 10;
            }
        }

        private static int StackPriority(Stack<char> stack)
        {
            if (stack.Count == 0)
                return 1;
            else
                return SearchPriority(stack.Peek());
        }

        private static string ParseText(string line)
        {
            line = line.Replace(" ", "");
            line = line.Replace("y=", "");
            return line;
        }

        public static string ParseExpression(string line)
        {
            string str = "";
            Stack<char> st = new Stack<char>();

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == 'x')
                    str += "x ";
                if (IsSign(line[i]))
                {
                    if (SearchPriority(line[i]) == 0)
                    {
                        st.Push(line[i]);
                        continue;
                    }
                    if (SearchPriority(line[i]) == 1)
                    {
                        while (st.Peek() != '(')
                            str += st.Pop();
                        st.Pop();
                        continue;
                    }
                    if (SearchPriority(line[i]) <= StackPriority(st) && st.Count > 0)
                        str += st.Pop();

                    if (SearchPriority(line[i]) > StackPriority(st))
                        st.Push(line[i]);
                }
                else if (char.IsDigit(line[i]))
                {
                    string s = "";
                    while (char.IsDigit(line[i]))
                    {
                        s += line[i];
                        i++;
                        if (line.Length <= i)
                            break;
                    }
                    i--;
                    str += s + " ";
                }
                else
                    continue;
            }
            while (st.Count != 0)
                str += st.Pop();
            return str;
        }


        public static bool IsSign(char x)
        {
            char[] signs = { '+', '-', '*', '^', '/', '(', ')' };
            if (signs.Contains(x))
                return true;
            else
                return false;
        }
    }
}
