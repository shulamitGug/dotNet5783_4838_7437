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

        /// <summary>
        /// dependency property of customer's cart 
        /// </summary>

        public BO.Cart CurrentCart
        {
            get { return (BO.Cart)GetValue(CurrentCartProperty); }
            set { SetValue(CurrentCartProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentCart.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentCartProperty =
            DependencyProperty.Register("CurrentCart", typeof(BO.Cart), typeof(Window), new PropertyMetadata(null));



        public CustomerDetails()
        {
            InitializeComponent();
            CurrentCart = new BO.Cart();
        }



        /// <summary>
        /// show all catalog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OkayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (txtAdrss.Text == "" || txtEmail.Text == "" || txtName.Text == "")
                MessageBox.Show("Not enough data has been entered");
            else
            {
                new AddNewOrderWindow(CurrentCart).Show();
                this.Close();

            }
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
