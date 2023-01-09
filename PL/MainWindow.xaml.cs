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

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        /// <summary>
        /// Constructor action for mainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }
        private void addNewOrd_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetails().Show();
        }

        private void managerWindow_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show(); 
        }

        private void orderTracking_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Visible;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            String input = InputTextBox.Text;
            try
            {
                new OrderTracking((int.Parse(input))).Show();

                InputBox.Visibility = System.Windows.Visibility.Collapsed;
                // Clear InputBox.
                InputTextBox.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("please enter number");
            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = System.Windows.Visibility.Collapsed;

            // Clear InputBox.
            InputTextBox.Text = String.Empty;

        }
    }
}
