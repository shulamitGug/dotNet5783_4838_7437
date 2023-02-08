using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Dependent attribute that describes input box for entering an order number
        /// </summary>
        public InputBox MyInput
        {
            get { return (InputBox)GetValue(MyInputProperty); }
            set { SetValue(MyInputProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyInput.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MyInputProperty =
            DependencyProperty.Register("MyInput", typeof(InputBox), typeof(Window), new PropertyMetadata(null));


        /// <summary>
        /// Constructor action for mainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            MyInput = new InputBox();
        }


        /// <summary>
        /// add order for customer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewOrd_Click(object sender, RoutedEventArgs e)
        {
            new AddNewOrderWindow().Show();
            this.Close();
        }


        /// <summary>
        /// open manager window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ManagerWindow_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show();
            this.Close();
        }


        /// <summary>
        /// change input box to be visible
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OrderTracking_Click(object sender, RoutedEventArgs e)
        {
           MyInput.Myvisibility = Visibility.Visible;
        }


        /// <summary>
        /// open order tracking according to id
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderTracking((int.Parse(MyInput.MyText))).Show();
                // Clear InputBox.
                MyInput.MyText = String.Empty;
                MyInput.Myvisibility =Visibility.Collapsed;
            }
            catch(BO.NotExistBlException ex)
            {
                MessageBox.Show($"{ex.InnerException}");
                MyInput.MyText = String.Empty;
            }
            catch (Exception)
            {
                MessageBox.Show("please enter number");
                MyInput.MyText = String.Empty;

            }
        }


        /// <summary>
        /// change input box to be hidden
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
            // Clear InputBox.
            InputTextBox.Text = String.Empty;

        }
    }




    /// <summary>
    /// new class that describe instance of input box
    /// </summary>
    public class InputBox: INotifyPropertyChanged
    {
        public Visibility Myvisibility //visible of input box
        {
            get { return myvisibility; }
            set
            {
                myvisibility = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("myvisibility"));
                }
            }
        }
        public string MyText //text of input box
        {
            get { return myText; }
            set
            {
                myText = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("myText"));
                }
            }
        }
        private string myText = String.Empty;
        private Visibility myvisibility= Visibility.Collapsed;
        public event PropertyChangedEventHandler? PropertyChanged;

    }

}
