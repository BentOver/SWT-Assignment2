using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;

namespace LadeSkabClassLibrary
{
    public class StationControl
    {
        // Enum med tilstande ("states") svarende til tilstandsdiagrammet for klassen
        public enum LadeskabState
        {
            DoorOpen,
            Available,
            Locked
        };


        // Her mangler flere member variable
        public LadeskabState _state { get; set; }
        private IChargeControl _charger;
        private int _oldId;
        private DoorState _doorState;
        private IDoor _door;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IRfidReader rfidReader, IChargeControl chargeControl)
        {
            door.DoorChangedEvent += HandleDoorChangedEvent;
            rfidReader.RfidReaderChangedEvent += HandleRfidChangedEvent;

            _charger = chargeControl;
            _door = door;
        }

        // Eksempel på event handler for eventet "RFID Detected" fra tilstandsdiagrammet for klassen
        private void RfidDetected(int id)
        {
            switch (_state)
            {
                case LadeskabState.Available:
                    // Check for ladeforbindelse
                    if (_charger.Connected)
                    {
                        _door.LockDoor();
                        _charger.StartCharge();
                        _oldId = id;
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst med RFID: {0}", id);
                        }

                        Console.WriteLine("Ladeskabet optaget");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        Console.WriteLine("Tilslutningsfejl");
                    }

                    break;

                case LadeskabState.DoorOpen:
                    // Ignore
                    break;

                case LadeskabState.Locked:
                    // Check for correct ID
                    if (id == _oldId)
                    {
                        _charger.StopCharge();
                        _door.UnlockDoor();
                        using (var writer = File.AppendText(logFile))
                        {
                            writer.WriteLine(DateTime.Now + ": Skab låst op med RFID: {0}", id);
                        }

                        Console.WriteLine("Fjern telefon");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        Console.WriteLine("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void DoorStateChangedDetected(DoorState doorState)
        {
            switch (doorState)
            {
                case DoorState.Closed:
                    Console.WriteLine("Indlæs RFID");
                    _state = LadeskabState.Available;
                    break;
                case DoorState.Opened:
                    Console.WriteLine("Tilslut telefon");
                    _state = LadeskabState.DoorOpen;
                    break;
                case DoorState.Locked:
                    //
                    break;
            }
        }

        // Her mangler de andre trigger handlere
        private void HandleDoorChangedEvent(object sender, DoorChangedEventArgs e)
        {
            DoorStateChangedDetected(e.DoorState);
        }

        private void HandleRfidChangedEvent(object sender, RfidReaderChangedEventArgs e)
        {
            RfidDetected(e.RfidRead);
        }

    }
}
