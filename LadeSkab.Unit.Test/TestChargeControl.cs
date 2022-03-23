using System;
using System.IO;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Models;
using NSubstitute;
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

        //[TestCase(true, true, 0)]
        //[TestCase(true, false, 500)]
        //[TestCase(false, true, 0)]
        //[TestCase(false, false, 0)]
        //public void SimulateOverloadConectedTestEventCurrentResult(bool Connected, bool Overload, double Current)
        //{
        //    _usbChargerSimulator.SimulateConnected(Connected);
        //    _usbChargerSimulator.SimulateOverload(Overload);

        //    _usbChargerSimulator.StartCharge();


        //    Assert.That((_usbChargerSimulator.CurrentValue), Is.EqualTo(Current));
        //}


        [TestCase(600.0, IChargeControl.State.ShortCircuit)]
        [TestCase(340.0, IChargeControl.State.Charging)]
        [TestCase(3.0, IChargeControl.State.FullyCharged)]
        [TestCase(0.0, IChargeControl.State.NoConnection)]
        public void SimulateOverloadConectedTestEventConsoleOutput(double current, IChargeControl.State ResultState)
        {
            //_usbChargerSimulator.SimulateConnected(Connected);
            //_usbChargerSimulator.SimulateOverload(Overload);
            _usbChargerSimulator.CurrentValueEvent += Raise.EventWith(new CurrentEventArgs{Current = current});

           
                
                Assert.AreEqual( ResultState,_uut.State);

        }

        //[Test]
        //public void SimulateChargingUntillFullyChargedTestConsoleOuput()
        //{
        //    _usbChargerSimulator.SimulateConnected(true);
        //    _usbChargerSimulator.SimulateOverload(false);

        //    using (StringWriter sw = new StringWriter())
        //    {
        //        Console.SetOut(sw);

        //        _usbChargerSimulator.StartCharge();
        //        string output;
        //        string[] ListOutput = new []{""};


        //        while (_usbChargerSimulator.CurrentValue > 5)
        //        {
        //        }
        //        long milliseconds = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        //        while (DateTimeOffset.Now.ToUnixTimeMilliseconds() < milliseconds + 1000)
        //        {

        //        if (!string.IsNullOrEmpty(sw.ToString()))
        //        {
        //            output = sw.ToString();
        //            ListOutput = output.Split(Environment.NewLine);

        //            break;
        //        }

        //        }
        //        _usbChargerSimulator.StopCharge();
        //        sw.Close();

        //        Assert.AreEqual("Telefonen er ved at lade", ListOutput[0].Replace(Environment.NewLine, ""));

        //    }

        //    Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        //}

    }
}