using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for SimulatorWindow.xaml
    /// </summary>
    public partial class SimulatorWindow : Window
    {

        public BO.Order Order
        {
            get { return (BO.Order)GetValue(OrderProperty); }
            set { SetValue(OrderProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Order.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrderProperty =
            DependencyProperty.Register("Order", typeof(BO.Order), typeof(Window), new PropertyMetadata(null));



        public MyTime Mytime
        {
            get { return (MyTime)GetValue(MytimeProperty); }
            set { SetValue(MytimeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Mytime.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MytimeProperty =
            DependencyProperty.Register("Mytime", typeof(MyTime), typeof(Window), new PropertyMetadata(null));


        BackgroundWorker back=new BackgroundWorker();
        
        public SimulatorWindow()
        {
            InitializeComponent();
            back.DoWork += B_DoWork;
            back.ProgressChanged += B_ProgressChanged;
            back.RunWorkerCompleted += Back_RunWorkerCompleted;
            back.WorkerSupportsCancellation = true;
            back.WorkerReportsProgress = true;
            back.RunWorkerAsync();

        }

        private void Back_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void B_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 1:

                    { //simulatoLbl.Content = DateTime.Now.ToString("h:mm:ss");
                        Mytime = new MyTime(Mytime?.seconds, Mytime?.begin, Mytime?.end, Mytime?.st, Mytime?.endTime, DateTime.Now.ToString("h:mm:ss"));
                        pbOrder.Value++;
                    }
                    break;
               //עדכון פקדים
                case 0:
                    {
                        Mytime = new MyTime(((Simulator.OurEventArgs)e.UserState!).seconds, DateTime.Now.ToString("h:mm:ss"), DateTime.Now.AddSeconds(((Simulator.OurEventArgs)e.UserState!).seconds).ToString("h:mm:ss"), ((Simulator.OurEventArgs)e.UserState!).order.Status==BO.OrderStatus.sent?BO.OrderStatus.provided: BO.OrderStatus.sent, DateTime.Now.AddSeconds(((Simulator.OurEventArgs)e.UserState!).seconds), DateTime.Now.ToString("h:mm:ss"));
                        Order = ((Simulator.OurEventArgs)e.UserState!).order;
                        pbOrder.Value=0;
                        //pbOrder.Minimum = 0;
                        pbOrder.Maximum = (int)(Mytime.seconds);
                    }
                    break;

            }
        }

        private void B_DoWork(object? sender, DoWorkEventArgs e)
        {
            Simulator.Simulator.Active();
            Simulator.Simulator.AddEventStop(Simulator_stopThread);
            Simulator.Simulator.AddEventPropertiesChanged(Simulator_propertiesChanged);
            while (!back.CancellationPending)
            {
                back.ReportProgress(1);
                Thread.Sleep(1000);
            }
        }

        private  void Simulator_propertiesChanged(object? sender,Simulator.OurEventArgs e)
        {
            back.ReportProgress(0, e);
        }
        private  void Simulator_stopThread(object? sender, EventArgs e)
        {
            back.CancelAsync();
            Simulator.Simulator.RemoveEventStop(Simulator_stopThread);
            Simulator.Simulator.RemoveEventPropertiesChanged(Simulator_propertiesChanged);
        }
        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    back.RunWorkerAsync();
        //}

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.StopActive();
        }

    }
    public class MyTime
    {
        public int? seconds { get; set; }
        public string? begin { get; set; }
        public string? end { get; set; }
        public string? clock { get; set; }
        public DateTime? endTime { get; set; }
        public BO.OrderStatus? st { get; set; }
        public MyTime(int? s,string? s1,string? f, BO.OrderStatus? status,DateTime? enda,string? _clock)
        {
            seconds = s;
            begin = s1;
            end = f;
            st = status;
            endTime = enda;
            clock = _clock;
        }
    }
}
