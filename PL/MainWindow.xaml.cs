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
        BlApi.IBl? bl = BlApi.Factory.Get();


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
        /// 
        public MainWindow()
        {
            InitializeComponent();
            MyInput = new InputBox();
        }
        private void addNewOrd_Click(object sender, RoutedEventArgs e)
        {
            new CustomerDetails().Show();
        }

        private void managerWindow_Click(object sender, RoutedEventArgs e)
        {
            new ManagerWindow().Show(); 
        }

        private void orderTracking_Click(object sender, RoutedEventArgs e)
        {
           MyInput.Myvisibility = Visibility.Visible;
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                new OrderTracking((int.Parse(MyInput.Text))).Show();
                MyInput.Myvisibility =Visibility.Collapsed;
                // Clear InputBox.
                MyInput.Text = String.Empty;
            }
            catch(BO.NotExistBlException ex)
            {
                MessageBox.Show($"{ex.InnerException}");
                MyInput.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show("please enter number");
                MyInput.Text = String.Empty;

            }
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            InputBox.Visibility = Visibility.Collapsed;
            // Clear InputBox.
            InputTextBox.Text = String.Empty;

        }
    }
    public class InputBox: INotifyPropertyChanged
    {
        public Visibility Myvisibility
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
        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("text"));
                }
            }
        }
        private string text= String.Empty;
        private Visibility myvisibility= Visibility.Collapsed;
        public event PropertyChangedEventHandler? PropertyChanged;

    }

}
