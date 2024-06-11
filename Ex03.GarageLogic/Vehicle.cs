using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ManufactureName;
        protected string m_LisenceNumber;
        protected float m_EnergyPercentageLeft;
        protected List<Tire> m_Tires;
        protected PowerSource m_Engine;
        protected readonly int k_MaxTireAirPressure;
        protected readonly float k_MaxFuelAmount;
        protected readonly float k_MaxBatteryTimeInHours;

        protected Vehicle(string i_LisenceNumber)
        {
            m_LisenceNumber = i_LisenceNumber;
        }

        public string ManufactureName
        {
            get
            {
                return m_ManufactureName;
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
                return m_EnergyPercentageLeft;
            }
        }

        public PowerSource Engine
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
    }
}