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
        private const float k_MaxTireAirPressure = 33f;
        private const eFuelType k_FuelType = eFuelType.Octan98;
        private const float k_MaxFuelAmount = 5.5f;
        private const float k_MaxBatteryTimeInHours = 2.5f;
        private const short k_LicenseTypeIndex = 0;
        private const short k_EngineVolumeIndex = 1;

        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public Motorcycle(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            for (int i = 0; i < k_NumOfTires; i++)
            {
                m_Tires.Add(new Tire(k_NumOfTires));
            }
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
                    throw new ArgumentException("License type is not correct!");
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
                if (value >= k_MinEngineVolume)
                {
                    m_EngineVolumeInCC = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Engine volume must be greater than {0}", k_MinEngineVolume));
                }
            }
        }

        public override float MaxTireAirPressure
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

        protected override bool doesTireHasCorrectMaxPressure(Tire i_Tire)
        {
            return i_Tire.MaxAirPressure == k_MaxTireAirPressure;
        }

        protected override bool hasRequiredNumberOfTires(List<Tire> i_Tires)
        {
            return i_Tires.Count == k_NumOfTires;
        }

        public override void setVehicleDetails(List<string> i_VehicleTypeDetails)
        {
            eLicenseType licenseType;
            int engineVolume;

            if (!(Enum.TryParse(i_VehicleTypeDetails[k_LicenseTypeIndex], out licenseType)))
            {
                throw new ArgumentException("License type must be a number!");
            }
            if (!(int.TryParse(i_VehicleTypeDetails[k_EngineVolumeIndex], out engineVolume)))
            {
                throw new ArgumentException("Engine volume in CC must be a number!");
            }
            LicenseType = licenseType;
            EngineVolume = engineVolume;
        }

        public override Dictionary<string, string> GetSpecificVehicleTypeDetails()
        {
            Dictionary<string, string> SpecificMotorcycleDetails = new Dictionary<string, string>();

            SpecificMotorcycleDetails.Add("Licence type", m_LicenseType.ToString());
            SpecificMotorcycleDetails.Add("Engine volume", m_EngineVolumeInCC.ToString());

            return SpecificMotorcycleDetails;
        }

        public override List<string> RequestAdditionalVehicleDetails()
        {
            return new List<string>()
            {
                $@"Enter license type option:
(1) {eLicenseType.A}
(2) {eLicenseType.A1}
(3) {eLicenseType.AA}
(4) {eLicenseType.B1}",
                "Enter the engine volume in CC:"
            };
        }
    }
}