using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using WOrderTracking.View;

namespace WOrderTracking.Utils
{
    public class ConfirmationMessageDialog
    {
        private MessageDialog messageDialog;
        private IPageWithConfirmation pageRoot;

        public ConfirmationMessageDialog(string content, string title, IPageWithConfirmation pageRoot)
        {
            this.pageRoot = pageRoot;

            messageDialog = new MessageDialog(content, title);
            messageDialog.CancelCommandIndex = 1;
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.Commands.Add(new UICommand("Confirm", new UICommandInvokedHandler(this.ConfirmButton_Click)));
            messageDialog.Commands.Add(new UICommand("Cancel", new UICommandInvokedHandler(this.CancelButton_Click)));
        }

        public async Task ShowAsync()
        {
            await messageDialog.ShowAsync();
        }

        private void ConfirmButton_Click(IUICommand command)
        {
            pageRoot.DoCommandAfterConfirmation();
        }

        private void CancelButton_Click(IUICommand command)
        {

        }
    }
}
