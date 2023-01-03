using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Order? newOrder
        {
            get { return (BO.Order)GetValue(newOrderProperty); }
            set { SetValue(newOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newOrderProperty =
            DependencyProperty.Register("newOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public UpdateOrder(int id,string state)
        {
            InitializeComponent();
           newOrder= bl!.Order.GetOrderDetails(id);
            Help.setX(state);
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void updateShipDateBtn_Click(object sender, RoutedEventArgs e)
        {
            newOrder = bl!.Order.updateSendingDate(newOrder!.ID);
        }
        private void updateDerliveryDateBtn_Click(object sender, RoutedEventArgs e)
        {
            newOrder=bl!.Order.UpdateProvideDate(newOrder!.ID);
        }

    }
   public  class ConvertDate1 : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(!Help.getX())
                return Visibility.Hidden;
            if (value == null)
            return Visibility.Visible;
            return Visibility.Hidden;
        }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }

}
