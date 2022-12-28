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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        BO.Order newOrder;

        public UpdateOrder(int id)
        {
            InitializeComponent();
           newOrder= bl!.Order.GetOrderDetails(id);
            stackPanel.DataContext = newOrder;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void updateShipDateBtn_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.DataContext = newOrder;
            newOrder = bl!.Order.updateSendingDate(newOrder.ID);
            MessageBox.Show("" + newOrder);

        }
        private void updateDerliveryDateBtn_Click(object sender, RoutedEventArgs e)
        {
            stackPanel.DataContext = newOrder;
            newOrder = bl!.Order.UpdateProvideDate(newOrder.ID);

        }
    }
}
