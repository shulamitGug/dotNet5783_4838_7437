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

        public BO.ProductItem productItem
        {
            get { return (BO.ProductItem)GetValue(productItemProperty); }
            set { SetValue(productItemProperty, value); }
        }

        // Using a DependencyProperty as the backing store for productItem.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productItemProperty =
            DependencyProperty.Register("productItem", typeof(BO.ProductItem), typeof(Window), new PropertyMetadata(null));

        public ProductItem(int id)
        {
            InitializeComponent();
            productItem=bl!.Product.GetProductItemById(id);
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
