using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WOrderTracking.Model
{
    public class OrderStatus
    {
        [XmlElement("Date")]
        public DateTime Date { get; set; }
        [XmlElement("Local")]
        public string Local { get; set; }
        [XmlElement("Status")]
        public string Status { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
