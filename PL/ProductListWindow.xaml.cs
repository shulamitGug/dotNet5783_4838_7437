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
    /// Interaction logic for ProductDisplay.xaml
    /// </summary>
    public partial class ProductListWindow : Window
    {
        private BlApi.IBl bl = new BlImplementation.Bl();
        IEnumerable<BO.ProductForList?> products;
        public ProductListWindow(BlApi.IBl _bl)
        {
            InitializeComponent();
            bl = _bl;
            products = bl.Product.GetProductForList();
            ProductListview.ItemsSource = products;
            CategoriesSelector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }

        private void CategoriesSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BO.Category categories = (BO.Category)CategoriesSelector.SelectedItem;
            products = bl.Product.GetProductForListByCondition(x => x?.CategoryP== (DO.Category)categories);
            ProductListview.ItemsSource = products;
        }

        private void AddProductBtn_Click(object sender, RoutedEventArgs e)
        {
            new AddUpdateProduct(bl,"add").Show();

        }
    }
}
