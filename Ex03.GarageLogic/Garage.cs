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

        public void VerifyIfVehicleInGarage(string i_LicenseNumber)
        {
            if (!IsVehicleInGarage(i_LicenseNumber))
            {
                throw new Exception($"Vehicle with license number {i_LicenseNumber} does not exist in the garage!");
            }
        }

        public VehicleRecordInGarage GetVehicleRecordByLicenseNumber(string i_LicenseNumber)
        {
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

        public void InflateVehicleTiresToMaximumByLicenseNumber(string i_LicenseNumber)
        {
            float additionalAirPressureNeededForMaxPressure;
            List<Tire> vehicleTires = m_VehicleRecords[i_LicenseNumber].Vehicle.Tires;

            foreach (Tire tire in vehicleTires)
            {
                additionalAirPressureNeededForMaxPressure = tire.MaxAirPressure - tire.CurrentAirPressure;
                tire.Inflate(additionalAirPressureNeededForMaxPressure);
            }
        }

        public void RefuelVehicle(string i_LicenseNumber, FuelTank.eFuelType i_FuelType, float i_AdditionalFuelInLiters)
        {
            m_VehicleRecords[i_LicenseNumber].Vehicle.Engine.ReEnergize(i_AdditionalFuelInLiters, i_FuelType);
        }

        public void ChargeVehicle(string i_LicenseNumber, float i_AdditionalChargingTimeInMinutes)
        {
            m_VehicleRecords[i_LicenseNumber].Vehicle.Engine.ReEnergize(i_AdditionalChargingTimeInMinutes / 60);
        }

        public void ChangeVehicleStatus(string i_LicenseNumber, VehicleRecordInGarage.eVehicleStatus i_NewVehicleStatus)
        {
            m_VehicleRecords[i_LicenseNumber].VehicleStatus = i_NewVehicleStatus;
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

        public Dictionary<string, string> GetAllVehicleInformation(string i_LicenseNumber)
        {
            Dictionary<string, string> allVehicleDetails = new Dictionary<string, string>(), basicVehicleDetails, specificVehicleDetails;
            VehicleRecordInGarage vehicleRecord = m_VehicleRecords[i_LicenseNumber];

            allVehicleDetails.Add("owner name", string.IsNullOrEmpty(vehicleRecord.OwnerName) ? "" : vehicleRecord.OwnerName);
            allVehicleDetails.Add("status", vehicleRecord.VehicleStatus.ToString());
            allVehicleDetails.Add("Owner phone number", string.IsNullOrEmpty(vehicleRecord.OwnerPhoneNumber) ? "" : vehicleRecord.OwnerPhoneNumber);
            basicVehicleDetails = getBasicVehicleInfo(vehicleRecord.Vehicle);
            allVehicleDetails = allVehicleDetails.Concat(basicVehicleDetails).ToDictionary(detail => detail.Key, detail => detail.Value);
            specificVehicleDetails = vehicleRecord.Vehicle.GetSpecificVehicleTypeDetails();
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