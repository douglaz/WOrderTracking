using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using WOrderTracking.Model;
using Windows.ApplicationModel;
using System.Xml.Linq;

namespace WOrderTracking.Persistence
{
    public class OrderDAO
    {
        public IList<Order> FindAll()
        {
            //var storageFolder = Package.Current.InstalledLocation;
            //var storageFile =  storageFolder.GetFileAsync("Order.xml");

            //var serializer = new XmlSerializer(typeof(List<Order>));
            //using (var stream = new StreamReader("Order.xml"))
            //{
            //    return serializer.Deserialize(stream) as List<Order>;
            //}

            var ordersXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Persistence/Orders.xml");
            var loadedData = XDocument.Load(ordersXMLPath);

            var orders = loadedData.Descendants("Order").Select(o => new Order
                    {
                        Id = (long)o.Element("Id"),
                        Name = (string)o.Element("Name"),
                        TrackingCode = (string)o.Element("TrackingCode"),
                        StatusHistory = o.Descendants("StatusHistory").Select(s => new OrderStatus()
                        {
                            Local = (string)s.Element("Local"),
                            Status = (string)s.Element("Status"),
                            Date = (DateTime)s.Element("Date")
                        }).ToList()
                    });

            return orders.ToList();
        }
    }
}
