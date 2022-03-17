using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Fakes;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{

    [TestFixture]
    public class TestFakeRfidReader
    {
        private FakeRfidReader _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new FakeRfidReader();
        }

        [TestCase(1,1,false)]
        [TestCase(2,5,true)]
        public void SetRfidDifferentStatesIsTrueWhenOldAndNewRfidIsDifferent(int newRFID, int oldRFID,bool expectedResult)
        {
            var WasCalled = false;
            _uut.SetRFIDState(oldRFID); // set _oldRFID property
            _uut.RfidReaderChangedEvent += ( o,args) => WasCalled = true;
            
            _uut.SetRFIDState(newRFID); //what is compared to _oldRFID

            Assert.That(WasCalled, Is.EqualTo(expectedResult));
        }


    }
}
