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



        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 1;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 2;
        }

        private void Button3_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 3;
        }

        private void Button4_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 4;
        }

        private void Button5_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 5;
        }

        private void Button6_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 6;
        }

        private void Button7_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 7;
        }

        private void Button8_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 8;
        }

        private void Button9_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10 + 9;
        }

        private void Button0_Click(object sender, RoutedEventArgs e)
        {
            Field.Content = Convert.ToInt64(Field.Content) * 10;
        }

        private void ButtonPlus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMinus_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonMulti_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonSub_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
