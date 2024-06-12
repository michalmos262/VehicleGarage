using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private List<Vehicle> m_Vehicles;
        private List<VehicleRecordInGarage> m_RecordsBook;

        public void AddVehicleToSystem(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (isVehicleInGarage(i_Vehicle.LisenceNumber) is false)
            {
                addVehicleToGarage(i_Vehicle);
                addNewVehicleRecordToBook(i_Vehicle.LisenceNumber, i_OwnerName, i_OwnerPhoneNumber);
            }
            else
            {
                //exeption
            }

        }

        private void addVehicleToGarage(Vehicle i_Vehicle)
        {
            makeNewTiresForVehicle(i_Vehicle, )
        }

        private void addNewVehicleRecordToBook(string i_LisenceNumber, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_RecordsBook.Add(new VehicleRecordInGarage(i_LisenceNumber, i_OwnerName, i_OwnerPhoneNumber));
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

        public void MakeNewTiresAndInsertThemToVehicle(string i_LisenceNumber, string i_TireManufacturerName)
        {
            Vehicle vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            List<Tire> newTires; 

            if (vehicle == null)
            {
                //exception
            }
            else // Vehicle already exists in system
            {
                newTires = makeNewTiresForVehicle(vehicle, i_TireManufacturerName);
                vehicle.Tires = newTires;
            }
        }

        private List<Tire> makeNewTiresForVehicle(Vehicle i_Vehicle, string i_ManufacturerName)
        {
            int i;
            List <Tire> newTires = new List<Tire>(i_Vehicle.NumOfTires);

            for (i = 0; i < i_Vehicle.NumOfTires; i++)
            {
                newTires.Add(new Tire(i_ManufacturerName, i_Vehicle.MaxTireAirPressure));
            }

            return newTires;
        }

        public void InflateVehicleTiresByLicenseNumber(string i_LisenceNumber)
        {
            Vehicle vehicle = getVehicleByLisenceNumber(i_LisenceNumber);

            if (vehicle == null)
            {
                //exception
            }
            else
            {
                foreach (Tire tire in vehicle.Tires)
                {
                    tire.Inflate(tire.MaxAirPressure - tire.CurrentAirPressure);
                }
            }
        }
    }
}
