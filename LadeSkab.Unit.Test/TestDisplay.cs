using System;
using System.IO;
using System.Threading;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Models;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace LadeSkab.Unit.Test
{
    [TestFixture]
    public class TestDisplay
    {
        private Display _uut;

        [SetUp]
        public void Setup()
        {
            _uut = new Display();
        }

        [TestCase("Is this sent to Display?")]
        [TestCase("")]
        [TestCase("What about this?!.-*")]
        public void StringSentToDisplayIsEqualToOriginalString(string s)
        {
            string consoleoutput;
            using (StringWriter stringWriter = new StringWriter())
            {
                Console.SetOut(stringWriter);
                _uut.PrintDisplayInfo(s);
                Thread.Sleep(50);
                consoleoutput = stringWriter.ToString().Replace(Environment.NewLine, "");

            }

            Assert.That(consoleoutput, Is.EqualTo(s));
        }

    }
}