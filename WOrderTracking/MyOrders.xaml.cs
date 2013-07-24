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
using WOrderTracking.Persistence;
using WOrderTracking.Utils;
using WOrderTracking.View;
using WOrderTracking.View.Item;
using System.Collections.ObjectModel;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WOrderTracking
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MyOrders : WOrderTracking.Common.LayoutAwarePage, IPageWithConfirmation
    {
        private ObservableCollection<OrderViewItem> myOrders;
        private OrderDAO orderDAO = new OrderDAO();

        public MyOrders()
        {
            this.InitializeComponent();
            myOrders = new ObservableCollection<OrderViewItem>();
            OrderViewItemsSource.Source = myOrders;
            SetOrders(orderDAO.FindAll().Select(o => new OrderViewItem(o)));
        }

        public void DoCommandAfterConfirmation()
        {
            DeleteSelectedOrders();
        }

        public void SetOrders(IEnumerable<OrderViewItem> orders)
        {
            foreach (var order in orders)
            {
                myOrders.Add(order);
            }
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

        private void MyOrdersGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var orderId = ((OrderViewItem)e.ClickedItem).Id;
            this.Frame.Navigate(typeof(OrderDetails), orderId);
        }

        private void MyOrdersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteOrderButton.Visibility = e.AddedItems.Any() ? Visibility.Visible : Visibility.Collapsed;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AddNewOrder));
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            var messageDialog = new ConfirmationMessageDialog("Are you sure you want to delete this selected orders?", "Delete", this);
            await messageDialog.ShowAsync();
        }
        
        private void DeleteSelectedOrders()
        {
            foreach (OrderViewItem order in MyOrdersGrid.SelectedItems.ToList())
            {
                myOrders.Remove(order);
                //orderDAO.Delete(order.Wrapped);
            }

            SharedBottomAppBar.IsOpen = false;
        }
    }
}
