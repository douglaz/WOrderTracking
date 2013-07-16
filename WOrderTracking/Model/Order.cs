using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrderTracking.Model
{
    public class Order
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string TrackingCode { get; set; }
        public List<OrderStatus> StatusHistory { get; set; }

        public OrderStatus LastStatus { 
            get {
                return StatusHistory.OrderByDescending(s => s.Date).FirstOrDefault();
            } 
        }

        public Order()
        {
            this.StatusHistory = new List<OrderStatus>();
        }
    }
}
