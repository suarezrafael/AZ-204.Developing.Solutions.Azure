﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosDB
{
    public class Order
    {
        public string orderId { get; set; }

        public string category { get; set; }

        public int quantity { get; set; }
    }
}
