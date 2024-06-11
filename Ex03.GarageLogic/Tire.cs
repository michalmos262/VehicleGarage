﻿namespace Ex03.GarageLogic
{
    public class Tire
    {
        private readonly string r_ManufacturerName;
        private float m_CurrentAirPressure;
        private readonly float r_MaxAirPressureByManufacturer;

        public Tire(string i_ManufacturerName, float i_MaxAirPressureByManufacturer)
        {
            r_ManufacturerName = i_ManufacturerName;
            m_CurrentAirPressure = 0;
            r_MaxAirPressureByManufacturer = i_MaxAirPressureByManufacturer;
        }

        public string ManufacturerName
        {
            get
            {
                return r_ManufacturerName;
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
            m_CurrentAirPressure += i_AdditionalAirPressure;
        }
    }
}
