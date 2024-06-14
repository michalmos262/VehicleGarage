using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelTank;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolumeInCC;
        private eLicenseType m_LicenseType;
        private const int k_NumOfTires = 2;
        private const int k_MaxTireAirPressure = 33;
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Octan98;
        private const float k_MaxFuelAmount = 5.5f;
        private const float k_MaxBatteryTimeInHours = 2.5f;
        private const short k_NumOfSpecificVehicleDetails = 2;
        private const short k_LicenseTypeIndex = 0;
        private const short k_EngineVolumeIndex = 1;

        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public Motorcycle(string i_LisenceNumber) : base(i_LisenceNumber)
        {
            initSpecificVehicleDetailsRequirements();
        }

        public eLicenseType LicenseType
        {
            get
            {
                return m_LicenseType;
            }
            set
            {
                if (Enum.IsDefined(typeof(eLicenseType), value))
                {
                    m_LicenseType = value;
                }
                else
                {
                    //TODO: throw exception
                }
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolumeInCC;
            }
            set
            {
                m_EngineVolumeInCC = value;
            }
        }

        public override int MaxTireAirPressure 
        { 
            get
            {
                return k_MaxTireAirPressure;
            }
        }

        public override int NumOfTires
        {
            get
            {
                return k_NumOfTires;
            }
        }

        public override float MaxFuelAmount
        {
            get
            {
                return k_MaxFuelAmount;
            }
        }

        public override float MaxTimeBatteryCanLastInHours
        {
            get
            {
                return k_MaxBatteryTimeInHours;
            }
        }

        public override FuelTank.eFuelType FuelType
        {
            get
            {
                return k_FuelType;
            }
        }

        private void initSpecificVehicleDetailsRequirements()
        {
            string LicenseTypeStr, EngineVolumeStr;

            m_SpecificVehicleDetailsRequirementsAsStrings = new List<string>(k_NumOfSpecificVehicleDetails);
            LicenseTypeStr = string.Format("1. License type. valid values: {0}, {1}, {2}, {3}", eLicenseType.A, eLicenseType.AA
                , eLicenseType.B1, eLicenseType.A1);
            EngineVolumeStr = string.Format("2. Engine volume in CC.");
            m_SpecificVehicleDetailsRequirementsAsStrings[k_LicenseTypeIndex] = LicenseTypeStr;
            m_SpecificVehicleDetailsRequirementsAsStrings[k_EngineVolumeIndex] = EngineVolumeStr;
        }

        protected override bool doesTireHasCorrectMaxPressure(Tire i_Tire)
        {
            return i_Tire.MaxAirPressure == k_MaxTireAirPressure;
        }

        protected override bool hasRequiredNumberOfTires(List<Tire> i_Tires)
        {
            return i_Tires.Count == k_NumOfTires;
        }

        public override void VerifyAndSetAllSpecificVehicleTypeDetails(List<string> i_SpecificVehicleTypeDetailsStrings)
        {
            eLicenseType licenseType;
            int engineVolume;

            if (i_SpecificVehicleTypeDetailsStrings == null || i_SpecificVehicleTypeDetailsStrings.Count != k_NumOfSpecificVehicleDetails)
            {
                //TODO: throw exception
            }
            else if (Enum.TryParse<eLicenseType>(i_SpecificVehicleTypeDetailsStrings[k_LicenseTypeIndex], out licenseType) is false)
            {
                //TODO: throw exception
            }
            else if (int.TryParse(i_SpecificVehicleTypeDetailsStrings[k_EngineVolumeIndex], out engineVolume) is false)
            {
                //TODO: throw exception
            }
            else // All the details are valid
            {
                m_LicenseType = licenseType;
                m_EngineVolumeInCC = engineVolume;
            }
        }
    }
}