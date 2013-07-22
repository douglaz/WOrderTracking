using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOrderTracking.Model;

namespace WOrderTracking.View.Item
{
    public class OrderViewItem
    {
        public Order Wrapped { get; set; }
        public long Id { get { return Wrapped.Id; } }
        public string Name { get { return Wrapped.Name; } }
        public string LastStatus
        {
            get
            {
                return Wrapped.LastStatus == null ? "No status" : string.Format("{0} - {1} - {2}", Wrapped.LastStatus.Date, Wrapped.LastStatus.Local, Wrapped.LastStatus.Status);
            }
        }

        public OrderViewItem(Order order)
        {
            Wrapped = order;
        }
    }
}
