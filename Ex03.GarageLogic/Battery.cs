using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Battery : EnergyTank
    {
        public Battery(float i_MaximumTimeBatteryCanLast) : base(i_MaximumTimeBatteryCanLast)
        {

        }

        public override void ReEnergize(float i_AdditionalEnergeyAmount, FuelTank.eFuelType? i_FuelType = null)
        {
            if (i_FuelType.HasValue)
            {
                // TODO: cant fuel a battery
            }
            CurrentEnergyAmount += i_AdditionalEnergeyAmount;
        }

        public override Dictionary<string, string> GetSpecificEnergeyTypeDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("Energey type", "Electricity");
            details.Add("Hours left in battery", m_CurrentEnergyAmount.ToString());

            return details;
        }
    }
}