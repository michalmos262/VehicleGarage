using System;
using System.Collections.Generic;
using static Ex03.GarageLogic.Car;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_Color;
        private int m_NumOfDoors;
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private const int k_NumOfTires = 5;
        private const int k_MaxTireAirPressure = 31;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxFuelAmount = 45f;
        private const float k_MaxBatteryTimeInHours = 3.5f;

        public enum eColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public Car(string i_LisenceNumber, int i_NumOfDoors, eColor i_Color) : base(i_LisenceNumber)
        {
            m_NumOfDoors = i_NumOfDoors;
            m_Color = i_Color;
            m_Tires = new List<Tire>(k_NumOfTires);
        }

        public eColor Color
        {
            get
            {
                return m_Color;
            }
            set
            {
                m_Color = value;
            }
        }

        public int NumOfDoors
        {
            get
            {
                return m_NumOfDoors;
            }
            set
            {
                if (!(value >= k_MinNumOfDoors && value <= k_MaxNumOfDoors))
                {
                    throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors);
                }
                else
                {
                    m_NumOfDoors = value;
                }
            }
        }
    }
}
