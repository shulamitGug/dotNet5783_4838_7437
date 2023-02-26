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
        BackgroundWorker back=new BackgroundWorker();
        
        public SimulatorWindow()
        {
            InitializeComponent();
            back.DoWork += B_DoWork;
            back.ProgressChanged += B_ProgressChanged;
            back.WorkerSupportsCancellation = true;
            back.WorkerReportsProgress = true;
        }

        private void B_ProgressChanged(object? sender, ProgressChangedEventArgs e)
        {
            switch(e.ProgressPercentage)
            {
                case 1:
                    simulatoLbl.Content = DateTime.Now.ToString("h:mm:ss");
                    break;
               //עדכון פקדים
                case 0:
                    {
                        startTime.Content= DateTime.Now.ToString("h:mm:ss");
                        finishTime.Content=(DateTime.Now+new TimeSpan(((Simulator.OurEventArgs)e.UserState).seconds)).TimeOfDay;
                        idLbl.Content = ((Simulator.OurEventArgs)e.UserState).order;
                        secLbl.Content = ((Simulator.OurEventArgs)e.UserState).seconds;
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
                Thread.Sleep(1000);
                back.ReportProgress(1);
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
            Simulator.Simulator.RemoveEventPropertiesChanged(Simulator_stopThread);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            back.RunWorkerAsync();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Simulator.Simulator.StopActive();
        }
    }
}
