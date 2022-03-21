using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkabClassLibrary.Events
{
    public enum DoorState
    {
        Closed,
        Opened,
        Locked
    }
    public class DoorChangedEventArgs : EventArgs
    {
        public DoorState DoorState { get; set; }
    }

}
