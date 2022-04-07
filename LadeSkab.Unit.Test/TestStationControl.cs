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

            _uut = new StationControl(_door, _rfidReader, _chargeControl, _display);
            
        }

        [TestCase(12)]
        public void DoorIsSetToOpenDoesNotLockAfterRFIDChanged(int id)
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs {DoorState = DoorState.Opened});
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs {RfidRead = id});

            _door.DidNotReceive().LockDoor();
        }


        [TestCase(1)]
        public void RfidDetectedStateClosedChargeControlConnectedResultIsLocked(int id)
        {
            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _door.Received().LockDoor();
        }

        [TestCase(1)]
        public void RfidDetectedStateClosedChargeControlNotConnectedResultIsClosed(int id)
        {
            _chargeControl.Connected = false;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _door.DidNotReceive().LockDoor();
        }

        [TestCase(1)]
        public void RfidDetectedStateDoorOpenNothingHappened(int id)
        {
            
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Opened });
          
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = id });

            _display.ReceivedWithAnyArgs(1).PrintDisplayInfo(default);
        }


        [TestCase(1,1)]
        public void RfidDetectedIsTheSameLockedStateResultIsClosed(int oldRfid, int newRfid)
        {
            

            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = oldRfid });

            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = newRfid });
            
            _door.Received().UnlockDoor();

        }

        [TestCase(1, 2)]
        public void RfidDetectedIsNotTheSameLockedStateResultIsLocked(int oldRfid, int newRfid)
        {

            _chargeControl.Connected = true;
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = oldRfid });

            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs { RfidRead = newRfid });


            _door.DidNotReceive().UnlockDoor();
        }

        [Test]
        public void DoorIsSetToClosedDisplayMethodIsCalled()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });

            _display.ReceivedWithAnyArgs().PrintDisplayInfo(default);
        }

    }
}