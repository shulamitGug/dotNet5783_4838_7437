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
        /// <summary>
        /// A function to display the products
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens</param>
        private void ShowProductsBtn_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl).Show();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            new AddNewOrderWindow().Show();
        }
    }
}
