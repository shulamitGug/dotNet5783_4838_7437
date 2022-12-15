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
        private int productId;
        public AddUpdateProduct(BlApi.IBl _bl)
        {
            InitializeComponent();
            deleteProdBtn.Visibility = Visibility.Hidden;
            bl = _bl;
            state = "add";
            OkayBtn.Content = state;
            CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
        }
        public AddUpdateProduct(BlApi.IBl _bl, int id)
        {
            InitializeComponent();
            productId = id;
            deleteProdBtn.Visibility = Visibility.Visible;
            bl = _bl;
            state = "update";
            OkayBtn.Content = state;
            BO.ProductItem product = bl.Product.GetProductItemById(id);
            CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            idProd_txt.Text = product.Id.ToString();
            InStockProd_txt.Text = product.Amount.ToString();
            NameProd_txt.Text = product.Name;
            PriceProd_txt.Text = product.Price.ToString();
            CategoryProd_selector.SelectedItem=product.Category;
        }

        private void OkayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (idProd_txt.Text == "" || InStockProd_txt.Text == "" || NameProd_txt.Text == "" || PriceProd_txt.Text == "" )
                MessageBox.Show("empty");
            else
            {
                try
                {
                    BO.Product newProduct = new BO.Product() { ID = Convert.ToInt32(idProd_txt.Text), CategoryP = (BO.Category)CategoryProd_selector.SelectedItem, Price = Convert.ToInt32(PriceProd_txt.Text), InStock = Convert.ToInt32(InStockProd_txt.Text), Name = NameProd_txt.Text };

                    if (state == "add")
                    {
                        bl.Product.Add(newProduct);
                        MessageBox.Show("the product added");
                    }
                    else
                    {
                        bl.Product.Update(newProduct);
                        MessageBox.Show("the product updated");
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }    
                
            }
        }

        private void deleteProdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.Product.Delete(productId);
                MessageBox.Show($"the product with {productId} id deleted");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex+"");
            }
        }
    }
}
