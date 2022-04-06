﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;
using LadeSkabClassLibrary.Models;

namespace LadeSkabClassLibrary.Models
{
    public class RfidReader : IRfidReader
    {
        private int _oldRfid;

        public event EventHandler<RfidReaderChangedEventArgs> RfidReaderChangedEvent;

        public void SetRFIDState(int newRFID)
        {
            OnRFIDChanged(new RfidReaderChangedEventArgs { RfidRead = newRFID });

            _oldRfid = newRFID; //bruges bla. til test af 
        }

        protected virtual void OnRFIDChanged(RfidReaderChangedEventArgs e)
        {
            RfidReaderChangedEvent?.Invoke(this, e);
        }
    }

}
