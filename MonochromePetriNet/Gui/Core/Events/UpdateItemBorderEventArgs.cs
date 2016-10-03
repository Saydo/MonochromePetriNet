using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonochromePetriNet.Gui.Core.Events
{
    public class UpdateItemBorderEventArgs : System.EventArgs
    {
        public bool IsChanged;

        public UpdateItemBorderEventArgs(bool isChanged)
            : base()
        {
            IsChanged = isChanged;
        }
    }
}
