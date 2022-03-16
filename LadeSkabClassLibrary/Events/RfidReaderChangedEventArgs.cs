using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkabClassLibrary.Events
{

    public class RfidReaderChangedEventArgs : EventArgs
    {
        public int RfidRead { get; set; }
    }
}
