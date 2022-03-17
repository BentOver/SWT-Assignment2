using System;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Models;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestStationControl
    {
        private FakeDoor _fakeDoor;
        private StationControl _uut;
        private FakeRfidReader _fakeRfidReader;
        private ChargeControl _chargeControl;
        private UsbChargerSimulator _usbCharger;
        [SetUp]
        public void Setup()
        {
            _usbCharger = new UsbChargerSimulator();
            _chargeControl = new ChargeControl(_usbCharger);
            _fakeDoor = new FakeDoor();
            _fakeRfidReader = new FakeRfidReader();
            _uut = new StationControl(_fakeDoor, _fakeRfidReader, _chargeControl);
            
        }

        [Test]
        public void DoorIsLocked()
        {
            
        }

      

        [TestCase(1,StationControl.LadeskabState.Closed, StationControl.LadeskabState.Locked)]
        
        //[TestCase(1,StationControl.LadeskabState.Locked, StationControl.LadeskabState.Closed)]
        public void RfidDetectedStateClosedResultIsLocked(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _uut._state = state;
            _fakeRfidReader.SetRFIDState(id);

            Assert.That(_uut._state, Is.EqualTo(expectedState));


            


        }

        [TestCase(1,StationControl.LadeskabState.Closed, StationControl.LadeskabState.Closed)]
        public void RfidDetectedStateClosedResultIsClosed(int id, StationControl.LadeskabState state, StationControl.LadeskabState expectedState)
        {
            _usbCharger.SimulateConnected(false);
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

        //_state er Closed => Locked
        //_state er DoorOpen => DoorOpen
        //_state er Locked => Closed

        [TestCase(1,1, StationControl.LadeskabState.Locked, StationControl.LadeskabState.Closed)]
        public void RfidDetectedLockedStateResultIsClosed(int oldRfid, int newRfid, StationControl.LadeskabState state,
            StationControl.LadeskabState expectedState)
        {
            _uut._state = StationControl.LadeskabState.Closed;
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