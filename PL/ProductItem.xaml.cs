using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for ProductItem.xaml
    /// </summary>
    public partial class ProductItem : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();


        /// <summary>
        /// A dependent attribute that describes product details
        /// </summary>
        public BO.ProductItem MyProductItem
        {
            get { return (BO.ProductItem)GetValue(MyProductItemProperty); }
            set { SetValue(MyProductItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProductItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyProductItemProperty =
            DependencyProperty.Register("MyProductItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));



        /// <summary>
        /// A constructive action that initializes the product
        /// </summary>
        /// <param name="id"></param>
        public ProductItem(int id)
        {
            InitializeComponent();
            MyProductItem = bl!.Product.GetProductItemById(id);
        }

        /// <summary>
        /// Back to viewing all products
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
