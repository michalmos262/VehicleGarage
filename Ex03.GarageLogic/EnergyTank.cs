namespace Ex03.GarageLogic
{
    public abstract class EnergyTank
    {
        protected float m_CurrentEnergyAmount;
        protected readonly float r_MaxEnergyAmount;

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
                    // TODO: add exception
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

        public abstract void ReEnegrize(float i_AdditionalEnergeyAmount, FuelTank.eFuelType? i_FuelType = null);
    }
}