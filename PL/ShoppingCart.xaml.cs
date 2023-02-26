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
using System.Collections.ObjectModel;
using System.Globalization;

namespace PL
{
    /// <summary>
    /// Interaction logic for ShoppingCart.xaml
    /// </summary>
    public partial class ShoppingCart : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public BO.Cart MyCart
        {
            get { return (BO.Cart)GetValue(MyCartProperty); }
            set { SetValue(MyCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyCartProperty =
            DependencyProperty.Register("MyCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        public ShoppingCart(BO.Cart currentCartOut)
        {
            InitializeComponent();
            MyCart = currentCartOut;
        }

        /// <summary>
        /// delete product from cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
            MyCart = bl!.Cart.UpdateAmountOfProduct(MyCart, id, 0);

        }

        /// <summary>
        /// finish order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetails(MyCart).Show();
            this.Close();
        }

        /// <summary>
        /// continue buying
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackShopping_Click(object sender, RoutedEventArgs e)
        {
            new AddNewOrderWindow(MyCart).Show();
            this.Close();

        }

        /// <summary>
        /// at least one of the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdDown_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount-1;
           MyCart=bl!.Cart.UpdateAmountOfProduct(MyCart, id, amount);
        }


        /// <summary>
        /// add one of the product
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmdUp_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount+1;
            try
            {
                MyCart = bl!.Cart.UpdateAmountOfProduct(MyCart, id, amount);
            }
            catch(Exception)
            {
                MessageBox.Show("אזל המלאי");
                MyCart!.Items!.FirstOrDefault(x => x?.ProductId == id)!.Amount--;
            }
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
