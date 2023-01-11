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

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).OrderItemId);
            MyCart = bl!.Cart.deleteProduct(MyCart,id);
        }

        //private void changeAmountBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
        //    int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
        //    bl!.Cart.UpdateAmountOfProduct(currentCart, id, amount);
        //    var temp = currentCart.Items;
        //    orderItems = temp == null ? new() : new(temp!);
        //    totalPriceTxt.Text = currentCart.TotalPrice.ToString();
        //}

        private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Cart.OrderConfirmation(MyCart);
                MessageBox.Show("The order is complete!");
                this.Close();
            }
            catch(BO.NotInStockException ex)
            {
                MessageBox.Show(ex+"");
            }
            catch (BO.NotValidException ex)
            { 
                MessageBox.Show(ex+" ");
                this.Close();
                new CustomerDetails().Show();
            }
        }
        private void backShopping_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            new AddNewOrderWindow(MyCart).Show();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount-1;
           MyCart=bl!.Cart.UpdateAmountOfProduct(MyCart, id, amount);
        }

        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((FrameworkElement)sender).DataContext).ProductId);
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount+1;
           MyCart= bl!.Cart.UpdateAmountOfProduct(MyCart, id, amount);
        }
    }
    public class ConvertEmptyCartGrid : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((IEnumerable<BO.OrderItem>)value==null)
                return Visibility.Hidden;
            return Visibility.Visible;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertEmptyCartText : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return Visibility.Visible;
            return Visibility.Hidden;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class ConvertPositiveAmount : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value > 1)
                return true;
            return false;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
