using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LisenceNumber;
        protected List<Tire> m_Tires;
        protected EnergyTank m_Engine;

        protected Vehicle(string i_ModelName, string i_LisenceNumber)
        {
            m_ModelName = i_ModelName;
            m_LisenceNumber = i_LisenceNumber;
        }

        public string ManufactureName
        {
            get
            {
                return m_ModelName;
            }
        }

        public string LisenceNumber
        {
            get
            {
                return m_LisenceNumber;
            }
        }

        public float EnergyPercentageLeft
        {
            get
            {
                return m_Engine.CurrentEnergyAmount;
            }
        }

        public EnergyTank Engine
        {
            get
            {
                return m_Engine;
            }

            set
            {
                m_Engine = value;
            }
        }

        public List<Tire> Tires
        {
            get
            {
                return m_Tires;
            }
            set
            {
                if (hasRequiredNumberOfTires(value) is false)
                {
                    //throw exception
                }
                else if (areAllNewTiresAtCorrectMaxPressure(value) is false)
                {
                    //throw exception
                }
                else
                {
                    m_Tires = value;
                }
            }
        }

        public abstract int MaxTireAirPressure { get; }
        public abstract int NumOfTires { get; }

        public abstract float MaxFuelAmount { get; }

        public abstract float MaxTimeBatteryCanLast { get; }

        public abstract FuelTank.eFuelType FuelType{ get; }



        public override int GetHashCode()
        {
            return m_LisenceNumber.GetHashCode();
        }

        private bool areAllNewTiresAtCorrectMaxPressure(List<Tire> i_NewTires)
        {
            bool isTiresAtCorrectMaxPressure = true;

            foreach (Tire tire in i_NewTires)
            {
                if (doesTireHasCorrectMaxPressure(tire) is false)
                {
                    isTiresAtCorrectMaxPressure = false;
                    break;
                }
            }

            return isTiresAtCorrectMaxPressure;
        }

        protected abstract bool doesTireHasCorrectMaxPressure(Tire i_Tire);

        protected abstract bool hasRequiredNumberOfTires(List<Tire> i_Tires);
    }
}