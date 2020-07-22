using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Prism.Mvvm;

namespace Calculator
{
    class Model : BindableBase
    {
        private double? Answer;
        private double Typed;
        private string Sign;

        bool IsError;
        double Memory;
        int ZeroCount = 1;
        bool IsAfterDot;

        public double? AnswerProperty { 
            get
            {
                return Answer.Value;
            }
            set
            {
                if (value.HasValue)
                    Answer = value;
                RaisePropertyChanged("Answer");
            }}
        public double TypedProperty
        {
            get
            {
                return Typed;
            }
            set
            {
                Typed = value;
                RaisePropertyChanged("Typed");
            }
        }
        public string SignProperty
        {
            get
            {
                return Sign;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                    Sign = value;
                RaisePropertyChanged("Sign");
            }
        }

        #region Functions
        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void OppositeSign_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            try
            {
                Window.Field.Content = Convert.ToDouble(Window.Field.Content) * (-1);
            }
            catch (Exception)
            {
                ErrorHandling();
            }
        }
        public void Backspace_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            if (Window.Answer.Content != null && Window.Sign.Content == null)
            {
                Window.Answer.Content = null;
                return;
            }

            if (IsError)
            {
                ErrorHandling();
                return;
            }
            //Transfer Content to the char array
            char[] temp = (Window.Field.Content as string).ToCharArray();
            //Copying all except for one to result
            char[] Result = new char[temp.Length - 1];
            for (int i = 0; i < Result.Length; ++i)
            {
                Result[i] = temp[i];
            }
            Window.Field.Content = Convert.ToString(Result);
        }
        public void Dot_Click(object sender, RoutedEventArgs e)
        {
            IsAfterDot = true;
        }

        public void C_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = 0;
            Window.Answer.Content = null;
            Window.Sign.Content = null;
        }
        public void CE_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = 0;
        }

        void GetError()
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = "Error";
            IsError = true;
        }
        void ErrorHandling()
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = 0;
            IsError = false;
        }


        public void Digit_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            //Retrieving button from sender
            Button button = sender as Button;

            if (Window.Answer.Content != null && Window.Sign.Content == null)
            {
                Window.Answer.Content = null;
                return;
            }

            //If the Field is overflown this will handle the infinity
            if (Convert.ToString(Window.Field.Content) == "∞")
            {
                ErrorHandling();
                return;
            }

            if (!IsAfterDot)
            {
                Window.Field.Content = Convert.ToDouble(Window.Field.Content) * 10 +
                        (button.Name[6] - '0');
            }
            else
            {
                try
                {
                    //Rounding the double result to avoid "zero-trouble" (1*)
                    Window.Field.Content = Math.Round(Convert.ToDouble(Window.Field.Content) +
                            (button.Name[6] - '0') * Math.Pow(0.1, ZeroCount++), ZeroCount);
                }
                catch (Exception)
                {
                    GetError();
                }
            }
        }

        public void Action_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Button button = sender as Button;
            if (Window.Answer.Content == null)
            {
                Window.Sign.Content = button.Tag;
                Window.Answer.Content = Window.Field.Content;
                Window.Field.Content = 0;
            }
            else if (Window.Answer.Content != null && Convert.ToDouble(Window.Field.Content) != 0)
            {
                Window.Answer.Content = GetResult(Convert.ToDouble(Window.Answer.Content), 
                    Convert.ToDouble(Window.Field.Content));
                Window.Field.Content = 0;
                Window.Sign.Content = button.Tag;
            }
            else
            {
                Window.Sign.Content = button.Tag;
            }
        }


        #region Memory
        public void MPlus_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Memory += Convert.ToDouble(Window.Field.Content);
        }

        public void MMinus_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Memory -= Convert.ToDouble(Window.Field.Content);
        }

        public void MReset_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = Memory;
        }
        #endregion



        public void Equals_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Answer.Content = GetResult(Convert.ToDouble(Window.Answer.Content),
                Convert.ToDouble(Window.Field.Content));
            Window.Field.Content = 0;
            Window.Sign.Content = null;
        }

        double GetResult(double FirstOperand, double SecondOperand)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            switch (Convert.ToString(Window.Sign.Content))
            {
                case "+":
                    return FirstOperand + SecondOperand;
                case "-":
                    return FirstOperand - SecondOperand;
                case "*":
                    return FirstOperand * SecondOperand;
                case "/":
                    return FirstOperand / SecondOperand;
                case "^":
                    return Math.Pow(FirstOperand, SecondOperand);
                case "y√x":
                    try
                    {
                        return Math.Pow(FirstOperand, 1 / SecondOperand);
                    }
                    catch (Exception)
                    { GetError(); }
                    break;
            }
            return 0;
        }
        #endregion
    }
}
