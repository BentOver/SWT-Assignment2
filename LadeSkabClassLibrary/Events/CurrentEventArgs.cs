using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LadeSkabClassLibrary.Events
{
    public class CurrentEventArgs : EventArgs
    {
        public double Current { get; set; }
    }
}
