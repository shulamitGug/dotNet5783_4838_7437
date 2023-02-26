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
using System.Globalization;
using Microsoft.Win32;
using System.Text.RegularExpressions;

namespace PL
{
    /// <summary>
    /// Interaction logic for AddUpdateProduct.xaml
    /// </summary>
    public partial class AddUpdateProduct : Window
    {

        BlApi.IBl? bl = BlApi.Factory.Get();


        public BO.Product Product
        {
            get { return (BO.Product)GetValue(ProductProperty); }
            set { SetValue(ProductProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductProperty =
            DependencyProperty.Register("Product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

        private bool update = false;
        
        public AddUpdateProduct()
        {
            InitializeComponent();
            Product = new BO.Product();
            Product.CategoryP= BO.Category.None;
        }



        /// <summary>
        /// An operation that receives a product id and updates its details
        /// </summary>
        /// <param name="_bl"> bl param from type BlApi.IBl</param>
        /// <param name="id"> Product id</param>
        public AddUpdateProduct(int id)
        {
            InitializeComponent();
            update = true;
            Product = bl!.Product.GetProductById(id);
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
                    try
                    {
                        if (update == true)
                        {
                            bl!.Product.Update(Product);
                            MessageBox.Show("the product updated");
                        }
                        else
                        {
                            bl!.Product.Add(Product);
                            MessageBox.Show("the product added");
                        }
                    }
                    catch (BO.AlreadyExistBlException ex)
                    {
                        MessageBox.Show($"{ex.InnerException}");
                    }
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" + ex);
                }

            }
        }
        /// <summary>
        /// Action that happens while clicking the delete button and deletes the requested product
        /// </summary>
        /// <param name="sender">sender runtime variable</param>
        /// <param name="e">A variable of the type of event that happens</param>
        private void DeleteProdBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl!.Product.Delete(Product.ID);
                MessageBox.Show($"the product with {Product.ID} id deleted");
                this.Close();
            }
            catch (BO.NotExistBlException ex)
            {
                MessageBox.Show($"{ex.InnerException}");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message+"");
            }
        }

        /// <summary>
        /// choose picture
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            BO.Product p = new BO.Product();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                p.InStock=Product.InStock;
                p.Name=Product.Name;
                p.Price=Product.Price;
                p.CategoryP=Product.CategoryP;
                p.Image = openFileDialog.SafeFileName;
                
            }
            if (update)
                p.ID = Product.ID;
            int tempId=Product.ID; ;
            Product = p;
            Product.ID = tempId;    

        }



        /// <summary>
        /// Back to the main menu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Force user to enter numbers only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
  
}
