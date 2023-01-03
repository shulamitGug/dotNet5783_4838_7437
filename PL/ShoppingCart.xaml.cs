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


        BO.Cart currentCart;
        public ObservableCollection<BO.OrderItem> orderItems
        {
            get { return (ObservableCollection<BO.OrderItem>)GetValue(orderItemsProperty); }
            set { SetValue(orderItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderItemsProperty =
            DependencyProperty.Register("orderItems", typeof(ObservableCollection<BO.OrderItem>), typeof(Window), new PropertyMetadata(null));

        // Using a DependencyProperty as the backing store for totalPrice.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty totalPriceProperty =
            DependencyProperty.Register("totalPrice", typeof(int), typeof(Window), new PropertyMetadata(0));
        public ShoppingCart(BO.Cart currentCartOut)
        {
            InitializeComponent();
            currentCart = currentCartOut;
            var temp = currentCart.Items;
            orderItems =temp==null?new():new(temp!) ;
            totalPriceTxt.Text = $"total price= {currentCart.TotalPrice}";
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).OrderItemId);
            currentCart = bl!.Cart.deleteProduct(currentCart,id);
            var temp = currentCart.Items;
            orderItems = temp == null ? new() : new(temp!);
            totalPriceTxt.Text = currentCart.TotalPrice.ToString();
        }

        private void changeAmountBtn_Click(object sender, RoutedEventArgs e)
        {
            int id = (((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).ProductId);
            int amount = ((BO.OrderItem)((System.Windows.FrameworkElement)sender).DataContext).Amount;
            bl!.Cart.UpdateAmountOfProduct(currentCart, id, amount);
            var temp = currentCart.Items;
            orderItems = temp == null ? new() : new(temp!);
            totalPriceTxt.Text = currentCart.TotalPrice.ToString();
        }

        private void OrderConfirmation_Click(object sender, RoutedEventArgs e)
        {
            bl!.Cart.OrderConfirmation(currentCart);
            MessageBox.Show("ההזמנה הושלמה!");
            this.Close();
        }
    }
    public class ConvertEmptyCart : IValueConverter
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

}
