using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Truck : Vehicle
    {
        private bool m_IsTransportingHazardousMaterials;
        private float m_CargoVolume;
        private const int k_NumOfTires = 12;
        private const int k_MaxTireAirPressure = 28;
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Soler;
        private const float k_MaxFuelAmount = 120f;
        private const int k_PossibleMinCargoVolume = 0;
        private const short k_NumOfSpecificVehicleDetails = 2;
        private const short k_IsTransportingHazardousMaterialsIndex = 0;
        private const short k_CargoVolumeIndex = 1;

        public Truck(string i_LisenceNumber) : base(i_LisenceNumber)
        {
            initSpecificVehicleDetailsRequirements();
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

        private void initSpecificVehicleDetailsRequirements()
        {
            string isTransportingHazardousMaterialsStr, cargoVolumeStr;

            m_SpecificVehicleDetailsRequirementsAsStrings = new List<string>(k_NumOfSpecificVehicleDetails);
            isTransportingHazardousMaterialsStr = string.Format("1. Does the truck carry hazardous materials? valid values: yes, no.");
            cargoVolumeStr = string.Format("2. Cargo volume.");
            m_SpecificVehicleDetailsRequirementsAsStrings[k_IsTransportingHazardousMaterialsIndex] = isTransportingHazardousMaterialsStr;
            m_SpecificVehicleDetailsRequirementsAsStrings[k_CargoVolumeIndex] = cargoVolumeStr;
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
            float cargoVolume;

            if (i_SpecificVehicleTypeDetailsStrings == null || i_SpecificVehicleTypeDetailsStrings.Count != k_NumOfSpecificVehicleDetails)
            {
                //TODO: throw exception
            }
            else if (float.TryParse(i_SpecificVehicleTypeDetailsStrings[k_CargoVolumeIndex], out cargoVolume) is false)
            {
                //TODO: throw exception
            }
            else if (i_SpecificVehicleTypeDetailsStrings[k_IsTransportingHazardousMaterialsIndex] != "yes"
                && i_SpecificVehicleTypeDetailsStrings[k_IsTransportingHazardousMaterialsIndex] != "no")
            {
                //TODO: throw exception
            }
            else // All the details are valid
            {
                m_IsTransportingHazardousMaterials = i_SpecificVehicleTypeDetailsStrings[k_IsTransportingHazardousMaterialsIndex] == "yes";
                m_CargoVolume = cargoVolume;
            }
        }
    }
}