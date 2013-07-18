using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Popups;

namespace WOrderTracking.Utils
{
    public class SimpleMessageDialog
    {
        private MessageDialog messageDialog;

        public SimpleMessageDialog(string content, string title)
        {
            messageDialog = new MessageDialog(content, title);
            messageDialog.CancelCommandIndex = 0;
            messageDialog.DefaultCommandIndex = 0;
            messageDialog.Commands.Add(new UICommand("OK", new UICommandInvokedHandler(this.OKButtonClicked)));
        }

        public async Task ShowAsync()
        {
            await messageDialog.ShowAsync();
        }

        private void OKButtonClicked(IUICommand command)
        {            
            
        }
    }
}
