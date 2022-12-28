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
    /// Interaction logic for OrderListWindow.xaml
    /// </summary>
    public partial class OrderListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        IEnumerable<BO.OrderForList?> orders;
        public OrderListWindow(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            orders = bl!.Order.GetOrders();
            orderList.ItemsSource = orders;
        }

        private void orderList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.OrderForList)((System.Windows.Controls.ListView)sender).SelectedItem).ID;
            new UpdateOrder(id).ShowDialog();
            orders = bl!.Order.GetOrders();
            orderList.ItemsSource = orders;
        }
    }
}
