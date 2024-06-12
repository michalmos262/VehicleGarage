
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

        public Vehicle makeNewVehicle(eVehicleType i_VehicleType, string i_LisenceNumber)
        {
            switch (i_VehicleType)
            {
                case eVehicleType.FuelCar:
                case eVehicleType.ElectricCar:
                    return new Car(i_LisenceNumber);
                    break;
                case eVehicleType.FuelMotorcycle:
                case eVehicleType.ElectricMotorcycle:
                    return new Motorcycle(i_LisenceNumber);
                case eVehicleType.Truck:
                    return new Truck(i_LisenceNumber);
                default:
                    //TODO: throw exception
                    return null;
            }
        }

    }
}
