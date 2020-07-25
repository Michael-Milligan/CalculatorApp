using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
        public static string Path = "Resources/defaultLanguage.txt";

        private double? Answer;
        private double Typed;
        private string Sign;

        bool IsError;
        double Memory;
        int ZeroCount = 1;
        bool IsAfterComma;

        public double? AnswerProperty { 
            get
            {
                if (Answer.HasValue)
                    return Answer.Value;
                else return null;
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
            catch (OverflowException)
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
        public void Comma_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Window = Application.Current.Windows[0] as MainWindow;
            IsAfterComma = true;
            Window.Field.Content = Convert.ToString(Window.Field.Content) + ",";

        }

        public void C_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Window.Field.Content = 0;
            Window.Answer.Content = null;
            Window.Sign.Content = null;
            IsAfterComma = false;
            ZeroCount = 1;
        }
        public void CE_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            IsAfterComma = false;
            ZeroCount = 1;
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

            if (!IsAfterComma)
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
                catch (OverflowException)
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

        public void ActionOneParameter_Click(object sender, RoutedEventArgs e)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            Button button = sender as Button;
            Window.Sign.Content = button.Tag;
            Window.Answer.Content = GetResultOneParameter(Convert.ToDouble(Window.Field.Content));
            Window.Sign.Content = null;
            Window.Field.Content = 0;
            
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
                        return Math.Pow(SecondOperand, 1 / FirstOperand);
                    }
                    catch (OverflowException)
                    { GetError(); }
                    break;
                case "log b a":
                    try
                    {
                        return Math.Log(FirstOperand, SecondOperand);
                    }
                    catch (OverflowException)
                    { GetError(); }
                    break;
            }
            return 0;
        }

        public double GetResultOneParameter(double Parameter)
        {
            var Window = Application.Current.Windows[0] as MainWindow;
            switch (Convert.ToString(Window.Sign.Content))
            {
                case "!":
                    return Factorial(Parameter);
                case "√":
                    return Math.Sqrt(Parameter);
                case "3√":
                    return Math.Pow(Parameter, 1 / 3);

                case "lg":
                    return Math.Log10(Parameter);
                case "ln":
                    return Math.Log(Parameter);
                default:
                    return 0;
            }
        }

        public double Factorial(double Number)
        {
            List<double> list = new List<double>();
            for (int i = 1; i <= Number; i++)
            {
                list.Add(i);
            }
            return list.Aggregate((FirstParameter, SecondParameter) =>
            {
                return FirstParameter * SecondParameter;
            });
        }

        public void ChangeLanguageClick(object sender, EventArgs e)
        {
            if (sender is MenuItem CurrentMenuItem)
            {
                string langName = (string)CurrentMenuItem.Tag;
                CultureInfo lang = App.Languages.Where(item => item.Name == langName).ToList()[0];
                if (lang != null)
                {
                    App.Language = lang;
                }
                App_LanguageChanged();
            }
        }

        public void App_LanguageChanged()
        {
            File.WriteAllText(Path, App.Language.Name);
        }
        #endregion
    }
}
