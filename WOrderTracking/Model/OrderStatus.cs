using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace WOrderTracking.Model
{
    public class OrderStatus
    {
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }
        [XmlAttribute("Local")]
        public string Local { get; set; }
        [XmlAttribute("Status")]
        public string Status { get; set; }
        [XmlIgnoreAttribute]
        public string AdditionalInfo { get; set; }

        public OrderStatus()
        {
        }

        public OrderStatus(XElement xElement)
        {
            Local = xElement.Attribute("Local").Value;
            Status = xElement.Attribute("Status").Value;
            Date = DateTime.Parse(xElement.Attribute("Date").Value);
        }

        public XElement ToXElement()
        {
            var xElement = new XElement("OrderStatus");
            xElement.SetAttributeValue("Local", this.Local);
            xElement.SetAttributeValue("Status", this.Status);
            xElement.SetAttributeValue("Date", this.Date);
            return xElement;
        }
    }
}
