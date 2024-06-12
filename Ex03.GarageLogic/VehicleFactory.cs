
namespace Ex03.GarageLogic
{
    internal class VehicleFactory
    {
        public enum eVehicleType
        {
            FuelCar,
            ElectricCar,
            FuelMotorcycle,
            ElectricMotorcycle,
            Truck
        }

        public Vehicle MakeNewVehicle(eVehicleType i_VehicleType, string i_ModelName, string i_LisenceNumber)
        {
            Vehicle newVehicle;
            EnergyTank newEnergyTank;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    newVehicle = new Car(i_ModelName, i_LisenceNumber);
                    break;
                case eVehicleType.FuelMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    newVehicle = new Motorcycle(i_ModelName, i_LisenceNumber);
                    break;
                case eVehicleType.Truck:
                    newVehicle = new Truck(i_ModelName, i_LisenceNumber);
                    break;
                default:
                    //TODO: throw exception
                    newVehicle = null;
                    break;
            }

            newVehicle.Engine = makeNewEnergyTank(newVehicle, i_VehicleType);
            return newVehicle;

        }

        private EnergyTank makeNewEnergyTank(Vehicle i_NewVehicle, eVehicleType i_VehicleType)
        {
            EnergyTank newEnergyTank;

            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.FuelMotorcycle:
                case eVehicleType.Truck:
                    newEnergyTank = new FuelTank(i_NewVehicle.MaxFuelAmount, i_NewVehicle.FuelType);
                    break;
                case eVehicleType.ElectricCar:
                case eVehicleType.ElectricMotorcycle:
                    newEnergyTank = new Battery(i_NewVehicle.MaxTimeBatteryCanLast);
                    break;
                default:
                    //TODO: throw exception
                    newEnergyTank = null;
                    break;
            }

            return newEnergyTank;
        }
    }
}
