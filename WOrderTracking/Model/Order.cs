using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml.Linq;

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

        public Order(XElement xElement)
        {
            Id = long.Parse(xElement.Attribute("Id").Value);
            Name = xElement.Attribute("Name").Value;
            TrackingCode = xElement.Attribute("TrackingCode").Value;
            StatusHistory = xElement.Descendants("StatusHistory").Elements().Select(o => new OrderStatus(o)).ToList();
        }

        public XElement ToXElement()
        {
            var xElement = new XElement("Order");
            xElement.SetAttributeValue("Name", this.Name);
            xElement.SetAttributeValue("TrackingCode", this.TrackingCode);
            foreach (var status in StatusHistory)
            {
                xElement.Add(status.ToXElement());
            }
            return xElement;
        }
    }
}
