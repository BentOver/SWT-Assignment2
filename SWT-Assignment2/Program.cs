using System.Diagnostics;
using LadeSkabClassLibrary;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Fakes;
using LadeSkabClassLibrary.Models;

class Program
{
    static void Main(string[] args)
    {
        FakeDoor _fakeDoor;
        StationControl _uut;
        FakeRfidReader _fakeRfidReader;
        ChargeControl _chargeControl;
        UsbChargerSimulator _usbCharger;

        
        _usbCharger = new UsbChargerSimulator();
        _chargeControl = new ChargeControl(_usbCharger);
        _fakeDoor = new FakeDoor();
        _fakeRfidReader = new FakeRfidReader();
        _uut = new StationControl(_fakeDoor, _fakeRfidReader, _chargeControl);


        // Assemble your system here from all the classes

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
                    _fakeDoor.SetDoorState(DoorState.Opened);
                    break;

                case 'C':
                    _fakeDoor.SetDoorState(DoorState.Closed);
                    break;

                case 'R':
                    System.Console.WriteLine("Indtast RFID id: ");
                    string idString = System.Console.ReadLine();

                    int id = Convert.ToInt32(idString);
                    _fakeRfidReader.SetRFIDState(id);
                    break;

                default:
                    break;
            }

            } while (!finish);
    }
}


