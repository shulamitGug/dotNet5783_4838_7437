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
    /// Interaction logic for AddNewOrderWindow.xaml
    /// </summary>
    public partial class AddNewOrderWindow : Window
    {
        public ObservableCollection<BO.ProductItem?> catalogProducts
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(catalogProductsProperty); }
            set { SetValue(catalogProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty catalogProductsProperty =
            DependencyProperty.Register("catalogProducts", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
        private BO.Cart? currentCart;

        //IEnumerable<BO.ProductItem?> products;
        BlApi.IBl? bl = BlApi.Factory.Get();
        public AddNewOrderWindow(BO.Cart currentCartOut)
        {
            InitializeComponent();
            currentCart = currentCartOut;
            var temp = bl!.Product.GetCatalog();
            catalogProducts = temp == null ? new() : new(temp);
            categorySelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            categorySelector.SelectedItem= (BO.Category.None);
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            int id=(((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).Id);
            if (!((BO.ProductItem)((System.Windows.FrameworkElement)sender).DataContext).InStock)
                MessageBox.Show("הפריט חסר במלאי");
            else
            {
                currentCart = bl!.Cart.AddProduct(currentCart!,id);
                MessageBox.Show("the product add to cart");
            }
        }

        private void categorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category categories = (BO.Category)categorySelector.SelectedItem;
            if (categories == (BO.Category.None))
            {
                var tmp = bl!.Product.GetCatalog();
                catalogProducts = tmp == null ? new() : new(tmp);
            }
            else
            {
                var tmp = bl!.Product.GetCatalog(x=>x?.CategoryP==(DO.Category)categories);
                catalogProducts = tmp == null ? new() : new(tmp);
            }

        }

        private void shopCartBtn_Click(object sender, RoutedEventArgs e)
        {
            new ShoppingCart(currentCart!).ShowDialog();
            this.Close();
        }
    }
}
