using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public abstract class Vehicle
    {
        protected string m_ModelName;
        protected string m_LicenseNumber;
        protected List<Tire> m_Tires;
        protected EnergyTank m_Engine;
        protected List<string> m_SpecificVehicleDetailsRequirementsAsStrings;
        protected const float k_MinEngineVolume = 0;

        protected Vehicle(string i_LicenseNumber)
        {
            m_LicenseNumber = i_LicenseNumber;
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

        public string LicenseNumber
        {
            get
            {
                return m_LicenseNumber;
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
                if (!hasRequiredNumberOfTires(value))
                {
                    throw new ArgumentException(string.Format("Invalid tires list length!"));
                }
                if (!areAllNewTiresAtCorrectMaxPressure(value))
                {
                    throw new ArgumentException(string.Format("Not all tires have the right max air pressure!"));
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

        public override int GetHashCode()
        {
            return m_LicenseNumber.GetHashCode();
        }

        public abstract float MaxTireAirPressure { get; }

        public abstract int NumOfTires { get; }

        public abstract float MaxFuelAmount { get; }

        public abstract float MaxTimeBatteryCanLastInHours { get; }

        public abstract FuelTank.eFuelType FuelType{ get; }

        protected abstract bool doesTireHasCorrectMaxPressure(Tire i_Tire);

        protected abstract bool hasRequiredNumberOfTires(List<Tire> i_Tires);

        public abstract void VerifyAndSetAllSpecificVehicleTypeDetails(List<string> i_VehicleTypeDetails);

        public abstract Dictionary<string, string> GetSpecificVehicleTypeDetails();

        public abstract List<string> RequestAdditionalVehicleDetails();
    }
}