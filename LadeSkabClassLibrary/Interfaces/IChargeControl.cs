using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Events;

namespace LadeSkabClassLibrary.Controls
{
    public interface IChargeControl
    {
        public enum State
        {
            Charging,
            ShortCircuit,
            NoConnection,
            FullyCharged,
        }

        void StartCharge();
        void StopCharge();

        public bool Connected { get; set; }
    }
}
