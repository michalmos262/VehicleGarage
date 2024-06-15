using System.Collections.Generic;
using Ex03.GrarageLogic;

namespace Ex03.GarageLogic
{
    public abstract class EnergyTank
    {
        protected float m_CurrentEnergyAmount;
        protected readonly float r_MaxEnergyAmount;
        protected const float k_MinEnergyAmount = 0;

        public EnergyTank(float i_MaxEnergyAmount)
        {
            r_MaxEnergyAmount = i_MaxEnergyAmount;
            m_CurrentEnergyAmount = 0;
        }

        public float CurrentEnergyAmount
        {
            get
            { 
                return m_CurrentEnergyAmount; 
            }
            set 
            {
                if (value <= r_MaxEnergyAmount)
                {
                    m_CurrentEnergyAmount = value;
                }
                else
                {
                    throw new ValueOutOfRangeException(k_MinEnergyAmount, r_MaxEnergyAmount);
                }
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return r_MaxEnergyAmount;
            }
        }

        public abstract void ReEnergize(float i_AdditionalEnergyAmount, FuelTank.eFuelType? i_FuelType = null);

        public abstract Dictionary<string, string> GetSpecificEnergyTypeDetails();
    }
}