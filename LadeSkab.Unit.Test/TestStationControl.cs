using System;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;
using LadeSkabClassLibrary.Models;
using NSubstitute;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestStationControl
    {
        private StationControl _uut;
        private IDoor _door;
        private IRfidReader _rfidReader;
        private IChargeControl _chargeControl;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _door = Substitute.For<IDoor>();
            _rfidReader = Substitute.For<IRfidReader>();

            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs {DoorState = DoorState.Closed});

            _uut = new StationControl(_door, _rfidReader, _chargeControl, _display);
            
        }

        [Test]
        public void DoorIsSetToOpenDoesNotLockAfterRFIDChanged()
        {
            _door.TryOpenDoor();
            //_rfidReader.SetRFIDState(12);
            //_rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs {RfidRead = 12});
           // Assert.That(_uut._state, Is.Not.EqualTo(StationControl.LadeskabState.Locked));
        }


        [TestCase(1)]
        public void RfidDetectedStateClosedResultIsLocked(int id)
        {
            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _door.Received().LockDoor();
        }

        [TestCase(1)]
        public void RfidDetectedStateClosedResultIsClosed(int id)
        {
            _chargeControl.Connected = false;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _door.DidNotReceive().LockDoor();
        }

        [TestCase(1)]
        public void RfidDetectedStateDoorOpen(int id)
        {
            
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Opened });
          
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _display.ReceivedWithAnyArgs(1).PrintDisplayInfo(default);
        }


        [TestCase(1,1)]
        public void RfidDetectedLockedStateResultIsClosed(int oldRfid, int newRfid)
        {
            

            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = oldRfid });

            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Locked });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = newRfid });
            
            _door.Received().UnlockDoor();

        }

        [TestCase(1, 2)]
        public void RfidDetectedLockedStateResultIsLocked(int oldRfid, int newRfid)
        {

            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = oldRfid });

            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Locked });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = newRfid });


            _display.ReceivedWithAnyArgs(3).PrintDisplayInfo(default);
        }

        [Test]
        public void DoorIsSetToClosedDisplayMethodIsCalled()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });

            _display.ReceivedWithAnyArgs().PrintDisplayInfo(default);
        }

    }
}