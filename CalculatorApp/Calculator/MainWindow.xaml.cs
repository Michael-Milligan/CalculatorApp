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

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void ButtonDigit_Click(object sender, RoutedEventArgs e)
        {
            //Retrieving button from sender
            Button button = sender as Button;
            //Exception catching
            try
            {
                checked
                {
                    Field.Content = Convert.ToInt64(Field.Content) * 10 +
                    (button.Name[6] - '0');
                }
            }
            catch (OverflowException)
            {
                Field.Content = "Error";
                IsError = true;
            }
            catch(Exception)
            {
                ErrorHandling();
            }
            
        }

        void ErrorHandling()
        {
            Field.Content = 0;
            IsError = false;
        }

        #region Actions
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
        #endregion
        

        private void BackSpace_Click(object sender, RoutedEventArgs e)
        {
            if(!IsError)
            Field.Content = Convert.ToInt64(Field.Content) / 10;
            else
            {
                ErrorHandling();
            }
        }
    }
}
