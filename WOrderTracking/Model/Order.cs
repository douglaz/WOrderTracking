using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace WOrderTracking.Model
{
    [DataContract]
    public class Order
    {
        [XmlElement("Id")]
        public long Id { get; set; }
        [XmlElement("Name")]
        public string Name { get; set; }
        [XmlElement("TrackingCode")]
        public string TrackingCode { get; set; }
        [XmlArray("StatusHistory"), XmlArrayItem(typeof(OrderStatus), ElementName="OrderStatus")]
        public List<OrderStatus> StatusHistory { get; set; }

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
