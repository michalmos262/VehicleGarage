using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class Garage
    {
        private Dictionary<string, VehicleRecordInGarage> m_VehicleRecords = new Dictionary<string, VehicleRecordInGarage>();

        public void AddNewVehicleToGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_VehicleRecords[i_Vehicle.LicenseNumber] = new VehicleRecordInGarage(i_Vehicle, i_OwnerName, i_OwnerPhoneNumber);
        }

        private VehicleRecordInGarage getVehicleRecordByLicenseNumber(string i_LicenseNumber)
        {
            VehicleRecordInGarage requestedVehicleRecord = null;

            if (IsVehicleInGarage(i_LicenseNumber))
            {
                requestedVehicleRecord = m_VehicleRecords[i_LicenseNumber];
            }
            else
            {
                throw new Exception($"Vehicle with license number {i_LicenseNumber} does not exist");
            }
            
            return requestedVehicleRecord;
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_VehicleRecords.ContainsKey(i_LicenseNumber);
        }

        private Vehicle getVehicleByLicenseNumber(string i_LicenseNumber)
        {
            VehicleRecordInGarage requestedVehicleRecord = getVehicleRecordByLicenseNumber(i_LicenseNumber);

            return requestedVehicleRecord != null ? requestedVehicleRecord.Vehicle : null;
        }

        public List<Tire> MakeNewTiresForVehicle(Vehicle i_Vehicle)
        {
            List <Tire> newTires = new List<Tire>(i_Vehicle.NumOfTires);

            for (int i = 0; i < i_Vehicle.NumOfTires; i++)
            {
                newTires.Add(new Tire(i_Vehicle.MaxTireAirPressure));
            }
            return newTires;
        }

        public void InflateVehicleTiresToMaximumByLicenseNumber(string i_LicenseNumber)
        {
            Vehicle vehicle;
            float additionalAirPressureNeededForMaxPressure;

            vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            foreach (Tire tire in vehicle.Tires)
            {
                additionalAirPressureNeededForMaxPressure = tire.MaxAirPressure - tire.CurrentAirPressure;
                tire.Inflate(additionalAirPressureNeededForMaxPressure);
            }
        }

        public void ReFuelVehicle(string i_LicenseNumber, FuelTank.eFuelType i_FuelType, float i_AdditionalFuelInLiters)
        {
            Vehicle vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            vehicle.Engine.ReEnergize(i_AdditionalFuelInLiters, i_FuelType);
        }

        public void ReChargeVehicle(string i_LicenseNumber, float i_AdditionalChargingTimeInHours)
        {
            Vehicle vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            vehicle.Engine.ReEnergize(i_AdditionalChargingTimeInHours);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleRecordInGarage.eVehicleStatus i_NewVehicleStatus)
        {
            VehicleRecordInGarage vehicleRecord = getVehicleRecordByLicenseNumber(i_LicenseNumber);
            vehicleRecord.VehicleStatus = i_NewVehicleStatus;
        }

        public List<string> GetLicenseNumbersList(VehicleRecordInGarage.eVehicleStatus? i_VehicleStatusFilter = null)
        {
            List<string> licenseNumbersList;

            if (i_VehicleStatusFilter.HasValue)
            {
                licenseNumbersList = getLicenseNumbersListFilteredByStatus(i_VehicleStatusFilter.Value);
            }
            else
            {
                licenseNumbersList = m_VehicleRecords.Keys.ToList();
            }

            return licenseNumbersList;
        }

        private List<string> getLicenseNumbersListFilteredByStatus(VehicleRecordInGarage.eVehicleStatus i_VehicleStatusFilter)
        {
            List<string> licenseNumbersListFilteredByStatus = new List<string>();

            foreach (string licenseNumber in m_VehicleRecords.Keys)
            {
                if (m_VehicleRecords[licenseNumber].VehicleStatus == i_VehicleStatusFilter)
                {
                    licenseNumbersListFilteredByStatus.Add(licenseNumber);
                }
            }

            return licenseNumbersListFilteredByStatus.Count == 0 ? null : licenseNumbersListFilteredByStatus;
        }

        public Dictionary<string, string> GetAllVehicleDetails(string i_LicenseNumber)
        {
            Vehicle vehicle;
            VehicleRecordInGarage vehicleRecord;
            Dictionary<string, string> allVehicleDetails = new Dictionary<string, string>(), basicVehicleDetails, specificVehicleDetails;

            vehicle = getVehicleByLicenseNumber(i_LicenseNumber);
            vehicleRecord = getVehicleRecordByLicenseNumber(i_LicenseNumber);
            allVehicleDetails.Add("owner name", string.IsNullOrEmpty(vehicleRecord.OwnerName) ? "" : vehicleRecord.OwnerName);
            allVehicleDetails.Add("status", vehicleRecord.VehicleStatus.ToString());
            basicVehicleDetails = getBasicVehicleInfo(vehicle);
            allVehicleDetails = allVehicleDetails.Concat(basicVehicleDetails).ToDictionary(detail => detail.Key, detail => detail.Value);
            specificVehicleDetails = vehicle.GetSpecificVehicleTypeDetails();
            allVehicleDetails = allVehicleDetails.Concat(specificVehicleDetails).ToDictionary(detail => detail.Key, detail => detail.Value);

            return allVehicleDetails;
        }

        private Dictionary<string, string> getBasicVehicleInfo(Vehicle i_Vehicle)
        {
            Dictionary<string, string> basicVehicleDetails = new Dictionary<string, string>(), energeyTankDetails;

            basicVehicleDetails.Add("License number", string.IsNullOrEmpty(i_Vehicle.LicenseNumber) ? "" : i_Vehicle.LicenseNumber);
            basicVehicleDetails.Add("Model", string.IsNullOrEmpty(i_Vehicle.ModelName) ? "" : i_Vehicle.ModelName);
            energeyTankDetails = i_Vehicle.Engine.GetSpecificEnergyTypeDetails();
            basicVehicleDetails = basicVehicleDetails.Concat(energeyTankDetails).ToDictionary(detail => detail.Key, detail => detail.Value);
            if (i_Vehicle.Tires != null && i_Vehicle.Tires.Count > 0 && !string.IsNullOrEmpty(i_Vehicle.Tires[0].ManufacturerName))
            {
                basicVehicleDetails.Add("Tires manufacturer", i_Vehicle.Tires[0].ManufacturerName);
                for (int i = 0; i < i_Vehicle.Tires.Count; i++)
                {
                    basicVehicleDetails.Add($"Air pressure in tire number {i + 1}", i_Vehicle.Tires[i].CurrentAirPressure.ToString());
                }
            }

            return basicVehicleDetails;
        }
    }
}