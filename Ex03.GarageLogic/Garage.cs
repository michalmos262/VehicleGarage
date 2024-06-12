using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private List<Vehicle> m_Vehicles;
        private List<VehicleRecordInGarage> m_RecordsBook;

        public VehicleRecordInGarage AddVehicleToSystem(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            AddNewVehicleRecordToBook(i_Vehicle.LisenceNumber, i_OwnerName, i_OwnerPhoneNumber);
            return AddVehicleToGarage(i_Vehicle);
        }

        public VehicleRecordInGarage AddVehicleToGarage(Vehicle i_Vehicle)
        {
            VehicleRecordInGarage vehicleRecord;

            vehicleRecord = getVehicleRecordByLisenceNumber(i_Vehicle.LisenceNumber);
            if (vehicleRecord == null) // Need to make new car
            {

            }

            return vehicleRecord;
        }

        public void AddNewVehicleRecordToBook(string i_LisenceNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (isVehicleInGarage(i_LisenceNumber) is false)
            {

            }
        }

        private VehicleRecordInGarage getVehicleRecordByLisenceNumber(string i_LisenceNumber)
        {
            VehicleRecordInGarage requestedVehiclerecord = null;

            foreach (VehicleRecordInGarage record in m_RecordsBook)
            {
                if (record.LisenceNumber == i_LisenceNumber)
                {
                    requestedVehiclerecord = record;
                    break;
                }
            }

            return requestedVehiclerecord;
        }

        private bool isVehicleInGarage(string i_LisenceNumber)
        {
            return getVehicleRecordByLisenceNumber(i_LisenceNumber) != null;
        }

        private Vehicle getVehicleByLisenceNumber(string i_LisenceNumber)
        {
            Vehicle requestedVehicle = null;

            foreach (Vehicle vehicle in m_Vehicles)
            {
                if (vehicle.LisenceNumber == i_LisenceNumber)
                {
                    requestedVehicle = vehicle;
                    break;
                }
            }

            return requestedVehicle;
        }
    }
}
