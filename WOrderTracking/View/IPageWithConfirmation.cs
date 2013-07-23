using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOrderTracking.View
{
    public interface IPageWithConfirmation
    {
        void DoCommandAfterConfirmation();
    }
}
