using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;

namespace LadeSkabClassLibrary.Models
{
    internal class RfidReader
    {
        public event EventHandler<RfidReaderChangedEventArgs> RfidReaderChangedEvent;
    }
}
