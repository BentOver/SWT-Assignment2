using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;

namespace LadeSkabClassLibrary.Models
{
    public class ChargeControl: IChargeControl
    {
        public event EventHandler<CurrentEventArgs> ChargeControlEvent;
        private IUsbCharger _usbCharger;

        public ChargeControl(IUsbCharger usbCharger)
        {
            _usbCharger = usbCharger;
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

            switch (Current)
            {
                case 0:
                    //Do nothing
                    break;
                case double n when (n > 0 && n <= 5):
                    StopCharge();
                    Console.WriteLine("Telefonen er fuldt opladt");
                    break;
                case double n when (n > 5 && n <= 500):
                    Console.WriteLine("Telefonen er ved at lade");
                    break;
                case double n when (n > 500):
                    StopCharge();
                    Console.WriteLine("Fejl 744: Ladning stopppet grundet fejl");
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
        }

    }
}
