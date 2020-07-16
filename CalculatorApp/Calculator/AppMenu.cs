using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
    class AppMenu : Menu
    {
        public AppMenu()
        {
            MenuItem Actions = new MenuItem();

            MenuItem Exit = new MenuItem();
            Exit.Header = "Exit";
            Exit.Click += Exit_Click;
            Actions.Items.Add(Exit);

            MenuItem Mode = new MenuItem();

            MenuItem Standard = new MenuItem();
            Standard.Header = "Standard";
            Standard.Click += Standard_Click;
            Mode.Items.Add(Standard);

            MenuItem Scientific = new MenuItem();
            Scientific.Header = "Scientific";
            Scientific.Click += Scientific_Click;
            Mode.Items.Add(Scientific);

            MenuItem Language = new MenuItem();

            Items.Add(Exit);
            Items.Add(Mode);
            Items.Add(Language);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Standard_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Windows[0].Content = new MainWindow().Content;
        }

        private void Scientific_Click(object sender, RoutedEventArgs e)
        {
            Window Current = Application.Current.Windows[0];
            Current.Height = 450;
            Current.Width = 450;
            Application.Current.Windows[0].Content = new Scientific().Content;
        }
    }
}
