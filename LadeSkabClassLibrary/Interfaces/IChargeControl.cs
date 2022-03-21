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
        void StartCharge();
        void StopCharge();

        bool Connected { get;}
    }
}
