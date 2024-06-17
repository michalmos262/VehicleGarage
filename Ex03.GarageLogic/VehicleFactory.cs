using Ex03.GrarageLogic;

namespace Ex03.GarageLogic
{
    public class VehicleFactory
    {
        private readonly string[] r_VehicleTypeNames = { "Fuel car", "Electric Car", "Fuel Motorcycle", "Electric Motorcycle", "Truck" };

        public string[] VehicleTypeNames
        {
            get
            {
                return r_VehicleTypeNames;
            }
        }

        public enum eVehicleType
        {
            FuelCar = 1,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public Vehicle MakeNewVehicle(eVehicleType i_VehicleType, string i_LicenseNumber)
        {
            Vehicle newVehicle;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                    newVehicle = new Car(i_LicenseNumber);
                    newVehicle.Engine = new FuelTank(newVehicle.MaxFuelAmount, newVehicle.FuelType);
                    break;
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(i_LicenseNumber);
                    newVehicle.Engine = new Battery(newVehicle.MaxTimeBatteryCanLastInHours);
                    break;
                case eVehicleType.FuelMotorcycle:
                    newVehicle = new Motorcycle(i_LicenseNumber);
                    newVehicle.Engine = new FuelTank(newVehicle.MaxFuelAmount, newVehicle.FuelType);
                    break;
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(i_LicenseNumber);
                    newVehicle.Engine = new Battery(newVehicle.MaxTimeBatteryCanLastInHours);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_LicenseNumber);
                    newVehicle.Engine = new FuelTank(newVehicle.MaxFuelAmount, newVehicle.FuelType);
                    break;
                default:
                    throw new ValueOutOfRangeException(0, r_VehicleTypeNames.Length);
            }

            return newVehicle;
        }
    }
}