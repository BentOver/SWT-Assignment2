using System.Diagnostics;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Models;

class Program
{
    static void Main(string[] args)
    {
        Door _door;
        StationControl _uut;
        RfidReader _RfidReader;
        ChargeControl _chargeControl;
        UsbChargerSimulator _usbCharger;
        Display _display;

        _display = new Display();
        _usbCharger = new UsbChargerSimulator();
        _chargeControl = new ChargeControl(_usbCharger, _display);
        _door = new Door();
        _RfidReader = new RfidReader();
        _uut = new StationControl(_door, _RfidReader, _chargeControl, _display);


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
                    _door.TryOpenDoor();
                    break;

                case 'C':
                    _door.TryCloseDoor();
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();
                    int id;
                    if (int.TryParse(idString,out id))
                        _RfidReader.SetRFIDState(id);
                    break;

                default:
                    break;
            }

            } while (!finish);
    }
}


