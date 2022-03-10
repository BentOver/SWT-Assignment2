using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;
using LadeSkabClassLibrary.Models;

namespace LadeSkabClassLibrary.Fakes
{
    public class FakeRfidReader : IRfidReader
    {
        private RFIDState _oldState;

        public event EventHandler<RfidReaderChangedEventArgs> RfidReaderChangedEvent;

        public void SetTemp(RFIDState newRFIDState)
        {
            if (newRFIDState != _oldState)
            {
                OnRFIDChanged(new RfidReaderChangedEventArgs { RfidState = newRFIDState });
                _oldState = newRFIDState;
            }
        }

        protected virtual void OnRFIDChanged(RfidReaderChangedEventArgs e)
        {
            RfidReaderChangedEvent?.Invoke(this, e);
        }
    }

}
