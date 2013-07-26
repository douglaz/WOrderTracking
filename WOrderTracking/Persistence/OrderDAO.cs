using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Windows.ApplicationModel;
using Windows.Storage;
using WOrderTracking.Model;

namespace WOrderTracking.Persistence
{
    public class OrderDAO
    {
        public IList<Order> FindAll()
        {
            var ordersXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Persistence/Orders.xml");
            var loadedData = XDocument.Load(ordersXMLPath);

            var orders = loadedData.Descendants("Order").Select(o => new Order(o));
            return orders.ToList();
        }

        public Order FindById(long id)
        {
            return FindAll().SingleOrDefault(o => o.Id == id);
        }

        public async void Save(Order order)
        {
            var ordersXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Persistence\\Orders.xml");
            var uriPath = new Uri("ms-appx:///Persistence/Orders.xml");
            StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uriPath);

            using (var file = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var stream = file.AsStreamForWrite())
                {
                    var loadedData = XDocument.Load(ordersXMLPath);
                    loadedData.Add(order.ToXElement());
                    loadedData.Save(stream);
                }
            }
        }

        public async void Delete(Order order)
        {
            var ordersXMLPath = Path.Combine(Package.Current.InstalledLocation.Path, "Persistence\\Orders.xml");
            var uriPath = new Uri("ms-appx:///Persistence/Orders.xml");
            StorageFile storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(uriPath);

            using (var file = await storageFile.OpenAsync(FileAccessMode.ReadWrite))
            {
                using (var stream = file.AsStreamForWrite())
                {
                    var loadedData = XDocument.Load(ordersXMLPath);
                    var orderToDelete = loadedData.Descendants("Order").Single(o => long.Parse(o.Attribute("Id").Value) == order.Id);
                    orderToDelete.Remove();
                    loadedData.Save(stream);
                }
            }
        }
    }
}
