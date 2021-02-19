using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppFruitshop.Models
{
    public class MultipleTableJoin
    {
        public Order orderlist { get; set; }
        public Customer customerlist { get; set; }
        public OrderDetail orderDetailist { get; set; }
    }
}