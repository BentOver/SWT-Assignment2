using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Controls;
using LadeSkabClassLibrary.Events;
using LadeSkabClassLibrary.Interfaces;
using LadeSkabClassLibrary.Models;

namespace LadeSkabClassLibrary
{
    public class StationControl
    {
        public enum LadeskabState
        {
            DoorOpen,
            Available,
            Locked
        };


        private LadeskabState _state { get; set; }
        private IChargeControl _charger;
        private int _oldId;
        private DoorState _doorState;
        private IDoor _door;
        private IDisplay _display;

        private string logFile = "logfile.txt"; // Navnet på systemets log-fil

        public StationControl(IDoor door, IRfidReader rfidReader, IChargeControl chargeControl, IDisplay display)
        {
            door.DoorChangedEvent += HandleDoorChangedEvent;
            rfidReader.RfidReaderChangedEvent += HandleRfidChangedEvent;
            _display = display;
            _charger = chargeControl;
            _door = door;
            
        }
        
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

                        _display.PrintDisplayInfo("Ladeskabet optaget");
                        _state = LadeskabState.Locked;
                    }
                    else
                    {
                        _display.PrintDisplayInfo("Tilslutningsfejl");
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

                        _display.PrintDisplayInfo("Fjern telefon");
                        _state = LadeskabState.Available;
                    }
                    else
                    {
                        _display.PrintDisplayInfo("Forkert RFID tag");
                    }

                    break;
            }
        }

        private void DoorStateChangedDetected(DoorState doorState)
        {
            switch (doorState)
            {
                case DoorState.Closed:
                    _display.PrintDisplayInfo("Indlæs RFID");
                    _state = LadeskabState.Available;
                    break;
                case DoorState.Opened:
                    _display.PrintDisplayInfo("Tilslut telefon");
                    _state = LadeskabState.DoorOpen;
                    break;
                case DoorState.Locked:
                    //
                    break;
            }
        }

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
