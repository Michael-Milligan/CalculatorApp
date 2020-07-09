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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        bool IsError;
        double Memory;
        int ZeroCount = 1;
        bool IsAfterDot;


        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonDigit_Click(object sender, RoutedEventArgs e)
        {
            //Retrieving button from sender
            Button button = sender as Button;

            if (Answer.Content != null && Sign.Content == null)
            {
                Answer.Content = null;
                return;
            }

            //If the Field is overflown this will handle the infinity
            if (Convert.ToString(Field.Content) == "∞")
            {
                ErrorHandling();
                return;
            }

            if (!IsAfterDot)
            {
                Field.Content = Convert.ToDouble(Field.Content) * 10 +
                        (button.Name[6] - '0');
            }
            else
            {
                try
                {
                    //Rounding the double result to avoid "zero-trouble" (1*)
                    Field.Content = Math.Round(Convert.ToDouble(Field.Content) +
                            (button.Name[6] - '0') * Math.Pow(0.1, ZeroCount++), ZeroCount);
                }
                catch (Exception)
                {
                    GetError();
                }
            }
        }

        void GetError()
        {
            Field.Content = "Error";
            IsError = true;
        }

        void ErrorHandling()
        {
            Field.Content = 0;
            IsError = false;
        }

        private void Action_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            if (Answer.Content == null)
            {
                Sign.Content = button.Tag;
                Answer.Content = Field.Content;
                Field.Content = 0;
            }
            else if (Answer.Content != null && Convert.ToDouble(Field.Content) != 0)
            {
                Answer.Content = GetResult(Convert.ToDouble(Answer.Content), Convert.ToDouble(Field.Content));
                Field.Content = 0;
                Sign.Content = button.Tag;
            }
            else
            {
                Sign.Content = button.Tag;
            }
        }

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if (Answer.Content != null && Sign.Content == null)
            {
                Answer.Content = null;
                return;
            }

            if (IsError) 
            { 
                ErrorHandling();
                return;
            }
            //Transfer Content to the char array
            char[] temp = (Field.Content as string).ToCharArray();
            //Copying all except for one to result
            char[] Result = new char[temp.Length - 1];
            for (int i = 0; i < Result.Length; ++i)
            {
                Result[i] = temp[i];
            }
            Field.Content = Convert.ToString(Result);
        }

        #region Memory
        private void MPlus_Click(object sender, RoutedEventArgs e)
        {
            Memory += Convert.ToDouble(Field.Content);
        }

        private void MMinus_Click(object sender, RoutedEventArgs e)
        {
            Memory -= Convert.ToDouble(Field.Content);
        }

        private void MReset_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Memory;
        }
        #endregion

        private void OppositeSign_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Field.Content = Convert.ToDouble(Field.Content) * (-1);
            }
            catch (Exception)
            {
                ErrorHandling();
            }
        }

        private void Dot_Click(object sender, RoutedEventArgs e)
        {
            IsAfterDot = true;
        }

        private void C_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = 0;
            Answer.Content = null;
            Sign.Content = null;
        }

        private void CE_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = 0;
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            Answer.Content = GetResult(Convert.ToDouble(Answer.Content),
                Convert.ToDouble(Field.Content));
            Field.Content = 0;
            Sign.Content = null;
        }

        double GetResult(double FirstOperand, double SecondOperand)
        {
            switch (Convert.ToString(Sign.Content)[0])
            {
                case '+':
                    return FirstOperand + SecondOperand;
                case '-': 
                    return FirstOperand - SecondOperand;
                case '*': 
                    return FirstOperand * SecondOperand;
                case '/': 
                    return FirstOperand / SecondOperand;
            }
            return 0;
        }

        private void Standard_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Content = new MainWindow().Content;
        }
    }
}
