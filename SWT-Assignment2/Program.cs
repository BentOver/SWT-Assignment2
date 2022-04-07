using System.Diagnostics;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Models;

class Program
{
    static void Main(string[] args)
    {
        Door _fakeDoor;
        StationControl _uut;
        RfidReader _fakeRfidReader;
        ChargeControl _chargeControl;
        UsbChargerSimulator _usbCharger;
        Display _display;

        _display = new Display();
        _usbCharger = new UsbChargerSimulator();
        _chargeControl = new ChargeControl(_usbCharger, _display);
        _fakeDoor = new Door();
        _fakeRfidReader = new RfidReader();
        _uut = new StationControl(_fakeDoor, _fakeRfidReader, _chargeControl, _display);


        bool finish = false;
            do
            {
                string input;
                System.Console.WriteLine("Indtast E, O, C, R: ");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input)) continue;

            switch (input[0]) 
            {
                case 'E':
                    finish = true;
                    break;

                case 'O':
                    _fakeDoor.TryOpenDoor();
                    break;

                case 'C':
                    _fakeDoor.TryCloseDoor();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();
                    int id;
                    if (int.TryParse(idString,out id))
                        _fakeRfidReader.SetRFIDState(id);
                    break;

                default:
                    break;
            }

            } while (!finish);
    }
}


