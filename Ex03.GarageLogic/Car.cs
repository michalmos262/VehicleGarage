using System;
using System.Collections.Generic;
using Ex03.GrarageLogic;
using static Ex03.GarageLogic.Car;
using static Ex03.GarageLogic.FuelTank;

namespace Ex03.GarageLogic
{
    public class Car : Vehicle
    {
        private eColor m_Color;
        private int m_NumOfDoors;
        private const int k_MinNumOfDoors = 2;
        private const int k_MaxNumOfDoors = 5;
        private const int k_NumOfTires = 5;
        private const float k_MaxTireAirPressure = 31f;
        private const eFuelType k_FuelType = eFuelType.Octan95;
        private const float k_MaxFuelAmount = 45f;
        private const float k_MaxBatteryTimeInHours = 3.5f;
        private const short k_NumOfDoorsDetailIndex = 0;
        private const short k_ColorDetailIndex = 1;

        public enum eColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public Car(string i_LicenseNumber) : base(i_LicenseNumber)
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
                if (Enum.IsDefined(typeof(eColor), value))
                {
                    m_Color = value;
                }
                else
                {
                    throw new ArgumentException("Color is not correct!");
                }
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
                    throw new ValueOutOfRangeException(k_MinNumOfDoors, k_MaxNumOfDoors);
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
                return k_MaxBatteryTimeInHours;
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

        public override void VerifyAndSetAllSpecificVehicleTypeDetails(List<string> i_VehicleTypeDetails)
        {
            int numOfDoors;
            eColor color;

            if (int.TryParse(i_VehicleTypeDetails[k_NumOfDoorsDetailIndex], out numOfDoors))
            {
                NumOfDoors = numOfDoors;
            }
            else
            {
                throw new FormatException("Number of doors should be a number!");
            }
            if (!Enum.TryParse<eColor>(i_VehicleTypeDetails[k_ColorDetailIndex], out color))
            {
                Color = color;
            }
        }

        public override Dictionary<string, string> GetSpecificVehicleTypeDetails()
        {
            Dictionary<string, string> specificCarDetails = new Dictionary<string, string>();

            specificCarDetails.Add("Number of doors", m_NumOfDoors.ToString());
            specificCarDetails.Add("Color", m_Color.ToString());

            return specificCarDetails;
        }

        public override List<string> RequestAdditionalVehicleDetails()
        {
            return new List<string>()
            {
                $"1. Number of doors. Valid values: {k_MinNumOfDoors}-{k_MaxNumOfDoors}",
                $@"2. Color. valid values:
(1) {eColor.Yellow}
(2) {eColor.White}
(3) {eColor.Red}
(4) {eColor.Black}"
            };
        }
    }
}