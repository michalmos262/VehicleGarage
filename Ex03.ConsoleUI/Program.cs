using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ex03;

namespace Ex03.ConsoleUI
{
    internal class Program
    {
        static void Main()
        {
            GarageManager garageManager = new GarageManager();
            garageManager.StartConsoleInteraction();
        }
    }
}