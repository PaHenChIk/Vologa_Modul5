using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Vologa_Modul5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       private void b0_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "0";
        }

        private void b1_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "1";
        }

        private void b2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "2";
        }
        private void b3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "3";
        }
        private void b4_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "4";
        }

        private void b5_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "5";
        }

        private void b6_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "6";
        }

        private void b7_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "7";
        }

        private void b8_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "8";
        }

        private void b9_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "9";
        }

        private void ravno_Click(object sender, EventArgs e)
        {
            try
            {
                var tokens = Tokenize(richTextBox1.Text);
                var rpnExpression = ConvertToRPN(tokens);
                var result = EvaluateRPN(rpnExpression);
                richTextBox1.Text = result.ToString();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Ошибка: " + ex.Message;
            }
        }

        private List<string> Tokenize(string expression)
        {
            var tokens = new List<string>();
            var number = new StringBuilder();

            foreach (var ch in expression)
            {
                if (char.IsDigit(ch) || ch == ','|| ch == '.')
                {
                    number.Append(ch);
                }
                else
                {
                    if (number.Length > 0)
                    {
                        tokens.Add(number.ToString());
                        number.Clear();
                    }

                    if (ch == '+' || ch == '-' || ch == '*' || ch == '/' || ch == '(' || ch == ')')
                    {
                        tokens.Add(ch.ToString());
                    }
                }
            }

            if (number.Length > 0)
            {
                tokens.Add(number.ToString());
            }

            return tokens;
        }

        private List<string> ConvertToRPN(List<string> tokens)
        {
            var output = new List<string>();
            var operators = new Stack<string>();

            foreach (var token in tokens)
            {
                if (double.TryParse(token, out _))
                {
                    output.Add(token);
                }
                else
                {
                    if (token == "(")
                    {
                        operators.Push(token);
                    }
                    else if (token == ")")
                    {
                        while (operators.Count > 0 && operators.Peek() != "(")
                        {
                            output.Add(operators.Pop());
                        }
                        operators.Pop(); 
                    }
                    else
                    {
                        while (operators.Count > 0 && GetPriority(operators.Peek()) >= GetPriority(token))
                        {
                            output.Add(operators.Pop());
                        }
                        operators.Push(token);
                    }
                }
            }

            while (operators.Count > 0)
            {
                output.Add(operators.Pop());
            }

            return output;
        }


        private int GetPriority(string op)
        {
            switch (op)
            {
                case "+":
                case "-":
                    return 1;
                case "*":
                case "/":
                    return 2;
                default:
                    return 0;
            }
        }

        private double EvaluateRPN(List<string> rpnExpression)
        {
            var numbers = new Stack<double>();

            foreach (var token in rpnExpression)
            {
                if (double.TryParse(token, out var number))
                {
                    numbers.Push(number);
                }
                else
                {
                    var rightOperand = numbers.Pop();
                    var leftOperand = numbers.Pop();

                    switch (token)
                    {
                        case "+":
                            numbers.Push(leftOperand + rightOperand);
                            break;
                        case "-":
                            numbers.Push(leftOperand - rightOperand);
                            break;
                        case "*":
                            numbers.Push(leftOperand * rightOperand);
                            break;
                        case "/":
                            numbers.Push(leftOperand / rightOperand);
                            break;
                    }
                }
            }

            return numbers.Pop();
        }




        private void plus_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "+";
        }

        private void minus_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "-";
        }

        private void umnozh_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "*";
        }

        private void delenie_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += "/";
        }

        private void koren_Click(object sender, EventArgs e)
        {
            try
            {
                decimal number = Decimal.Parse(richTextBox1.Text);
                var result = Math.Sqrt((double)number);
                richTextBox1.Text = result.ToString();
            }
            catch (Exception ex)
            {
                richTextBox1.Text = "Ошибка: " + ex.Message;
            }

        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
