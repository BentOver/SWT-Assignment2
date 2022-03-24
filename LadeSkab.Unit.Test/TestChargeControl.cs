using System;
using System.IO;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Models;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private IUsbCharger _usbChargerSimulator;

        [SetUp]
        public void Setup()
        {

            _usbChargerSimulator = Substitute.For<IUsbCharger>();
            _uut = new ChargeControl(_usbChargerSimulator);
        }


        [TestCase(int.MaxValue, IChargeControl.State.ShortCircuit)]
        [TestCase(500.1, IChargeControl.State.ShortCircuit)]
        [TestCase(500, IChargeControl.State.Charging)]
        [TestCase(499.9, IChargeControl.State.Charging)]
        [TestCase(5.1, IChargeControl.State.Charging)]
        [TestCase(5, IChargeControl.State.FullyCharged)]
        [TestCase(4.9, IChargeControl.State.FullyCharged)]
        [TestCase(0.1, IChargeControl.State.FullyCharged)]
        [TestCase(0, IChargeControl.State.NoConnection)]
        public void SimulateOverloadConectedTestEventConsoleOutput(double current, IChargeControl.State ResultState)
        {
            _usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs {Current = current});
            Assert.AreEqual(ResultState, _uut.State);

        }


        [TestCase(int.MaxValue, false)]
        [TestCase(0, false)]
        [TestCase(-0, false)]
        [TestCase(-0.1, true)]
        [TestCase(-int.MaxValue, true)]
        public void CurrentValueIsExceptionThrown(double current, bool expectThrow)
        {
            if (expectThrow)
            {
                Assert.Throws<ArgumentException>((() =>
                    _usbChargerSimulator.CurrentValueEvent +=
                        Raise.EventWith(new CurrentEventArgs {Current = current})));
            }
            else
            {
                Assert.DoesNotThrow((() =>
                    _usbChargerSimulator.CurrentValueEvent +=
                        Raise.EventWith(new CurrentEventArgs { Current = current })));
            }
        }

        [TestCase(true)]
        [TestCase(false)]
        public void ConnectedValue_USBChargerIsConnected_ValueIsTrue(bool connectValue)
        {
            _usbChargerSimulator.Connected.Returns(connectValue);

            Assert.That(_uut.Connected, Is.EqualTo(connectValue));
        }

        [Test]
        public void StartCharge_()
        {
            _uut.StartCharge();
            _usbChargerSimulator.Received().StartCharge();
        }

    }
}