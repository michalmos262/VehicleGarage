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
        private const FuelTank.eFuelType k_FuelType = FuelTank.eFuelType.Octan95;
        private const float k_MaxFuelAmount = 45f;
        private const float k_MaxBatteryTimeInHours = 3.5f;

        public enum eColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public Car(string i_ModelName, string i_LisenceNumber) : base(i_ModelName, i_LisenceNumber)
        {
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
                if (value >= k_MinNumOfDoors && value <= k_MaxNumOfDoors)
                {
                    m_NumOfDoors = value;
                }
                else
                {
                    //throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors);
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

        public FuelTank.eFuelType FuelType
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
    }
}