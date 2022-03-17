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

        [SetUp]
        public void Setup()
        {
            _chargeControl = new ChargeControl(new UsbChargerSimulator());
            _uut = new StationControl(_fakeDoor, _fakeRfidReader, _chargeControl);
        }

        [Test]
        public void DoorIsLocked()
        {

        }
        



    }
}