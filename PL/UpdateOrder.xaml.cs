﻿using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for UpdateOrder.xaml
    /// </summary>
    public partial class UpdateOrder : Window
    {
        BlApi.IBl? bl = BlApi.Factory.Get();



        public Visibility State
        {
            get { return (Visibility)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        // Using a DependencyProperty as the backing store for State.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(Visibility), typeof(Window), new PropertyMetadata(null));




        public BO.Order? newOrder
        {
            get { return (BO.Order)GetValue(newOrderProperty); }
            set { SetValue(newOrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for newOrder.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty newOrderProperty =
            DependencyProperty.Register("newOrder", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));

        public UpdateOrder(int id, string _state)
        {
            InitializeComponent();
            State = _state == "show" ? Visibility.Hidden : Visibility.Visible;
            newOrder = bl!.Order.GetOrderDetails(id);
            //Help.setX(state);
        }
        private void updateShipDateBtn_Click(object sender, RoutedEventArgs e)
        {
            newOrder = bl!.Order.updateSendingDate(newOrder!.ID);
        }

        private void updateDerliveryDateBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                newOrder = bl!.Order.UpdateProvideDate(newOrder!.ID);
            }
            catch(Exception ex)
            {
                MessageBox.Show($"{ex.Message}");
            }

        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }

}
