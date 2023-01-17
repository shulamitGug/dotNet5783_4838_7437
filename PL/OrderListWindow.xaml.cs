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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        /// <summary>
        /// Dependent attribute to describe a dependent collection of orders in a list
        /// </summary>
        public ObservableCollection<BO.OrderForList?> Orders
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(OrdersProperty); }
            set { SetValue(OrdersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdersProperty =
            DependencyProperty.Register("Orders", typeof(ObservableCollection<BO.OrderForList?>), typeof(Window), new PropertyMetadata(null));

        public OrderListWindow(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            var temp = bl!.Order.GetOrders();
            Orders =temp==null?new():new(temp) ;
        }


        /// <summary>
        /// update Order by double click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)((ListView)sender).SelectedItem).ID;
            new UpdateOrder(id,"update").ShowDialog();
            var temp = bl!.Order.GetOrders();
            Orders = temp == null ? new() : new(temp);
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
