using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using WOrderTracking.Model;
using WOrderTracking.Utils;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace WOrderTracking
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class AddNewOrder : WOrderTracking.Common.LayoutAwarePage
    {
        private Order viewOrder;

        public AddNewOrder()
        {
            this.InitializeComponent();
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
            if (navigationParameter == null)
            {
                viewOrder = new Order();
            }
            else
            {
                viewOrder = (Order)navigationParameter;
                NameTextBox.Text = viewOrder.Name;
                TrackingCodeTextBox.Text = viewOrder.TrackingCode;
            }

            if (pageState != null)
            {
                NameTextBox.Text = pageState["nameTextBox"] as string;
                TrackingCodeTextBox.Text = pageState["trackingCodeTextBox"] as string;
            }
            
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
            pageState.Add("nameTextBox", NameTextBox.Text);
            pageState.Add("trackingCodeTextBox", TrackingCodeTextBox.Text);
        }

        private async void AddNewOrderButton_Click(object sender, RoutedEventArgs e)
        {
            var orderName = NameTextBox.Text;
            var orderTrackingCode = TrackingCodeTextBox.Text;

            bool inputIsValid = ValidateInput(orderName, orderTrackingCode);

            if (inputIsValid)
            {
                viewOrder.Name = orderName;
                viewOrder.TrackingCode = orderTrackingCode;
                this.Frame.Navigate(typeof(MainPage), viewOrder);
            }
            else
            {
                var messageDialog = new SimpleMessageDialog("Error when adding order - please verify your input", "Error");
                await messageDialog.ShowAsync();
            }
        }

        private bool ValidateInput(string orderName, string orderTrackingCode)
        {
            var isValid = true;
            if (string.IsNullOrWhiteSpace(orderName))
            {
                isValid = false;
                SetTextBoxErrorStyle(NameTextBox);
            }
            else
            {
                SetTextBoxNormalStyle(NameTextBox);
            }

            if (string.IsNullOrWhiteSpace(orderTrackingCode) || !TrackingCodeValidator.ValidateTrackingCode(orderTrackingCode))
            {
                isValid = false;
                SetTextBoxErrorStyle(TrackingCodeTextBox);
            }
            else
            {
                SetTextBoxNormalStyle(TrackingCodeTextBox);
            }

            return isValid;
        }

        private void SetTextBoxErrorStyle(TextBox textBox)
        {
            textBox.BorderThickness = new Thickness(5);
            textBox.BorderBrush = new SolidColorBrush(Colors.Red);
        }

        private void SetTextBoxNormalStyle(TextBox textBox)
        {
            textBox.BorderThickness = new Thickness(2);
            textBox.BorderBrush = new SolidColorBrush(Colors.Black);
        }
    }
}
