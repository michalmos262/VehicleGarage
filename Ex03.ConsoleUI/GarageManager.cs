using System;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.VehicleRecordInGarage;
using static Ex03.GarageLogic.VehicleFactory;
using System.Collections.Generic;
using static Ex03.ConsoleUI.InputValidator;
using System.Linq;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private Garage m_Garage = new Garage();
        private bool m_IsQuit = false;

        public enum eMenuOption
        {
            InsertNewVehicle = 1,
            PrintLicenseNumbers,
            ChangeVehicleStatus,
            InflateTiresToMax,
            RefuelVehicle,
            ChargeVehicle,
            PrintVehicleInformation
        }

        private static void printMenu()
        {
            Console.WriteLine(
                $@"
Please enter an option number (any other key to exit):
------------------------------------------------------------------------
({(int)eMenuOption.InsertNewVehicle}) Add a new vehicle to the garage
({(int)eMenuOption.PrintLicenseNumbers}) Print the license numbers of the vehicles in the garage
({(int)eMenuOption.ChangeVehicleStatus}) Change a vehicle status
({(int)eMenuOption.InflateTiresToMax}) Inflate a vehicle tires to maximum
({(int)eMenuOption.RefuelVehicle}) Refuel a vehicle (for fuel vehicles only)
({(int)eMenuOption.ChargeVehicle}) Charge a vehicle (for battery vehicles only)
({(int)eMenuOption.PrintVehicleInformation}) Print a vehicle information");
        }

        private void printVehicleTypes()
        {
            Console.WriteLine("Choose a vehicle type option:");
            for (int i = 0; i < sr_VehicleTypeNames.Length; i++)
            {
                Console.WriteLine("({0}) {1}", i + 1, sr_VehicleTypeNames[i]);
            }
        }

        private static void getVehicleTypeDetails(Vehicle io_vehicle)
        {
            List<string> vehicleDetails;
            List<string> userInputsList = new List<string>();
            string userInput;

            vehicleDetails = io_vehicle.RequestAdditionalVehicleDetails();
            foreach (string vehicleDetail in vehicleDetails)
            {
                Console.WriteLine(vehicleDetail);
                userInput = Console.ReadLine();
                userInputsList.Add(userInput);
            }

            io_vehicle.setVehicleDetails(userInputsList);
        }

        private int getMaxValueInEnum<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        private void getEnergyResourceDetails(EnergyTank io_Egnine)
        {
            bool isValidInput = false;

            Console.WriteLine("Enter the current amount of energy of the new engine:");
            while (!isValidInput)
            {
                try
                {
                    string userInput = Console.ReadLine();
                    io_Egnine.CurrentEnergyAmount = TryParseFloat(userInput);
                    isValidInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private bool isSetAllTires()
        {
            bool chosenOption = false;
            string userInput;

            Console.WriteLine("Do you want to set all tires once? Enter 'yes' or any other key for no:");
            userInput = Console.ReadLine();
            if (userInput == "yes")
            {
                chosenOption = true;
            }

            return chosenOption;
        }

        private static void getTireDetails(Tire io_Tire)
        {
            bool isValidInput = false;
            Console.WriteLine("Enter the manufacturer Name:");
            io_Tire.ManufacturerName = Console.ReadLine();
            while (!isValidInput)
            {
                try
                {
                    Console.WriteLine("Enter the current Air Pressure:");
                    string userInput = Console.ReadLine();
                    io_Tire.CurrentAirPressure = TryParseFloat(userInput);
                    isValidInput = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
        }

        private void getTiresDetails(List<Tire> io_Tires)
        {
            int i = 1;
            bool isAllTiresTheSame = isSetAllTires();
            if (isAllTiresTheSame)
            {
                getTireDetails(io_Tires[0]);
                foreach (Tire tire in io_Tires)
                {
                    tire.CurrentAirPressure = io_Tires[0].CurrentAirPressure;
                    tire.ManufacturerName = io_Tires[0].ManufacturerName;
                }
            }
            else
            {
                foreach (Tire tire in io_Tires)
                {
                    Console.WriteLine("Enter tire {0} details:", i);
                    getTireDetails(tire);
                    i++;
                }
            }
        }

        private Vehicle getVehicleFromUser(string i_LicenseNumber)
        {
            printVehicleTypes();
            string vehicleTypeChoice = Console.ReadLine();
            eVehicleType vehicleType = (eVehicleType)ParseEnumOption(vehicleTypeChoice, getMaxValueInEnum<eVehicleType>());
            Vehicle vehicle = MakeNewVehicle(vehicleType, i_LicenseNumber);
            Console.WriteLine("Enter model Name:");
            vehicle.ModelName = Console.ReadLine();
            getVehicleTypeDetails(vehicle);
            getEnergyResourceDetails(vehicle.Engine);
            getTiresDetails(vehicle.Tires);
            return vehicle;
        }

        private void addNewVehicleToGarage(string i_LicenseNumber)
        {
            try
            {
                string ownerName, ownerPhoneNumber;
                Vehicle vehicle;

                Console.WriteLine("Enter owner Name:");
                ownerName = Console.ReadLine();
                Console.WriteLine("Enter owner phone number:");
                ownerPhoneNumber = Console.ReadLine();
                vehicle = getVehicleFromUser(i_LicenseNumber);
                m_Garage.AddNewVehicleToGarage(vehicle, ownerName, ownerPhoneNumber);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private string getLicenseNumberFromUser()
        {
            Console.WriteLine("Enter license number:");
            string licenseNumber = Console.ReadLine();

            return licenseNumber;
        }

        private void insertNewVehicle()
        {
            string licenseNumber = getLicenseNumberFromUser();

            if (m_Garage.IsVehicleInGarage(licenseNumber))
            {
                Console.WriteLine($"Vehicle is already in the garage, moving its status to: {eVehicleStatus.InRepair}");
                m_Garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
            }
            else
            {
                addNewVehicleToGarage(licenseNumber);
            }
        }

        private void makeAction(eMenuOption i_MenuOption)
        {
            switch (i_MenuOption)
            {
                case eMenuOption.InsertNewVehicle:
                    insertNewVehicle();
                    break;
                // TODO: continue menu
                default:
                    m_IsQuit = true;
                    break;
            }
        }

        public void StartConsoleInteraction()
        {
            Console.WriteLine("Welcome to the garage!");
            try
            {
                while (!m_IsQuit)
                {
                    printMenu();
                    string userInput = Console.ReadLine();
                    eMenuOption chosenMenuOption = (eMenuOption)ParseEnumOption(userInput, getMaxValueInEnum<eMenuOption>());
                    makeAction(chosenMenuOption);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Goodbye!");
            }
        }
    }
}
