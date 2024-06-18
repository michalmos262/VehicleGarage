using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class Battery : EnergyTank
    {
        public Battery(float i_MaximumTimeBatteryCanLast) : base(i_MaximumTimeBatteryCanLast)
        {

        }

        public override void ReEnergize(float i_AdditionalChargingTimeInHours, FuelTank.eFuelType? i_FuelType = null)
        {
            if (i_FuelType.HasValue)
            {
                throw new ArgumentException("Can't refuel a vehicle uses battery!");
            }
            CurrentEnergyAmount += i_AdditionalChargingTimeInHours;
        }

        public override Dictionary<string, string> GetSpecificEnergyTypeDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>
            {
                { "Energy type", "Electricity" },
                { "Hours left in battery", m_CurrentEnergyAmount.ToString() }
            };

            return details;
        }
    }
}