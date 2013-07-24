using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace WOrderTracking.Model
{
    [XmlRoot]
    public class Order
    {
        [XmlAttribute("Id")]
        public long Id { get; set; }
        [XmlAttribute("Name")]
        public string Name { get; set; }
        [XmlAttribute("TrackingCode")]
        public string TrackingCode { get; set; }
        [XmlArray("StatusHistory"), XmlArrayItem(typeof(OrderStatus))]
        public List<OrderStatus> StatusHistory { get; set; }
        [XmlIgnoreAttribute]
        public OrderStatus LastStatus
        {
            get
            {
                return StatusHistory.OrderByDescending(s => s.Date).FirstOrDefault();
            }
        }

        public Order()
        {
            this.StatusHistory = new List<OrderStatus>();
        }
    }
}
