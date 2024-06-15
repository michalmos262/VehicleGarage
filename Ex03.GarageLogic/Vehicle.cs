using System;
using System.Collections.Generic;
using System.Linq;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LisenceNumber;
        protected List<Tire> m_Tires;
        protected EnergyTank m_Engine;
        protected List<string> m_SpecificVehicleDetailsRequirementsAsStrings;

        protected Vehicle(string i_LisenceNumber)
        {
            m_LisenceNumber = i_LisenceNumber;
        }

        public string ModelName
        {
            get
            {
                return m_ModelName;
            }
            set
            {
                m_ModelName = value;
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

                    //TODO: throw exception
                }
                else if (areAllNewTiresAtCorrectMaxPressure(value) is false)
                {
                    //TODO: throw exception
                }
                else
                {
                    m_Tires = value;
                }
            }
        }

        public List<string> SpecificVehicleDetailsRequirementsAsStrings
        {
            get
            {
                return m_SpecificVehicleDetailsRequirementsAsStrings;
            }
        }

        public abstract int MaxTireAirPressure { get; }

        public abstract int NumOfTires { get; }

        public abstract float MaxFuelAmount { get; }

        public abstract float MaxTimeBatteryCanLastInHours { get; }

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

        public abstract void VerifyAndSetAllSpecificVehicleTypeDetails(List<string> i_SpecificVehicleTypeDetailsStrings);

        public abstract Dictionary<string, string> GetSpecificVehicleTypeDetails();
    }
}