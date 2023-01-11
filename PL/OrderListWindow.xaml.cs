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


        public ObservableCollection<BO.OrderForList?> orders
        {
            get { return (ObservableCollection<BO.OrderForList?>)GetValue(ordersProperty); }
            set { SetValue(ordersProperty, value); }
        }

        // Using a DependencyProperty as the backing store for orders.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ordersProperty =
            DependencyProperty.Register("orders", typeof(ObservableCollection<BO.OrderForList>), typeof(Window), new PropertyMetadata(null));


        public OrderListWindow(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            var temp = bl!.Order.GetOrders();
            orders =temp==null?new():new(temp) ;
        }

        private void orderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)((ListView)sender).SelectedItem).ID;
            new UpdateOrder(id,"update").ShowDialog();
            var temp = bl!.Order.GetOrders();
            orders = temp == null ? new() : new(temp);
        }
    }
}
