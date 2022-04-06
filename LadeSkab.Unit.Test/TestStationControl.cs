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
        private Door _fakeDoor;
        private StationControl _uut;
        private RfidReader _fakeRfidReader;
        private IChargeControl _chargeControl;
        private IDisplay _display;

        [SetUp]
        public void Setup()
        {
            _chargeControl = Substitute.For<IChargeControl>();
            _display = Substitute.For<IDisplay>();
            _fakeDoor = new Door();
            _fakeDoor.SetDoorState(DoorState.Closed);
            _fakeRfidReader = new RfidReader();

            _uut = new StationControl(_fakeDoor, _fakeRfidReader, _chargeControl, _display);
            
        }

        [Test]
        public void DoorIsSetToOpenDoesNotLockAfterRFIDChanged()
        {
            _fakeDoor.TryOpenDoor();
            _fakeRfidReader.SetRFIDState(12);
            Assert.That(_uut._state, Is.Not.EqualTo(StationControl.LadeskabState.Locked));

        }


        [TestCase(1,StationControl.LadeskabState.Available, StationControl.LadeskabState.Locked)]
        public void RfidDetectedStateClosedResultIsLocked(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _uut._state = state;
            _chargeControl.Connected = true;
            _fakeRfidReader.SetRFIDState(id);
            //RfidDetected changes state
            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1,StationControl.LadeskabState.Available, StationControl.LadeskabState.Available)]
        public void RfidDetectedStateClosedResultIsClosed(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _chargeControl.Connected = false;
            _uut._state = state;
            _fakeRfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1, StationControl.LadeskabState.DoorOpen, StationControl.LadeskabState.DoorOpen)]
        public void RfidDetectedStateDoorOpen(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _uut._state = state;
            _fakeRfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));

        }


        [TestCase(1,1, StationControl.LadeskabState.Locked, StationControl.LadeskabState.Available)]
        public void RfidDetectedLockedStateResultIsClosed(int oldRfid, int newRfid, StationControl.LadeskabState state,
            StationControl.LadeskabState expectedState)
        {
            _uut._state = StationControl.LadeskabState.Available;
            _chargeControl.Connected = true;
            _fakeRfidReader.SetRFIDState(oldRfid);
            _uut._state = state;
            _fakeRfidReader.SetRFIDState(newRfid);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

        [TestCase(1, StationControl.LadeskabState.Locked, StationControl.LadeskabState.Locked)]
        public void RfidDetectedLockedStateResultIsLocked(int id, StationControl.LadeskabState state,
            StationControl.LadeskabState expectedState)
        {

            _uut._state = state;
            _fakeRfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));
        }

    }
}