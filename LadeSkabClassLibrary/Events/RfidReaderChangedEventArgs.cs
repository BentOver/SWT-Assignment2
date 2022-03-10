using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkabClassLibrary.Events
{
    public enum RFIDState
    {
        Accepted,
        Declined
    }
    public class RfidReaderChangedEventArgs : EventArgs
    {
        public RFIDState RfidState { get; set; }
    }
}
