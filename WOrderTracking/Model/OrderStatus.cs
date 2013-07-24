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
        [XmlAttribute("Date")]
        public DateTime Date { get; set; }
        [XmlAttribute("Local")]
        public string Local { get; set; }
        [XmlAttribute("Status")]
        public string Status { get; set; }
        [XmlIgnoreAttribute]
        public string AdditionalInfo { get; set; }
    }
}
