using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    internal class Garage
    {
        private Dictionary<string, VehicleRecordInGarage> m_VehicleRecords;

        public void AddNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            if (isVehicleInGarage(i_Vehicle.LisenceNumber) is false)
            {
                m_VehicleRecords[i_Vehicle.LisenceNumber] = new VehicleRecordInGarage(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber);
            }
            else
            {
                //TODO: throw exception
            }
        }

        private VehicleRecordInGarage getVehicleRecordByLisenceNumber(string i_LisenceNumber)
        {
            VehicleRecordInGarage requestedVehicleRecord = null;

            if (isVehicleInGarage(i_LisenceNumber))
            {
                requestedVehicleRecord = m_VehicleRecords[i_LisenceNumber];
            }
            else
            {
                throw new Exception($"Vehicle with lisence number {i_LisenceNumber} does not exist");
            }
            
            return requestedVehicleRecord;
        }

        private bool isVehicleInGarage(string i_LisenceNumber)
        {
            return m_VehicleRecords.ContainsKey(i_LisenceNumber);
        }

        private Vehicle getVehicleByLisenceNumber(string i_LisenceNumber)
        {
            VehicleRecordInGarage requestedVehicleRecord = getVehicleRecordByLisenceNumber(i_LisenceNumber);

            return requestedVehicleRecord != null ? requestedVehicleRecord.Vehicle : null;
        }

        public void MakeNewTiresAndInsertThemToVehicle(string i_LisenceNumber, string i_TireManufacturerName)
        {
            Vehicle vehicle;
            List<Tire> newTires;

            try
            {
                vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
                newTires = makeNewTiresForVehicle(vehicle, i_TireManufacturerName);
                vehicle.Tires = newTires;
            }
            catch (Exception e)
            {
                throw new Exception("Can't make new tires for car that does not exist in the garage");
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

        public void InflateVehicleTiresToMaximumByLicenseNumber(string i_LisenceNumber)
        {
            Vehicle vehicle;
            float additionalAirPressureNeededForMaxPressure;

            vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            foreach (Tire tire in vehicle.Tires)
            {
                additionalAirPressureNeededForMaxPressure = tire.MaxAirPressure - tire.CurrentAirPressure;
                tire.Inflate(additionalAirPressureNeededForMaxPressure);
            }
        }

        public void ChangeAllVehicleSpecificDetails(string i_LisenceNumber, List<string> i_AllSpecificVehicleTypeDetails)
        {
            Vehicle vehicle;

            vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            try
            {
                vehicle.VerifyAndSetAllSpecificVehicleTypeDetails(i_AllSpecificVehicleTypeDetails);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public void ReFuelVehicle(string i_LisenceNumber, FuelTank.eFuelType i_FuelType, float i_AdditionalFuelInLiters)
        {
            Vehicle vehicle;

            vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            try
            {
                vehicle.Engine.ReEnergize(i_AdditionalFuelInLiters, i_FuelType);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public void ReChargeVehicle(string i_LisenceNumber, float i_AdditionalChargingTimeInHours)
        {
            Vehicle vehicle;

            vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            try
            {
                vehicle.Engine.ReEnergize(i_AdditionalChargingTimeInHours);
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public void ChangeVehicleStatus(string i_LisenceNumber, VehicleRecordInGarage.eVehicleStatus i_NewVehicleStatus)
        {
            VehicleRecordInGarage vehicleRecord;

            vehicleRecord = getVehicleRecordByLisenceNumber(i_LisenceNumber);
            vehicleRecord.VehicleStatus = i_NewVehicleStatus;
        }

        public List<string> GetLisenceNumbersList(VehicleRecordInGarage.eVehicleStatus? i_VehicleStatusFilter = null)
        {
            List<string> lisenceNumbersList;

            if (i_VehicleStatusFilter.HasValue)
            {
                lisenceNumbersList = getLisenceNumbersListFilteredByStatus(i_VehicleStatusFilter.Value);
            }
            else
            {
                lisenceNumbersList = m_VehicleRecords.Keys.ToList();
            }

            return lisenceNumbersList;
        }

        private List<string> getLisenceNumbersListFilteredByStatus(VehicleRecordInGarage.eVehicleStatus i_VehicleStatusFilter)
        {
            List<string> lisenceNumbersListFilteredByStatus = new List<string>();

            foreach (string lisenceNumber in m_VehicleRecords.Keys)
            {
                if (m_VehicleRecords[lisenceNumber].VehicleStatus == i_VehicleStatusFilter)
                {
                    lisenceNumbersListFilteredByStatus.Add(lisenceNumber);
                }
            }

            return lisenceNumbersListFilteredByStatus.Count == 0 ? null : lisenceNumbersListFilteredByStatus;
        }

        public void UpdateModelName(string i_LisenceNumber, string i_ModelName)
        {
            Vehicle vehicle;

            try
            {
                vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
                vehicle.ModelName = i_ModelName;
            }
            catch (Exception e)
            {
                // TODO
            }
        }

        public Dictionary<string, string> GetAllVehicleDetails(string i_LisenceNumber)
        {
            Vehicle vehicle;
            VehicleRecordInGarage vehicleRecord;
            Dictionary<string, string> allVehicleDetails = new Dictionary<string, string>(), basicVehicleDetails, specificVehicleDetails;

            vehicle = getVehicleByLisenceNumber(i_LisenceNumber);
            vehicleRecord = getVehicleRecordByLisenceNumber(i_LisenceNumber);
            allVehicleDetails.Add("owner name", string.IsNullOrEmpty(vehicleRecord.OwnerName) ? "" : vehicleRecord.OwnerName);
            allVehicleDetails.Add("status", vehicleRecord.VehicleStatus.ToString());
            basicVehicleDetails = getBasicVehicleInfo(vehicle);
            allVehicleDetails = allVehicleDetails.Concat(basicVehicleDetails).ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // Concat dictionaries
            specificVehicleDetails = vehicle.GetSpecificVehicleTypeDetails();
            allVehicleDetails = allVehicleDetails.Concat(specificVehicleDetails).ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // Concat dictionaries

            return allVehicleDetails;
        }

        private Dictionary<string, string> getBasicVehicleInfo(Vehicle i_Vehicle)
        {
            Dictionary<string, string> basicVehicleDetails = new Dictionary<string, string>(), energeyTankDetails;
            
            basicVehicleDetails.Add("license number", string.IsNullOrEmpty(i_Vehicle.LisenceNumber) ? "" : i_Vehicle.LisenceNumber);
            basicVehicleDetails.Add("model", string.IsNullOrEmpty(i_Vehicle.ModelName) ? "" : i_Vehicle.ModelName);
            energeyTankDetails = i_Vehicle.Engine.GetSpecificEnergeyTypeDetails();
            basicVehicleDetails = basicVehicleDetails.Concat(energeyTankDetails).ToDictionary(kvp => kvp.Key, kvp => kvp.Value); // Concat dictionaries
            if (i_Vehicle.Tires != null && i_Vehicle.Tires.Count > 0 && !string.IsNullOrEmpty(i_Vehicle.Tires[0].ManufacturerName))
            {
                basicVehicleDetails.Add("tires manufacturer", i_Vehicle.Tires[0].ManufacturerName);
                for (int i = 0; i < i_Vehicle.Tires.Count; i++)
                {
                    basicVehicleDetails.Add($"air pressure in tire number {i + 1}", i_Vehicle.Tires[i].CurrentAirPressure.ToString());
                }
            }
            
            return basicVehicleDetails;
        }
    }
}
