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
    /// Interaction logic for AddUpdareProduct.xaml
    /// </summary>
    public partial class AddUpdateProduct : Window
    {
        private string state;
        private BlApi.IBl bl = new BlImplementation.Bl();

        public AddUpdateProduct(BlApi.IBl _bl,string _state)
        {
            InitializeComponent();
            bl=_bl;
            state = _state;
            CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        public AddUpdateProduct(BlApi.IBl _bl, int id)
        {
            InitializeComponent();
            bl = _bl;
            state = "update";

        }

        private void OkayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (idProd_txt.Text == "" || InStockProd_txt.Text == "" || NameProd_txt.Text == "" || PriceProd_txt.Text == "" || CategoryProd_selector.SelectedItem == "")
                MessageBox.Show("empty");
            else
            {
                try
                {
                    BO.Product newProduct = new BO.Product() { ID = Convert.ToInt32(idProd_txt.Text), CategoryP = (BO.Category)CategoryProd_selector.SelectedItem, Price = Convert.ToInt32(PriceProd_txt.Text), InStock = Convert.ToInt32(InStockProd_txt.Text), Name = NameProd_txt.Text };
                    bl.Product.Add(newProduct);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }    
                
            }
        }
    }
}
