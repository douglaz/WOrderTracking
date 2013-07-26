using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;
using WOrderTracking.Model;
using WOrderTracking.Persistence;

namespace WOrderTracking.Tasks
{
    public class CorreiosTask : IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var deferral = taskInstance.GetDeferral();

            var orderDAO = new OrderDAO();

            var orders = orderDAO.FindAll();

            var newStatusCount = 0;
            foreach (var order in orders)
            {
                var oldStatus = order.StatusHistory.ToArray();
                var latestStatus = GetLatestStatus(order);
                var newStatus = latestStatus.Except(oldStatus).ToArray();
                newStatusCount += newStatus.Length;
                var updatedStatus = oldStatus.Union(latestStatus).ToList();
                order.StatusHistory = updatedStatus;
                orderDAO.Save(order);
                ShowNotification(order, newStatus);
            }

            SendBadgeNotification(newStatusCount.ToString());
            SendTileTextNotification(newStatusCount.ToString());

            deferral.Complete();
        }

        private void ShowNotification(Order order, OrderStatus[] newStatus)
        {
            foreach(var status in newStatus)
            {
                SendToastNotification(string.Format("Order {0} change: {1} - {2}", order.Name, status.Date, status.Status));
            }
        }

        private IList<OrderStatus> GetLatestStatus(Order order)
        {
            // TODO: really connect and get stuff
            return order.StatusHistory.Concat(new[] { new OrderStatus() { Date = DateTime.Now, Status = "Something new happened at: " + DateTime.Now } }).ToArray();
        }

        public void SendToastNotification(string message, string imageName = null)
        {
            var notificationXml = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastImageAndText01);
            var toastElements = notificationXml.GetElementsByTagName("text");
            toastElements[0].AppendChild(notificationXml.CreateTextNode(message));
            if (string.IsNullOrEmpty(imageName))
            {
                imageName = @"Assets/Logo.png";
            }
            var imageElement = notificationXml.GetElementsByTagName("image");
            imageElement[0].Attributes[1].NodeValue = imageName;
            var toastNotification = new ToastNotification(notificationXml);
            ToastNotificationManager.CreateToastNotifier().Show(toastNotification);
        }

        private void SendBadgeNotification(string message)
        {
            var badgeXml = BadgeUpdateManager.GetTemplateContent(BadgeTemplateType.BadgeGlyph);
            var badgeElement = (XmlElement)badgeXml.SelectSingleNode("/badge");
            badgeElement.SetAttribute("value", message);
            var badge = new BadgeNotification(badgeXml);
            BadgeUpdateManager.CreateBadgeUpdaterForApplication().Update(badge);
        }

        private static void SendTileTextNotification(string message)
        {
            var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileWideText04);
            var tileAttributes = tileXml.GetElementsByTagName("text");
            tileAttributes[0].AppendChild(tileXml.CreateTextNode(message));
            var tileNotification = new TileNotification(tileXml);
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

    }
}
