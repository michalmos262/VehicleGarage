using System;
using Ex03.GarageLogic;
using static Ex03.GarageLogic.VehicleRecordInGarage;
using static Ex03.GarageLogic.VehicleFactory;
using System.Collections.Generic;
using static Ex03.ConsoleUI.InputValidator;
using System.Linq;
using static Ex03.GarageLogic.FuelTank;

namespace Ex03.ConsoleUI
{
    public class GarageManager
    {
        private Garage m_Garage = new Garage();
        private VehicleFactory m_VehicleFactory = new VehicleFactory();
        private bool m_IsQuit = false;

        public enum eMenuOption
        {
            InsertNewVehicle = 1,
            ShowLicenseNumbers,
            ChangeVehicleStatus,
            InflateTiresToMax,
            RefuelVehicle,
            ChargeVehicle,
            ShowVehicleInformation
        }

        private static void printMenu()
        {
            Console.WriteLine(
                $@"
Please enter an option number (or any other key to exit):
----------------------------------------------------------
({(int)eMenuOption.InsertNewVehicle}) Add a new vehicle to the garage
({(int)eMenuOption.ShowLicenseNumbers}) Show the license numbers of the vehicles in the garage
({(int)eMenuOption.ChangeVehicleStatus}) Change a vehicle status
({(int)eMenuOption.InflateTiresToMax}) Inflate a vehicle tires to maximum
({(int)eMenuOption.RefuelVehicle}) Refuel a vehicle (for fuel vehicles only)
({(int)eMenuOption.ChargeVehicle}) Charge a vehicle (for battery vehicles only)
({(int)eMenuOption.ShowVehicleInformation}) Show a vehicle information");
        }

        private void printVehicleTypes()
        {
            Console.WriteLine("Choose a vehicle type option:");
            for (int i = 0; i < m_VehicleFactory.VehicleTypeNames.Length; i++)
            {
                Console.WriteLine("({0}) {1}", i + 1, m_VehicleFactory.VehicleTypeNames[i]);
            }
        }

        private static void setVehicleTypeDetails(Vehicle io_vehicle)
        {
            List<string> vehicleDetails;
            List<string> userInputsList = new List<string>();
            string userInput;

            Console.WriteLine("Please enter the details below:");
            vehicleDetails = io_vehicle.RequestAdditionalVehicleDetails();
            foreach (string vehicleDetail in vehicleDetails)
            {
                Console.WriteLine(vehicleDetail);
                userInput = Console.ReadLine();
                userInputsList.Add(userInput);
            }

            io_vehicle.VerifyAndSetAllSpecificVehicleTypeDetails(userInputsList);
        }

        private int getMaxValueInEnum<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<int>().Max();
        }

        private void setEnergyResourceDetails(EnergyTank io_Egnine)
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
            Console.WriteLine("Do you want to set all tires once? Enter 'yes' or any other key for no:");
            string userInput = Console.ReadLine();
            bool isUserChoseToSetAllTires = userInput == "yes";

            return isUserChoseToSetAllTires;
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
                    Console.WriteLine("Enter the current air pressure:");
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

        private void setTiresDetails(List<Tire> io_Tires)
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
            eVehicleType vehicleType =
                (eVehicleType)ParseEnumOption(vehicleTypeChoice, getMaxValueInEnum<eVehicleType>());
            Vehicle vehicle = m_VehicleFactory.MakeNewVehicle(vehicleType, i_LicenseNumber);
            Console.WriteLine("Enter model Name:");
            vehicle.ModelName = Console.ReadLine();
            setVehicleTypeDetails(vehicle);
            setEnergyResourceDetails(vehicle.Engine);
            vehicle.Tires = m_Garage.MakeNewTiresForVehicle(vehicle);
            setTiresDetails(vehicle.Tires);
            return vehicle;
        }

        private void getNewVehicleFromUserAndAddToGarage(string i_LicenseNumber)
        {
            string ownerName, ownerPhoneNumber;
            Vehicle vehicle;
            bool isAllSet = false;

            while (!isAllSet)
            {
                try
                {
                    vehicle = getVehicleFromUser(i_LicenseNumber);
                    Console.WriteLine("Thank you for the vehicle details, please enter the owner name:");
                    ownerName = Console.ReadLine();
                    Console.WriteLine("Enter the owner phone number:");
                    ownerPhoneNumber = Console.ReadLine();
                    m_Garage.AddNewVehicleToGarage(vehicle, ownerName, ownerPhoneNumber);
                    
                    isAllSet = true;
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                    Console.WriteLine("Please enter all the vehicle details again:\n");
                }
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
            try
            {
                string licenseNumber = getLicenseNumberFromUser();

                if (m_Garage.IsVehicleInGarage(licenseNumber))
                {
                    Console.WriteLine(
                        $"Vehicle is already in the garage, moving its status to: {eVehicleStatus.InRepair}");
                    m_Garage.ChangeVehicleStatus(licenseNumber, eVehicleStatus.InRepair);
                }
                else
                {
                    getNewVehicleFromUserAndAddToGarage(licenseNumber);
                    Console.WriteLine($"Vehicle was added successfully to the garage!");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private static void printEnum(Type i_TypeOfEnum, int i_EnumSize)
        {
            string enumKey;
            for (int i = 1; i <= i_EnumSize; i++)
            {
                enumKey = Enum.GetName(i_TypeOfEnum, i);
                Console.WriteLine("({0}) {1}", i, enumKey);
            }

        }

        private eVehicleStatus getVehicleStatus()
        {
            Console.WriteLine("Choose a vehicle status ");
            int maxValueInEnum = getMaxValueInEnum<eVehicleStatus>();
            printEnum(typeof(eVehicleStatus), maxValueInEnum);
            string userInput = Console.ReadLine();
            eVehicleStatus statusChoice = (eVehicleStatus)ParseEnumOption(userInput, maxValueInEnum);
            return statusChoice;
        }

        private void printLicenseNumbers(List<string> i_LicenseNumbers)
        {
            if (i_LicenseNumbers == null)
            {
                Console.WriteLine("No license numbers to show!");
            }
            else
            {
                Console.WriteLine("The license numbers:");
                int i = 1;
                foreach (string licenseNumber in i_LicenseNumbers)
                {
                    Console.WriteLine($"{i}. {licenseNumber}");
                    i++;
                }
            }
        }

        private void showLicenseNumbers()
        {
            List<string> licenseNumbers;
            bool isEmptyGarage = m_Garage.IsGarageEmpty();

            if (isEmptyGarage)
            {
                Console.WriteLine("Garage is empty! No license numbers to show");
            }
            else
            {
                Console.WriteLine(
                    "Do you want to see license numbers of a specific vehicle status? Enter 'yes' or press any key for no");
                string userInput = Console.ReadLine();
                bool isFilteredByStatus = userInput == "yes";
                if (isFilteredByStatus)
                {
                    eVehicleStatus statusChoice = getVehicleStatus();
                    licenseNumbers = m_Garage.GetLicenseNumbersList(statusChoice);
                }
                else
                {
                    licenseNumbers = m_Garage.GetLicenseNumbersList();
                }

                printLicenseNumbers(licenseNumbers);
            }
        }

        private void changeVehicleStatus()
        {
            string licenseNumber = getLicenseNumberFromUser();

            try
            {
                eVehicleStatus previousVehicleStatus = m_Garage.GetVehicleRecordByLicenseNumber(licenseNumber).VehicleStatus;
                Console.WriteLine("To which status to move?");
                int maxValueInEnum = getMaxValueInEnum<eVehicleStatus>();
                printEnum(typeof(eVehicleStatus), maxValueInEnum);
                string userInput = Console.ReadLine();
                try
                {
                    eVehicleStatus newVehicleStatus = (eVehicleStatus)ParseEnumOption(userInput, maxValueInEnum);
                    m_Garage.ChangeVehicleStatus(licenseNumber, newVehicleStatus);
                    Console.WriteLine($"Vehicle status was changed from {previousVehicleStatus} to {newVehicleStatus}!");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void inflateTiresToMax()
        {
            string licenseNumber = getLicenseNumberFromUser();

            try
            {
                m_Garage.InflateVehicleTiresToMaximumByLicenseNumber(licenseNumber);
                Console.WriteLine("Inflation of the vehicle tires to maximum air pressure succeeded!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void refuelVehicle()
        {
            string licenseNumber = getLicenseNumberFromUser();

            try
            {
                Console.WriteLine("Enter the amount of fuel to add to the vehicle:");
                string fuelAmountInput = Console.ReadLine();
                float fuelToAdd = TryParseFloat(fuelAmountInput);
                Console.WriteLine("Enter a fuel type option:");
                int maxValueInEnum = getMaxValueInEnum<eFuelType>();
                printEnum(typeof(eFuelType), maxValueInEnum);
                string optionChoiceInput = Console.ReadLine();
                eFuelType fuelType = (eFuelType)ParseEnumOption(optionChoiceInput, maxValueInEnum);
                m_Garage.ReFuelVehicle(licenseNumber, fuelType, fuelToAdd);
                Console.WriteLine("Refueling succeeded!");
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void chargeVehicle()
        {
            string licenseNumber = getLicenseNumberFromUser();

            try
            {
                Console.WriteLine("Enter the additional time amount for charging (in minutes):");
                string timeAmountInMinutesInput = Console.ReadLine();

                try
                {
                    float timeAmountInMinutes = TryParseFloat(timeAmountInMinutesInput);
                    float timeAmountInHours = timeAmountInMinutes / 60;
                    m_Garage.ReChargeVehicle(licenseNumber, timeAmountInHours);
                    Console.WriteLine("Charging succeeded!");
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception.Message);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void getVechileFromUserAndPrintHisInformation()
        {
            string licenseNumber = getLicenseNumberFromUser();
            Dictionary<string, string> vehicleDetailsDict;

            try
            {
                vehicleDetailsDict = m_Garage.GetAllVehicleInformation(licenseNumber);
                foreach (string carAttribute in vehicleDetailsDict.Keys)
                {
                    Console.WriteLine(string.Format("attribute = {0} | Info = {1}", carAttribute, vehicleDetailsDict[carAttribute]));
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        private void makeGarageAction(eMenuOption i_MenuOption)
        {
            switch (i_MenuOption)
            {
                case eMenuOption.InsertNewVehicle:
                    insertNewVehicle();
                    break;
                case eMenuOption.ShowLicenseNumbers:
                    showLicenseNumbers();
                    break;
                case eMenuOption.ChangeVehicleStatus:
                    changeVehicleStatus();
                    break;
                case eMenuOption.InflateTiresToMax:
                    inflateTiresToMax();
                    break;
                case eMenuOption.RefuelVehicle:
                    refuelVehicle();
                    break;
                case eMenuOption.ChargeVehicle:
                    chargeVehicle();
                    break;
                case eMenuOption.ShowVehicleInformation:
                    getVechileFromUserAndPrintHisInformation();
                    break;
                default:
                    m_IsQuit = true;
                    break;
            }
        }

        public void StartConsoleInteraction()
        { 
            setSomeVehicles();
            Console.WriteLine("Welcome to the garage!");
            try
            {
                while (!m_IsQuit)
                {
                    printMenu();
                    string userInput = Console.ReadLine();
                    eMenuOption chosenMenuOption = (eMenuOption)ParseEnumOption(userInput, getMaxValueInEnum<eMenuOption>());
                    makeGarageAction(chosenMenuOption);
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                Console.WriteLine("Goodbye!");
            }
        }
        private void setSomeVehicles() //TODO: DELETE THIS FUNCTION, was made for debug
        {
            Vehicle v = m_VehicleFactory.MakeNewVehicle((eVehicleType.FuelCar), "123");
            v.Tires = m_Garage.MakeNewTiresForVehicle(v);
            m_Garage.AddNewVehicleToGarage(v, "mic1", "123");

            v = m_VehicleFactory.MakeNewVehicle((eVehicleType.ElectricCar), "4");
            v.Tires = m_Garage.MakeNewTiresForVehicle(v);
            m_Garage.AddNewVehicleToGarage(v, "mic2", "456");

            v = m_VehicleFactory.MakeNewVehicle((eVehicleType.FuelCar), "1");
            v.Tires = m_Garage.MakeNewTiresForVehicle(v);
            m_Garage.AddNewVehicleToGarage(v, "mic3", "789");
        }
    }
}