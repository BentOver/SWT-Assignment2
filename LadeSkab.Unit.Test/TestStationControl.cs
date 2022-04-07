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

        [Test]
        public void DoorIsSetToOpenDoesNotLockAfterRFIDChanged()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs {DoorState = DoorState.Opened});
            _rfidReader.RfidReaderChangedEvent += Raise.EventWith(new RfidReaderChangedEventArgs {RfidRead = 12});

            _door.DidNotReceive().LockDoor();
        }

        [TestCase(1,StationControl.LadeskabState.Available, StationControl.LadeskabState.Locked)]
        public void RfidDetectedStateClosedResultIsLocked(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _uut._state = state;
            _chargeControl.Connected = true;
            //_rfidReader.SetRFIDState(id);
            //RfidDetected changes state
            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1,StationControl.LadeskabState.Available, StationControl.LadeskabState.Available)]
        public void RfidDetectedStateClosedResultIsClosed(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _chargeControl.Connected = false;
            _uut._state = state;
            //_rfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1, StationControl.LadeskabState.DoorOpen, StationControl.LadeskabState.DoorOpen)]
        public void RfidDetectedStateDoorOpen(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _uut._state = state;
            //_rfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));

        }


        [TestCase(1,1, StationControl.LadeskabState.Locked, StationControl.LadeskabState.Available)]
        public void RfidDetectedLockedStateResultIsClosed(int oldRfid, int newRfid, StationControl.LadeskabState state,
            StationControl.LadeskabState expectedState)
        {
            _uut._state = StationControl.LadeskabState.Available;
            _chargeControl.Connected = true;
            //_rfidReader.SetRFIDState(oldRfid);
            _uut._state = state;
            //_rfidReader.SetRFIDState(newRfid);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1, StationControl.LadeskabState.Locked, StationControl.LadeskabState.Locked)]
        public void RfidDetectedLockedStateResultIsLocked(int id, StationControl.LadeskabState state,
            StationControl.LadeskabState expectedState)
        {

            _uut._state = state;
            //_rfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [Test]
        public void DoorIsSetToClosedDisplayMethodIsCalled()
        {
            _door.DoorChangedEvent += Raise.EventWith(new DoorChangedEventArgs { DoorState = DoorState.Closed });

            _display.ReceivedWithAnyArgs().PrintDisplayInfo(default);
        }

    }
}