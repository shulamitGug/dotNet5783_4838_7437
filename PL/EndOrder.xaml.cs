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
    /// Interaction logic for EndOrder.xaml
    /// </summary>
    public partial class EndOrder : Window
    {




        public int OrdId
        {
            get { return (int)GetValue(OrdIdProperty); }
            set { SetValue(OrdIdProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OrdId.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrdIdProperty =
            DependencyProperty.Register("OrdId", typeof(int), typeof(Window), new PropertyMetadata(0));


        public EndOrder(int id)
        {
            InitializeComponent();
            OrdId = id;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
}
