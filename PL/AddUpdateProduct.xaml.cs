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
    /// Interaction logic for AddUpdateProduct.xaml
    /// </summary>
    public partial class AddUpdateProduct : Window
    {
        private string state;
        BlApi.IBl? bl = BlApi.Factory.Get();

        private int productId;
        public AddUpdateProduct()
        {
            InitializeComponent();
            deleteProdBtn.Visibility = Visibility.Hidden;
            state = "add";
            OkayBtn.Content = state;
            CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategoryProd_selector.SelectedItem = (BO.Category)7;
        }
        /// <summary>
        /// An operation that receives a product id and updates its details
        /// </summary>
        /// <param name="_bl"> bl param from type BlApi.IBl</param>
        /// <param name="id"> Product id</param>
        public AddUpdateProduct( int id)
        {
            InitializeComponent();
            idProd_txt.IsEnabled = false;
            productId = id;
            deleteProdBtn.Visibility = Visibility.Visible;
            state ="update";
            OkayBtn.Content = state;
            BO.ProductItem product = bl!.Product.GetProductItemById(id);
            CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            idProd_txt.Text = product.Id.ToString();
            InStockProd_txt.Text = product.Amount.ToString();
            NameProd_txt.Text = product.Name;
            PriceProd_txt.Text = product.Price.ToString();
            CategoryProd_selector.SelectedItem=product.Category;
        }
        /// <summary>
        /// Action that happens while clicking the confirmation button and updates or adds the product respectively
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens</param>
        private void OkayBtn_Click(object sender, RoutedEventArgs e)
        {
            if (idProd_txt.Text == "" || InStockProd_txt.Text == "" || NameProd_txt.Text == "" || PriceProd_txt.Text == "")
                MessageBox.Show("Not enough data has been entered");
            else
            {
                try
                {
                    BO.Product newProduct = new BO.Product() { ID = Convert.ToInt32(idProd_txt.Text), CategoryP = (BO.Category)CategoryProd_selector.SelectedItem, Price = Convert.ToInt32(PriceProd_txt.Text), InStock = Convert.ToInt32(InStockProd_txt.Text), Name = NameProd_txt.Text };

                    if (state == "add")
                    {
                        bl!.Product.Add(newProduct);
                        MessageBox.Show("the product added");
                    }
                    else
                    {
                        bl!.Product.Update(newProduct);
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
        /// <summary>
        /// Action that happens while clicking the delete button and deletes the requested product
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens</param>
        private void deleteProdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Product.Delete(productId);
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
