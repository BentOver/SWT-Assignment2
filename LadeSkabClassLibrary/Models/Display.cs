using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LadeSkabClassLibrary.Interfaces;

namespace LadeSkabClassLibrary.Models
{
    public class Display: IDisplay
    {
        public void PrintDisplayInfo(string s)
        {
            Console.WriteLine(s);
        }
    }
}
