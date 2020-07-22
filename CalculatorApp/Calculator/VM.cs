using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Calculator
{
    class VM : BindableBase
    {
        readonly Model Model = new Model();

        public double? Answer => Model.AnswerProperty;
        public double Typed => Model.TypedProperty;
        public string Sign => Model.SignProperty;

        public DelegateCommand Exit_Click { get; }

        public VM()
        {
            Model.PropertyChanged += (Sender, Args) =>
             { RaisePropertyChanged(Args.PropertyName); };

            Exit_Click = new DelegateCommand(() =>
            {
                Model.Exit_Click(1, new RoutedEventArgs());
            });
        }
    }
}
