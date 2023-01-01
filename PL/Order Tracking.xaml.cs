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
    /// Interaction logic for Order_Tracking.xaml
    /// </summary>
    public partial class Order_Tracking : Window
    {
        public BO.OrderTracking? orderTracking
        {
            get { return (BO.OrderTracking?)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));
        BlApi.IBl? bl = BlApi.Factory.Get();

        public Order_Tracking(int id)
        {
            InitializeComponent();
          orderTracking= bl!.Order.StatusOrder(5);
            MessageBox.Show(orderTracking.Status+"ff");
        }

        private void ShowBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
