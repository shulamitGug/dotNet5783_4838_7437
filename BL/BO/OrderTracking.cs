﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class OrderTracking
    {
        public int ID { get; set; }

        public OrderStatus? StatusOrder { get; set; }
        public List<Tuple<DateTime?, string?>>? Tracking { get; set; }
        public override string ToString()
        {
            return ToolStringClass.ToStringProperty(this);
        }

    }
}
