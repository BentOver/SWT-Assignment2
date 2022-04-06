using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;
using NSubstitute.Exceptions;

namespace LadeSkabClassLibrary.Models
{
    public class ChargeControl: IChargeControl
    {
        private IUsbCharger _usbCharger;

        public IChargeControl.State State { get; set; }
        private IDisplay _display;
        public ChargeControl(IUsbCharger usbCharger, IDisplay display)
        {
            _usbCharger = usbCharger;
            _display = display;
            usbCharger.CurrentValueEvent += HandleCurrentChangedEvent;
        }

        public void StartCharge()
        {
            _usbCharger.StartCharge();
        }

        public void StopCharge()
        {
            _usbCharger.StopCharge();
        }

        public void HandleCurrentChangedEvent(object sender, CurrentEventArgs e)
        {
            double Current = e.Current;
            if (Current < 0)
            {
                throw new ArgumentException("Your phone is charging the system??");
            }
            switch (Current)
            {
                case 0:
                    //Do nothing
                    State = IChargeControl.State.NoConnection;
                    break;
                case double n when (n > 0 && n <= 5):
                    StopCharge();
                    _display.PrintDisplayInfo("Telefonen er fuldt opladt");
                    State = IChargeControl.State.FullyCharged;
                    break;
                case double n when (n > 5 && n <= 500):
                    _display.PrintDisplayInfo("Telefonen er ved at lade");
                    State = IChargeControl.State.Charging;
                    break;
                case double n when (n > 500):
                    StopCharge();
                    _display.PrintDisplayInfo("Fejl 744: Ladning stopppet grundet fejl");
                    State = IChargeControl.State.ShortCircuit;
                    break;
            }
        }

        private bool connected;
        public bool Connected
        {
            get
            {
                connected = _usbCharger.Connected;
                return connected;
            }
            set { connected = _usbCharger.Connected; }
            
        }

    }
}
