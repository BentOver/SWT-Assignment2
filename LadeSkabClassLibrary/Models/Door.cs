using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;


namespace LadeSkabClassLibrary.Models
{
    public class Door: IDoor
    {
        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;
    }
}
