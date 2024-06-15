using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelTank;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTransportingHazardousMaterials;
        private float m_CargoVolume;
        private const int k_NumOfTires = 12;
        private const float k_MaxTireAirPressure = 28f;
        private const eFuelType k_FuelType = eFuelType.Soler;
        private const float k_MaxFuelAmount = 120f;
        private const int k_PossibleMinCargoVolume = 0;
        private const short k_NumOfSpecificVehicleDetails = 2;
        private const short k_IsTransportingHazardousMaterialsIndex = 0;
        private const short k_CargoVolumeIndex = 1;

        public Truck(string i_LicenseNumber) : base(i_LicenseNumber)
        {
            
        }

        public bool IsTransportingHazardousMaterials
        {
            get
            {
                return m_IsTransportingHazardousMaterials;
            }
            set
            {
                m_IsTransportingHazardousMaterials = value;
            }
        }

        public float CargoVolume
        {
            get
            {
                return m_CargoVolume;
            }
            set
            {
                if (value >= k_PossibleMinCargoVolume)
                {
                    m_CargoVolume = value;
                }
                else
                {
                    throw new ArgumentException(string.Format("Cargo Volume must be at least {0}", k_PossibleMinCargoVolume));
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
                throw new InvalidOperationException("Truck does not have a battery");
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
            float cargoVolume;

            if (float.TryParse(i_VehicleTypeDetails[k_CargoVolumeIndex], out cargoVolume))
            {
                CargoVolume = cargoVolume;
            }
            else
            {
                throw new FormatException("Cargo volume must be a positive number!");
            }
            if (i_VehicleTypeDetails[k_IsTransportingHazardousMaterialsIndex] == "yes")
            {
                m_IsTransportingHazardousMaterials = true;
            }
            else
            {
                m_IsTransportingHazardousMaterials = false;
            }
        }

        public override Dictionary<string, string> GetSpecificVehicleTypeDetails()
        {
            Dictionary<string, string> SpecificTruckDetails = new Dictionary<string, string>();

            SpecificTruckDetails.Add("Transporting hazardous materials", m_IsTransportingHazardousMaterials.ToString());
            SpecificTruckDetails.Add("Cargo volume", m_CargoVolume.ToString());

            return SpecificTruckDetails;
        }

        public override List<string> RequestAdditionalVehicleDetails()
        {
            return new List<string>()
            {
                $@"Enter the cargo volume:",
                "Is the truck transporting hazardous materials? Enter 'yes' or any other key for no:"
            };
        }
    }
}