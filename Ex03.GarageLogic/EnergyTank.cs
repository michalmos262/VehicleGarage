namespace Ex03.GarageLogic
{
    abstract public class EnergyTank
    {
        private float m_CurrentEnergyAmount;
        private readonly float r_MaxEnergyAmount;

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
                // TODO: add exception
            }
        }

        public float MaxEnergyAmount
        {
            get
            {
                return r_MaxEnergyAmount;
            }
        }
    }
}
