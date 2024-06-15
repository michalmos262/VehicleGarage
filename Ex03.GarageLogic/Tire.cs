using Ex03.GrarageLogic;

namespace Ex03.GarageLogic
{
    public class Tire
    {
        private string m_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressureByManufacturer;
        private const float k_MinAirPressure = 0;

        public Tire(float i_MaxAirPressureByManufacturer)
        {
            r_MaxAirPressureByManufacturer = i_MaxAirPressureByManufacturer;
        }

        public string ManufacturerName
        {
            get
            {
                return m_ManufacturerName;
            }
            set
            {
                m_ManufacturerName = value;
            }
        }

        public float CurrentAirPressure
        {
            get 
            { 
                return m_CurrentAirPressure; 
            }
            set
            {
                if (value <= r_MaxAirPressureByManufacturer)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinAirPressure, r_MaxAirPressureByManufacturer);
                }
            }
        }

        public float MaxAirPressure
        {
            get 
            { 
                return r_MaxAirPressureByManufacturer; 
            }
        }

        public void Inflate(float i_AdditionalAirPressure)
        {
            CurrentAirPressure += i_AdditionalAirPressure;
        }
    }
}