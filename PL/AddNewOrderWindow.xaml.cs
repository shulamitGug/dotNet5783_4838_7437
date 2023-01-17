using System;
using System.Collections;
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
        /// <summary>
        /// A dependent feature that stores all the products in the catalog and updates when there is a change
        /// </summary>


        public ObservableCollection<BO.ProductItem?> CatalogProducts
        {
            get { return (ObservableCollection<BO.ProductItem?>)GetValue(CatalogProductsProperty); }
            set { SetValue(CatalogProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CatalogProducts.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CatalogProductsProperty =
            DependencyProperty.Register("CatalogProducts", typeof(ObservableCollection<BO.ProductItem?>), typeof(Window), new PropertyMetadata(null));
 
        private BO.Cart? currentCart;
        
        BlApi.IBl? bl = BlApi.Factory.Get();

        public AddNewOrderWindow(BO.Cart currentCartOut)
        {
            InitializeComponent();
            currentCart = currentCartOut;
            var temp = bl!.Product.GetCatalog();
            CatalogProducts = temp == null ? new() : new(temp);
     
        }


         /// <summary>
         /// add product to cart
         /// </summary>
         /// <param name="sender">product</param>
         /// <param name="e"></param>
        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            int id=(((BO.ProductItem)((FrameworkElement)sender).DataContext).Id);
            if (!((BO.ProductItem)((FrameworkElement)sender).DataContext).InStock)
                MessageBox.Show("The item is out of stock");
            else
            {
                currentCart = bl!.Cart.AddProduct(currentCart!,id);
                MessageBox.Show("the product add to cart");
            }
        }


        /// <summary>
        /// filter products by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CategorySelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category categories = (BO.Category)CategorySelector.SelectedItem;
            if (categories == (BO.Category.None))
            {
                var tmp= bl!.Product.GetCatalog();
                CatalogProducts = tmp == null ? new() : new(tmp);
            }
            else
            {
                var tmp = bl!.Product.GetCatalog(x=>x?.CategoryP==(DO.Category)categories);
                CatalogProducts = tmp == null ? new() : new(tmp);
            }

        }


        /// <summary>
        /// see the shopping cart
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ShopCartBtn_Click(object sender, RoutedEventArgs e)
        {
            new ShoppingCart(currentCart!).Show();
            this.Close();
        }


        /// <summary>
        /// see the product detail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductItem)((ListView)sender).SelectedItem).Id;
            new ProductItem(id).ShowDialog();   
        }


        /// <summary>
        /// Sort into groups by category
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupingBtn_Click(object sender, RoutedEventArgs e)
        {
            var x =
                     from prod in bl!.Product.GetCatalog()
                     group prod by prod.Category into g
                     select new { Key = g.Key, prod = g };
        }


        /// <summary>
        /// Sort by price
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderByPrice_Click(object sender, RoutedEventArgs e)
        {
            var temp = CatalogProducts.OrderBy(x => x?.Price);
            CatalogProducts = temp == null ? new() : new(temp);
        }



        /// <summary>
        /// Back to the main menu
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
