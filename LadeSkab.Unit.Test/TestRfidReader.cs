using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Models;
using NuGet.Frameworks;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{

    [TestFixture]
    public class TestRfidReader
    {
        private RfidReader _uut;
        private RfidReaderChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new RfidReader();
        }

        [TestCase(1)]
        [TestCase(-2)]
        public void SetRFIDStateRfidValueChangedEventFired(int rfidValue)
        {
            _uut.RfidReaderChangedEvent += (o, args) => _receivedEventArgs = args;
            _uut.SetRFIDState(rfidValue);

            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(1)]
        [TestCase(-2)]
        public void SetRFIDStateNoListenEventFired(int rfidValue)
        {
            _uut.SetRFIDState(rfidValue);

            Assert.That(_receivedEventArgs, Is.Null);
        }

        [TestCase(1,5)]
        [TestCase(2,2)]
        [TestCase(-1,13)]
        public void SetRFIDStateRfidValueChangedCorrectNewRfidValueChanged(int newRFID, int oldRFID)
        {
            _uut.RfidReaderChangedEvent += (o, args) => _receivedEventArgs = args;
            _uut.SetRFIDState(oldRFID); // set _oldRFID property
            _uut.SetRFIDState(newRFID); //what is compared to _oldRFID

            Assert.That(_receivedEventArgs.RfidRead, Is.EqualTo(newRFID));
        }


    }
}
