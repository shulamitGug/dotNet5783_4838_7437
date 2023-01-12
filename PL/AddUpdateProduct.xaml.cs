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

namespace PL
{
    /// <summary>
    /// Interaction logic for AddUpdateProduct.xaml
    /// </summary>
    public partial class AddUpdateProduct : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();
        public BO.Product product
        {
            get { return (BO.Product)GetValue(productProperty); }
            set { SetValue(productProperty, value); }
        }

        // Using a DependencyProperty as the backing store for product.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty productProperty =
            DependencyProperty.Register("product", typeof(BO.Product), typeof(Window), new PropertyMetadata(null));

        bool update = false;
        public AddUpdateProduct()
        {
            InitializeComponent();
            product = new BO.Product();
            //CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
            CategoryProd_selector.SelectedItem = (BO.Category)7;
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
            product = bl!.Product.GetProductById(id);
            //CategoryProd_selector.ItemsSource = Enum.GetValues(typeof(BO.Category));
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
                         if(update==true)
                        {
                            bl!.Product.Update(product);
                            MessageBox.Show("the product updated");
                        }
                        else
                        {
                            bl!.Product.Add(product);
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
                    MessageBox.Show(""+ex);
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
                bl!.Product.Delete(product.ID);
                MessageBox.Show($"the product with {product.ID} id deleted");
                this.Close();
            }
            catch (BO.NotExistBlException ex)
            {
                MessageBox.Show($"{ex.InnerException}");
            }
        }
    }
    public class ConvertStatus : IValueConverter
    {
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return "add";
            return "update";

        }
    }
    public class convertStatusVisible : IValueConverter
    {
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return Visibility.Hidden;
            return Visibility.Visible;

        }
    }
    public class ConvertIsEnabled : IValueConverter
    {
        //convert from target property type to source property type
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((int)value == 0)
                return true;
            return false;

        }
    }
}
