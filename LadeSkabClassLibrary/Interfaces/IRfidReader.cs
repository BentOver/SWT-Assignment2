﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;

namespace LadeSkabClassLibrary.Interfaces
{
    public interface IRfidReader
    {
        event EventHandler<RfidReaderChangedEventArgs> RfidReaderChangedEvent;
    }

}
