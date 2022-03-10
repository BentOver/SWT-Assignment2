using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkabClassLibrary.Controls
{
    internal class IChargeControl
    {
        event EventHandler<ChargeControlEventArgs> ChargeControlEvent;
    }
}
