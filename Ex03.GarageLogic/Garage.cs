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

        private void verifyIfVehicleInGarage(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new Exception($"Vehicle with license number {i_LicenseNumber} does not exist in the garage!");
            }
        }

        public VehicleRecordInGarage GetVehicleRecordByLicenseNumber(string i_LicenseNumber)
        {
            verifyIfVehicleInGarage(i_LicenseNumber);
            return m_VehicleRecords[i_LicenseNumber];
        }

        public bool IsGarageEmpty()
        {
            return m_VehicleRecords.Count == 0;
        }

        public bool IsVehicleInGarage(string i_LicenseNumber)
        {
            return m_VehicleRecords.ContainsKey(i_LicenseNumber);
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

        public void InflateVehicleTiresToMaximumByLicenseNumber(Vehicle io_Vehicle)
        {
            float additionalAirPressureNeededForMaxPressure;

            foreach (Tire tire in io_Vehicle.Tires)
            {
                additionalAirPressureNeededForMaxPressure = tire.MaxAirPressure - tire.CurrentAirPressure;
                tire.Inflate(additionalAirPressureNeededForMaxPressure);
            }
        }

        public void RefuelVehicle(Vehicle io_Vehicle, FuelTank.eFuelType i_FuelType, float i_AdditionalFuelInLiters)
        {
            io_Vehicle.Engine.ReEnergize(i_AdditionalFuelInLiters, i_FuelType);
        }

        public void ChargeVehicle(Vehicle io_Vehicle, float i_AdditionalChargingTimeInHours)
        {
            io_Vehicle.Engine.ReEnergize(i_AdditionalChargingTimeInHours);
        }

        public void ChangeVehicleStatus(VehicleRecordInGarage io_VehicleRecord, VehicleRecordInGarage.eVehicleStatus i_NewVehicleStatus)
        {
            io_VehicleRecord.VehicleStatus = i_NewVehicleStatus;
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

        public Dictionary<string, string> GetAllVehicleInformation(VehicleRecordInGarage i_VehicleRecord)
        {
            Dictionary<string, string> allVehicleDetails = new Dictionary<string, string>(), basicVehicleDetails, specificVehicleDetails;

            allVehicleDetails.Add("owner name", string.IsNullOrEmpty(i_VehicleRecord.OwnerName) ? "" : i_VehicleRecord.OwnerName);
            allVehicleDetails.Add("status", i_VehicleRecord.VehicleStatus.ToString());
            allVehicleDetails.Add("Owner phone number", string.IsNullOrEmpty(i_VehicleRecord.OwnerPhoneNumber) ? "" : i_VehicleRecord.OwnerPhoneNumber);
            basicVehicleDetails = getBasicVehicleInfo(i_VehicleRecord.Vehicle);
            allVehicleDetails = allVehicleDetails.Concat(basicVehicleDetails).ToDictionary(detail => detail.Key, detail => detail.Value);
            specificVehicleDetails = i_VehicleRecord.Vehicle.GetSpecificVehicleTypeDetails();
            allVehicleDetails = allVehicleDetails.Concat(specificVehicleDetails).ToDictionary(detail => detail.Key, detail => detail.Value);

            return allVehicleDetails;
        }

        private Dictionary<string, string> getBasicVehicleInfo(Vehicle i_Vehicle)
        {
            Dictionary<string, string> basicVehicleDetails = new Dictionary<string, string>
            {
                { "License number", string.IsNullOrEmpty(i_Vehicle.LicenseNumber) ? "" : i_Vehicle.LicenseNumber },
                { "Model", string.IsNullOrEmpty(i_Vehicle.ModelName) ? "" : i_Vehicle.ModelName }
            }, energeyTankDetails;

            energeyTankDetails = i_Vehicle.Engine.GetSpecificEnergyTypeDetails();
            basicVehicleDetails = basicVehicleDetails.Concat(energeyTankDetails).ToDictionary(detail => detail.Key, detail => detail.Value);
            if (i_Vehicle.Tires != null && i_Vehicle.Tires.Count > 0 && !string.IsNullOrEmpty(i_Vehicle.Tires[0].ManufacturerName))
            {
                basicVehicleDetails.Add("Tires manufacturer", i_Vehicle.Tires[0].ManufacturerName);
                for (int i = 0; i < i_Vehicle.Tires.Count; i++)
                {
                    basicVehicleDetails.Add($"Air pressure in tire #{i + 1}", i_Vehicle.Tires[i].CurrentAirPressure.ToString());
                }
            }

            return basicVehicleDetails;
        }
    }
}