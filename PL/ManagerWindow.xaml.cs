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
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for ManagerWindow.xaml
    /// </summary>
    public partial class ManagerWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();

        public ManagerWindow()
        {
            InitializeComponent();
        }


        /// <summary>
        /// show all products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProductsShow_Click(object sender, RoutedEventArgs e)
        {
            new ProductListWindow(bl).Show();
            this.Close();
        }


        /// <summary>
        /// show all orders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderShow_Click(object sender, RoutedEventArgs e)
        {
            new OrderListWindow(bl!).Show();
            this.Close();
        }


        /// <summary>
        /// back to main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
