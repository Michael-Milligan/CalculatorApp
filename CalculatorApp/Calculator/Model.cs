using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Prism.Mvvm;

namespace Calculator
{
    class Model : BindableBase
    {
        private double? Answer;
        private double Typed;
        private string Sign;

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


        #endregion
    }
}
