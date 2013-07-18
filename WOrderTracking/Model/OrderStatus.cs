using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrderTracking.Model
{
    public class OrderStatus
    {
        public DateTime Date { get; set; }
        public string Local { get; set; }
        public string Status { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
