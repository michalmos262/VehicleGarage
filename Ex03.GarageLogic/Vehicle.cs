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

        protected Vehicle(string i_LisenceNumber)
        {
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

        public override int GetHashCode()
        {
            return m_LisenceNumber.GetHashCode();
        }

        public void AddTire(Tire i_Tire)
        {
            m_Tires.Add(i_Tire);
        }

        public void InsertTires(List<Tire> i_Tires)
        {
            if (hasRequiredNumberOfTires(i_Tires) is false)
            {
                //exception
            }
            else if (areAllTiresAtCorrectMaxPressure(i_Tires) is false)
            {
                //exception
            }
            else
            {
                m_Tires = i_Tires;
            }
        }

        private bool areAllTiresAtCorrectMaxPressure(List<Tire> i_Tires)
        {
            bool isTiresAtCorrectMaxPressure = true;

            foreach (Tire tire in i_Tires)
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