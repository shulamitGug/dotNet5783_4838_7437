using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulator
{
    public static class Simulator
    {

        static volatile bool flagActive;
       static BlApi.IBl? bl = BlApi.Factory.Get();
        public static int open = 0;
        private static event EventHandler? stopThread;
        private static event EventHandler<OurEventArgs>? propertiesChanged;

        /// <summary>
        /// start the thread
        /// </summary>
        public static void Active()
        {
            open = 1;
            Random random = new Random();
            new Thread(() =>
            {
                BO.Order boOrd;
                flagActive = true;
                DateTime date;
                while (flagActive)
                {
                    int? oldId = bl!.Order.GetOldestOrderId();
                    if (oldId != null)
                    {
                        boOrd = bl!.Order.GetOrderDetails((int)oldId);
                        int secs = random.Next(3, 11);
                        date = DateTime.Now + new TimeSpan(secs * 1000);
                        propertiesChanged?.Invoke(null, new OurEventArgs(secs, boOrd));
                        Thread.Sleep(secs * 1000);

                        if (!(boOrd.Status == BO.OrderStatus.approved))
                            bl.Order.UpdateProvideDate(boOrd.ID);
                        else
                            bl!.Order.updateSendingDate(boOrd.ID);
                    }
                    else
                    {
                        Thread.Sleep(1000);
                    }

                }
                stopThread?.Invoke(null,EventArgs.Empty);
            }).Start();   
        }

        /// <summary>
        /// stop
        /// </summary>
        public static void StopActive()
        {
            flagActive = false;
            open = 0;
        }

        /// <summary>
        /// add event
        /// </summary>
        /// <param name="e"></param>
        public static void AddEventStop(EventHandler e)
        {
            stopThread += e;
        }


        /// <summary>
        /// add event
        /// </summary>
        /// <param name="e"></param>
        public static void AddEventPropertiesChanged(EventHandler<OurEventArgs> e)
        {
            propertiesChanged += e;
        }

        /// <summary>
        /// remove the event
        /// </summary>
        /// <param name="e"></param>
        public static void  RemoveEventStop(EventHandler e)
        {
            stopThread -= e;
        }
        
        /// <summary>
        /// remove the event
        /// </summary>
        /// <param name="e"></param>
        public static void RemoveEventPropertiesChanged(EventHandler<OurEventArgs> e)
        {
            propertiesChanged -= e;
        }
    }

    /// <summary>
    /// new class to save the details
    /// </summary>
    public class OurEventArgs : EventArgs
    {
        public BO.Order order;
        public int seconds;
        
        public OurEventArgs(int newSecond, BO.Order newOrder)
        {
            order = newOrder;
            seconds = newSecond;
        }
    }
}
