using System;
using System.IO;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Models;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestChargeControl
    {
        private ChargeControl _uut;
        private UsbChargerSimulator _usbChargerSimulator;
        [SetUp]
        public void Setup()
        {
            _usbChargerSimulator = new UsbChargerSimulator();
            _uut = new ChargeControl(_usbChargerSimulator);
        }

        [TestCase(true, true, 0)]
        [TestCase(true, false, 500)]
        [TestCase(false, true, 0)]
        [TestCase(false, false, 0)]
        public void SimulateOverloadConectedTestEventCurrentResult(bool Connected, bool Overload, double Current)
        {
            _usbChargerSimulator.SimulateConnected(Connected);
            _usbChargerSimulator.SimulateOverload(Overload);

            _usbChargerSimulator.StartCharge();


            Assert.That((_usbChargerSimulator.CurrentValue), Is.EqualTo(Current));
        }


        [TestCase(true, true, 0, "Fejl 744: Ladning stopppet grundet fejl")]
        [TestCase(true, false, 500, "Telefonen er ved at lade")]
        [TestCase(false, true, 0, "Telefonen er fuldt opladt")]
        [TestCase(false, false, 0, "Telefonen er fuldt opladt")]
        public void SimulateOverloadConectedTestEventConsoleOutput(bool Connected, bool Overload, double Current, string consoleoutput)
        {
            _usbChargerSimulator.SimulateConnected(true);
            _usbChargerSimulator.SimulateOverload(false);

            _usbChargerSimulator.StartCharge();



            using (StringWriter sw = new StringWriter())
            {
                Console.SetOut(sw);

                _uut = new ChargeControl(_usbChargerSimulator);
                _usbChargerSimulator.StartCharge();

                string expected = string.Format(consoleoutput+"{0}", Environment.NewLine);
                //Assert.That((expected),Is.EqualTo(sw.ToString()));
                Assert.AreEqual(consoleoutput,sw.ToString());
            }

            Console.SetOut(new StreamWriter(Console.OpenStandardError()));
        }

        //using (StringWriter sw = new StringWriter())
        //{
        //    Console.SetOut(sw);

        //    ConsoleUser cu = new ConsoleUser();
        //    cu.DoWork();

        //    string expected = string.Format("Ploeh{0}", Environment.NewLine);
        //    Assert.AreEqual<string>(expected, sw.ToString());
        //}

    }
}