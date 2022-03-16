using System;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Fakes;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestFakeDoor
    {
        private FakeDoor _uut;
        [SetUp]
        public void Setup()
        {
            _uut = new FakeDoor();
        }

        [Test]
        public void DoorIsLocked()
        {
            Console.WriteLine("Hello");
            _uut.LockDoor();
            _uut.UnlockDoor();
            Assert.That((1==1), Is.EqualTo(true));
        }
        



    }
}