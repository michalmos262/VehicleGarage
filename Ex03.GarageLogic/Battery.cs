using Ex03.GrarageLogic;
using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Battery : EnergyTank
    {
        public Battery(float i_MaximumTimeBatteryCanLast) : base(i_MaximumTimeBatteryCanLast)
        {

        }

        public override void ReEnergize(float i_AdditionalEnergyAmount, FuelTank.eFuelType? i_FuelType = null)
        {
            if (i_FuelType.HasValue)
            {
                throw new ArgumentException(string.Format("Can't add fuel to a vehicle uses battery"));
            }
            CurrentEnergyAmount += i_AdditionalEnergyAmount;
        }

        public override Dictionary<string, string> GetSpecificEnergyTypeDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("Energy type", "Electricity");
            details.Add("Hours left in battery", m_CurrentEnergyAmount.ToString());

            return details;
        }
    }
}