using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WOrderTracking.Model;

namespace WOrderTracking.View.Item
{
    public class OrderStatusViewItem
    {
        public OrderStatus Wrapped { get; private set; }
        public string Date { get { return Wrapped.Date.ToString("yyyy/MM/dd H:mm:ss"); } }
        public string Local { get { return Wrapped.Local; } }
        public string Status { get { return Wrapped.Status; } }
        public string AdditionalInfo { get { return Wrapped.AdditionalInfo; } }

        public OrderStatusViewItem(OrderStatus orderStatus)
        {
            Wrapped = orderStatus;
        }
    }
}
