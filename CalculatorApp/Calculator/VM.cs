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

        public DelegateCommand C_Click { get; }
        public DelegateCommand CE_Click { get; }

        public DelegateCommand OppositeSign_Click { get; }
        public DelegateCommand Backspace_Click { get; }
        public DelegateCommand Dot_Click { get; }

        public DelegateCommand<object> Digit_Click { get; }

        public DelegateCommand<object> Action_Click { get; }

        public DelegateCommand MPlus_Click { get; }
        public DelegateCommand MMinus_Click { get; }
        public DelegateCommand MReset_Click { get; }

        public DelegateCommand Equals_Click { get; }

        public DelegateCommand<object> Language_Click { get; }



        public VM()
        {
            Model.PropertyChanged += (Sender, Args) =>
             { RaisePropertyChanged(Args.PropertyName); };

            Exit_Click = new DelegateCommand(() =>
            {
                Model.Exit_Click(1, new RoutedEventArgs());
            });

            C_Click = new DelegateCommand(() =>
                {
                    Model.C_Click(1, new RoutedEventArgs());
                });
            CE_Click = new DelegateCommand(() =>
                {
                    Model.C_Click(1, new RoutedEventArgs());
                });

            Backspace_Click = new DelegateCommand(() =>
            {
                Model.Backspace_Click(1, new RoutedEventArgs());
            });
            Dot_Click = new DelegateCommand(() =>
            {
                Model.Dot_Click(1, new RoutedEventArgs());
            });
            OppositeSign_Click = new DelegateCommand(() =>
            {
                Model.OppositeSign_Click(1, new RoutedEventArgs());
            });


            Digit_Click = new DelegateCommand<object>(sender =>
            {
                Model.Digit_Click(sender, new RoutedEventArgs());
            });

            Action_Click = new DelegateCommand<object>(sender =>
            {
                Model.Action_Click(sender, new RoutedEventArgs());
            });

            MPlus_Click = new DelegateCommand(() =>
            {
                Model.MPlus_Click(1, new RoutedEventArgs());
            }); 
            MMinus_Click = new DelegateCommand(() =>
            {
                Model.MMinus_Click(1, new RoutedEventArgs());
            }); 
            MReset_Click = new DelegateCommand(() =>
            {
                Model.MReset_Click(1, new RoutedEventArgs());
            });

            Equals_Click = new DelegateCommand(() =>
            {
                Model.Equals_Click(1, new RoutedEventArgs());
            });

            Language_Click = new DelegateCommand<object>((sender) =>
            {
                Model.ChangeLanguageClick(sender, new RoutedEventArgs());
            });
        }
    }
}
