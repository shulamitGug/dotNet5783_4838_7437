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
    /// Interaction logic for CustomerDetails.xaml
    /// </summary>
    public partial class CustomerDetails : Window
    {


        public BO.Cart currentCart
        {
            get { return (BO.Cart)GetValue(currentCartProperty); }
            set { SetValue(currentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for currentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty currentCartProperty =
            DependencyProperty.Register("currentCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));


        public CustomerDetails()
        {
            InitializeComponent();
            currentCart = new BO.Cart();
        }

        private void okayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdrss.Text == "" || txtEmail.Text == "" || txtName.Text == "")
                MessageBox.Show("לא הוזנו מספיק פרטים");
            else
            {
                new AddNewOrderWindow(currentCart).ShowDialog();
                this.Close();
            }
        }
    }
}
