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

        public Truck(string i_ModelName, string i_LisenceNumber) : base(i_ModelName, i_LisenceNumber)
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

        protected override bool doesTireHasCorrectMaxPressure(Tire i_Tire)
        {
            return i_Tire.MaxAirPressure == k_MaxTireAirPressure;
        }

        protected override bool hasRequiredNumberOfTires(List<Tire> i_Tires)
        {
            return i_Tires.Count == k_NumOfTires;
        }
    }
}