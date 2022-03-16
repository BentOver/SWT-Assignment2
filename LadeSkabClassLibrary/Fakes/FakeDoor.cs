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
    public class FakeDoor : IDoor
    {
        private DoorState _oldState;

        public event EventHandler<DoorChangedEventArgs> DoorChangedEvent;

        public void SetDoorState(DoorState newDoorState)
        {
            if (newDoorState != _oldState)
            {
                OnDoorChanged(new DoorChangedEventArgs { DoorState = newDoorState });
                _oldState = newDoorState;
            }
        }

        public void LockDoor()
        {
            SetDoorState(DoorState.Locked);
        }

        public void UnlockDoor()
        {
            SetDoorState(DoorState.Closed);
        }

        protected virtual void OnDoorChanged(DoorChangedEventArgs e)
        {
            DoorChangedEvent?.Invoke(this, e);
        }
    }

}
