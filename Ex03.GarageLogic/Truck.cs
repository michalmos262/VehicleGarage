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

        public Truck(string i_LisenceNumber, bool i_IsTransportingHazardousMaterials, float i_CargoVolume) : base(i_LisenceNumber)
        {
            m_IsTransportingHazardousMaterials = i_IsTransportingHazardousMaterials;
            m_CargoVolume = i_CargoVolume;
            m_Tires = new List<Tire>(k_NumOfTires);
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
    }
}