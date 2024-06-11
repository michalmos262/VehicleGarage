using System.Collections.Generic;
using static Ex03.GarageLogic.Motorcycle;

namespace Ex03.GarageLogic
{
    public class Motorcycle : Vehicle
    {
        private int m_EngineVolumeInCC;
        private const int k_NumOfTires = 2;
        private const int k_MaxTireAirPressure = 33;
        private const eFuelType k_FuelType = eFuelType.Octan98;
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

        public Motorcycle(string i_LisenceNumber, eLicenseType i_LicenseType, int i_EngineVolumeInCC) : base(i_LisenceNumber)
        {
            m_LicenseType = i_LicenseType;
            m_EngineVolumeInCC = i_EngineVolumeInCC;
            m_Tires = new List<Tire>(k_NumOfTires);
        }

        public eLicenseType LicenseType
        {
            get { return m_LicenseType; }
            set
            {
                if (!(value >= eLicenseType.A && value <= eLicenseType.B1))
                {
                    throw new ValueOutOfRangeException((int)eLicenseType.A, (int)eLicenseType.B1);
                }
                else
                {
                    m_LicenseType = value;
                }
            }
        }

        public int EngineVolume
        {
            get
            {
                return m_EngineVolumeInCC;
            }
        }
    }
}