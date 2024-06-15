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
        private const short k_NumOfSpecificVehicleDetails = 2;
        private const short k_NumOfDoorsDetailIndex = 0;
        private const short k_ColorDetailIndex = 1;


        public enum eColor
        {
            Yellow,
            White,
            Red,
            Black
        }

        public Car(string i_LisenceNumber) : base(i_LisenceNumber)
        {
            initSpecificVehicleDetailsRequirements();
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
                {
                    //TODO: throw exception
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

        private void initSpecificVehicleDetailsRequirements()
        {
            string numOfDoorsStr, colorStr;

            m_SpecificVehicleDetailsRequirementsAsStrings = new List<string>(k_NumOfSpecificVehicleDetails);
            numOfDoorsStr = string.Format("1. Number of doors. valid values: {0}-{1}", k_MinNumOfDoors, k_MaxNumOfDoors);
            colorStr = string.Format("2. Color. valid values: {0}, {1}, {2}, {3}", eColor.Red, eColor.Yellow
                , eColor.White, eColor.Black);
            m_SpecificVehicleDetailsRequirementsAsStrings[k_NumOfDoorsDetailIndex] = numOfDoorsStr;
            m_SpecificVehicleDetailsRequirementsAsStrings[k_ColorDetailIndex] = colorStr;
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
            bool isNumOfDoorsStringIsNumber;
            int numOfDoors;
            eColor color;

            if (i_SpecificVehicleTypeDetailsStrings == null || i_SpecificVehicleTypeDetailsStrings.Count != k_NumOfSpecificVehicleDetails)
            {
                //TODO: throw exception
            }
            isNumOfDoorsStringIsNumber = int.TryParse(i_SpecificVehicleTypeDetailsStrings[k_NumOfDoorsDetailIndex], out numOfDoors);
            if (!isNumOfDoorsStringIsNumber)
            {
                //TODO: throw exception
            }
            else if (isNumOfDoorsStringIsNumber && (numOfDoors < k_MinNumOfDoors || k_MaxNumOfDoors < numOfDoors))
            {
                //TODO: throw exception
            }
            else if (!Enum.TryParse<eColor>(i_SpecificVehicleTypeDetailsStrings[k_ColorDetailIndex], out color))
            {
                //TODO: throw exception
            }
            else // All the details are valid
            {
                m_NumOfDoors = numOfDoors;
                m_Color = color;
            }
        }

        public override Dictionary<string, string> GetSpecificVehicleTypeDetails()
        {
            Dictionary<string, string> SpecificCarDetails = new Dictionary<string, string>();

            SpecificCarDetails.Add("number of doors", m_NumOfDoors.ToString());
            SpecificCarDetails.Add("color", m_Color.ToString());

            return SpecificCarDetails;
        }
    }
}