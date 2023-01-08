using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for OrderTracking.xaml
    /// </summary>
    public partial class OrderTracking : Window
    {


        public BO.OrderTracking orderTracking
        {
            get { return (BO.OrderTracking)GetValue(orderTrackingProperty); }
            set { SetValue(orderTrackingProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orderTracking.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty orderTrackingProperty =
            DependencyProperty.Register("orderTracking", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        //public ObservableCollection<Tuple<DateTime?, string?>>? Tracking
        //{
        //    get { return (ObservableCollection<Tuple<DateTime?, string?>>?)GetValue(TrackingProperty); }
        //    set { SetValue(TrackingProperty, value); }
        //}

        //// Using a DependencyProperty as the backing store for Tracking.  This enables animation, styling, binding, etc...
        //public static readonly DependencyProperty TrackingProperty =
        //    DependencyProperty.Register("Tracking", typeof(ObservableCollection<Tuple<DateTime?, string?>>), typeof(Window), new PropertyMetadata(null));
        BlApi.IBl? bl = BlApi.Factory.Get();
        //BO.OrderTracking orderTracking;

        public OrderTracking(int id)
        {
            InitializeComponent();
            orderTracking = bl!.Order.StatusOrder(id);
            //var temp = orderTracking.Tracking;
            //Tracking = temp == null ? new() : new(temp);
            //idTxt.Text="order id "+orderTracking.ID.ToString();
            statusTxt.Text=orderTracking.Status.ToString();

        }

        private void orderDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
           new UpdateOrder(orderTracking.ID,"show").Show();
        }
    }
}
