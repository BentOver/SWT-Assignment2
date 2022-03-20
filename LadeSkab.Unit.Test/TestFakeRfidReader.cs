using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Fakes;
using NuGet.Frameworks;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{

    [TestFixture]
    public class TestFakeRfidReader
    {
        private FakeRfidReader _uut;
        private RfidReaderChangedEventArgs _receivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _receivedEventArgs = null;
            _uut = new FakeRfidReader();

            _uut.RfidReaderChangedEvent += (o, args) => _receivedEventArgs = args;
        }

        ////Jesper: Jeg tror ikke at vi skal teste dette for FakeRFIDReader, men nærmere for StationControl, fordi jeg mener det er StationControl logik.
        /// 
        //[TestCase(1,1,false)]
        //[TestCase(2,5,true)]
        //public void SetRfidDifferentStatesIsTrueWhenOldAndNewRfidIsDifferent(int newRFID, int oldRFID,bool expectedResult)
        //{
        //    var wasCalled = false;
        //    _uut.SetRFIDState(oldRFID); // set _oldRFID property
        //    _uut.RfidReaderChangedEvent += (o, args) => wasCalled = true;
        //    _uut.SetRFIDState(newRFID); //what is compared to _oldRFID
        //    Assert.That(wasCalled, Is.EqualTo(expectedResult));
        //}

        //Test at event bliver afsendt når metode SetRFIDState bliver kaldt
        [TestCase(1)]
        [TestCase(-2)]
        public void SetRFIDState_RfidValueChanged_EventFired(int rfidValue)
        { 
            _uut.SetRFIDState(rfidValue); // set _oldRFID property

            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        //Test at ny værdi bliver sat når metode SetRFIDState bliver kaldt
        [TestCase(1,5)]
        [TestCase(2,2)]
        [TestCase(-1,13)]
        public void SetRFIDState_RfidValueChanged_CorrectNewRfidValueChanged(int newRFID, int oldRFID)
        {
            _uut.SetRFIDState(oldRFID); // set _oldRFID property
            _uut.SetRFIDState(newRFID); //what is compared to _oldRFID

            Assert.That(_receivedEventArgs.RfidRead, Is.EqualTo(newRFID));
        }


    }
}
