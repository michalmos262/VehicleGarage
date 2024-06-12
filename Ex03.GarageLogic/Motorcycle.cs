using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.FuelTank;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolumeInCC;
        private const int k_NumOfTires = 2;
        private const int k_MaxTireAirPressure = 33;
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Octan98;
        private const float k_MaxFuelAmount = 5.5f;
        private const float k_MaxBatteryTimeInHours = 2.5f;
        private eLicenseType m_LicenseType;

        public enum eLicenseType
        {
            A,
            A1,
            AA,
            B1
        }

        public Motorcycle(string i_ModelName, string i_LisenceNumber) : base(i_ModelName, i_LisenceNumber)
        {
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