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
using System.Collections.ObjectModel;
using System.Globalization;

namespace PL
{

    /// <summary>
    /// Interaction logic for ProductDisplay.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();


        public ObservableCollection<BO.ProductForList?> Products
        {
            get { return (ObservableCollection<BO.ProductForList?>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty =
            DependencyProperty.Register("Products", typeof(ObservableCollection<BO.ProductForList?>), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// A function that returns the list of products to display
        /// </summary>
        /// <param name="_bl"> bl param from type BlApi.IBl</param>
        public ProductListWindow(BlApi.IBl? _bl)
        {
            InitializeComponent();
            bl = _bl;
            var tmp = bl!.Product.GetProductForList(); 
            Products = tmp == null ? new():new(tmp);
        }
        /// <summary>
        /// Filter by category
        /// </summary>
        /// <param name="sender"> sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens </param>

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category categories = (BO.Category)CategoriesSelector.SelectedItem;
            if (categories == (BO.Category)7)
            {
                var tmp = bl!.Product.GetProductForList();
                Products = tmp == null ? new() : new(tmp);
            }
            else
            {
                var tmp = bl!.Product.GetProductForListByCategory(categories);
                Products = tmp == null ? new() : new(tmp);
            }
        }
        /// <summary>
        /// Adding a product
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens </param>
        private void AddProductBtn_Click(object sender,RoutedEventArgs e)
        {
            new AddUpdateProduct().ShowDialog();
            var tmp = bl!.Product.GetProductForList();
            Products = tmp == null ? new() : new(tmp);
            CategoriesSelector.SelectedItem = (BO.Category)7;
        }
        /// <summary>
        /// View product details by double clicking on it
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens</param>
        private void ProductListview_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int id = ((BO.ProductForList)((System.Windows.Controls.ListView)sender).SelectedItem).ID;
            new AddUpdateProduct(id).ShowDialog();
            var tmp = bl!.Product.GetProductForList();
            Products = tmp == null ? new() : new(tmp);
            CategoriesSelector.SelectedItem = (BO.Category)7;
        }


        //private void ProductListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{

        //}

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }
    }
   
}
