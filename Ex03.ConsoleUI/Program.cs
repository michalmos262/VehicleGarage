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
            GarageLogic.Tire tire1 = new GarageLogic.Tire("test man", 31);
            GarageLogic.Tire tire2 = new GarageLogic.Tire("test man", 31);
            GarageLogic.Tire tire3 = new GarageLogic.Tire("test man", 31);
            GarageLogic.Tire tire4 = new GarageLogic.Tire("test man", 31);
            List<GarageLogic.Tire>  tiresList = new List<GarageLogic.Tire>(4);
            tiresList.Add(tire1);
            tiresList.Add(tire2);
            tiresList.Add(tire3); 
            tiresList.Add(tire4);

            GarageLogic.Car car = new GarageLogic.Car("toyota", tiresList, 4, GarageLogic.Car.eColor.Red);
            Console.WriteLine("Main Method");
        }
    }
}
