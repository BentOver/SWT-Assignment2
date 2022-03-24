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

        //public void LockDoor()
        //{
        //    //Hardware stuff
        //}

        //public void UnlockDoor()
        //{
        //    //Hardware stuff
        //}


        //public void TryOpenDoor()
        //{
        //    //Hardware stuff
        //}

        //public void TryCloseDoor()
        //{
        //    //Hardware stuff
        //}
    }
}
