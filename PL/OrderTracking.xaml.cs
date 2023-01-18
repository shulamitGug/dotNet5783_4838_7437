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


        /// <summary>
        /// dependancy property of order tracking
        /// </summary>
        public BO.OrderTracking? MyOrderTrackng
        {
            get { return (BO.OrderTracking?)GetValue(MyOrderTrackngProperty); }
            set { SetValue(MyOrderTrackngProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyOrderTrackng.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyOrderTrackngProperty =
            DependencyProperty.Register("MyOrderTrackng", typeof(BO.OrderTracking), typeof(Window), new PropertyMetadata(null));


        BlApi.IBl? bl = BlApi.Factory.Get();


        /// <summary>
        /// constructor of order tracking window
        /// </summary>
        /// <param name="id"></param>
        public OrderTracking(int id)
        {
            InitializeComponent();
            MyOrderTrackng = bl!.Order.StatusOrder(id);
        }


        /// <summary>
        /// show all the details of this order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderDetailsBtn_Click(object sender, RoutedEventArgs e)
        {
           new UpdateOrder(MyOrderTrackng!.ID,"show").Show();
        }


        /// <summary>
        /// back to the main window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
