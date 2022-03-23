using System;

namespace LadeSkabClassLibrary
{
    public class CurrentEventArgs : EventArgs
    {
        // Value in mA (milliAmpere)
        public double Current { set; get; }
    }

    public interface IUsbCharger
    {
        // Event triggered on new current value
        event EventHandler<CurrentEventArgs> CurrentValueEvent;

        // Direct access to the current current value
        double CurrentValue { get; }

        // Require connection status of the phone
        bool Connected { get; }

        public void SimulateConnected(bool connected);

        public void SimulateOverload(bool overload);


        // Start charging
        void StartCharge();
        // Stop charging
        void StopCharge();
    }
}