using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WOrderTracking.Model;
using WOrderTracking.View.Item;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WOrderTracking
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class OrderDetails : WOrderTracking.Common.LayoutAwarePage
    {
        public OrderDetails()
        {
            this.InitializeComponent();
            detailsList.ItemsSource = BuildOrders().First().StatusHistory.Select(s => new OrderStatusViewItem(s));

        }

        private IList<Order> BuildOrders()
        {
            return new List<Order>() { 
                new Order() { 
                    TrackingCode = "ABC678HUJ000", 
                    Name = "Order1", 
                    StatusHistory = new List<OrderStatus>(){
                        new OrderStatus(){
                            Date = DateTime.Now, 
                            Local = "Jaguaré", 
                            Status="Encaminhamento",
                            },
                            new OrderStatus(){
                            Date = DateTime.Now.AddDays(-1), 
                            Local = "Centro", 
                            Status="Transferindo" 
                            }
                    }
                }, 
                new Order(){
                    TrackingCode="64N5I56NIN",
                    Name = "Order 2",
                    StatusHistory = new List<OrderStatus>(){
                        new OrderStatus(){
                            Date = DateTime.Now.AddMonths(-2),
                            Local = "São Paulo",
                            Status = "Em transporte"
                        }
                    }
                },
                new Order(){
                    TrackingCode="I4565IO4JIO",
                    Name = "Order 3",
                    StatusHistory = new List<OrderStatus>(){
                        new OrderStatus(){
                            Date = DateTime.Now.AddMonths(-2),
                            Local = "Hong Kong",
                            Status = "Enviado"
                        }
                    }
                }
            };
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
