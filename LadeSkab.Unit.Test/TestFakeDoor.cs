using System;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Events;

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
        public void SetDoorStateEvent()
        {
            DoorState testState = DoorState.Closed;
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;
            _uut.SetDoorState(testState);

            Assert.That(testState, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void SetDoorStateToClosed()
        {
            DoorState testState = DoorState.Closed;
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;
            _uut.SetDoorState(testState);

            Assert.That(testState, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void SetDoorStateToOpened()
        {
            DoorState testState = DoorState.Opened;
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;
            _uut.SetDoorState(testState);

            Assert.That(testState, Is.EqualTo(DoorState.Opened));
        }

        [Test]
        public void SetDoorStateToLocked()
        {

            DoorState testState = DoorState.Closed;
            _uut.SetDoorState(testState);

            testState = DoorState.Locked;
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;
            _uut.SetDoorState(testState);

            Assert.That(testState, Is.EqualTo(DoorState.Locked));
        }


        [Test]
        public void ClosedDoorLock()
        {
            DoorState testState = DoorState.Closed;
            _uut.SetDoorState(testState);
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;

            _uut.LockDoor();

            Assert.That(testState, Is.EqualTo(DoorState.Locked));
        }

        [Test]
        public void ClosedDoorUnlock()
        {
            DoorState testState = DoorState.Closed;
            _uut.TryCloseDoor();
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;

            _uut.UnlockDoor();

            Assert.That(testState, Is.EqualTo(DoorState.Closed));
        }

        [Test]
        public void ClosedDoorOpen()
        {
            DoorState testState = DoorState.Closed;
            _uut.TryCloseDoor();
            _uut.DoorChangedEvent += (o, args) => testState = args.DoorState;
            _uut.TryOpenDoor();


            Assert.That(testState, Is.EqualTo(DoorState.Opened));
        }

        [Test]
        public void OpenedDoorLock()
        {
            _uut.TryOpenDoor();
            DoorState testState = DoorState.Opened;
            _uut.SetDoorState(testState);

            Assert.Throws<ArgumentException>(() => _uut.LockDoor());

        }

    }
}