using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelTank : EnergyTank
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Octan95 = 1,
            Octan96,
            Octan98,
            Soler
        }

        public FuelTank(float i_MaxFuelAmount, eFuelType i_FuelType) : base(i_MaxFuelAmount)
        {
            if (Enum.IsDefined(typeof(eFuelType), i_FuelType))
            {
                r_FuelType = i_FuelType;
            }
            else
            {
                throw new ArgumentException("Fuel type is not correct!");
            }
        }

        public override void ReEnergize(float i_AdditionalEnergyAmount, FuelTank.eFuelType? i_FuelType = null)
        {
            if (!i_FuelType.HasValue)
            {
                throw new ArgumentException(string.Format("Can't charge a vehicle uses fuel!"));
            }
            if (i_FuelType != r_FuelType)
            {
                throw new ArgumentException("Invalid fuel type!");
            }
            CurrentEnergyAmount += i_AdditionalEnergyAmount;
        }

        public override Dictionary<string, string> GetSpecificEnergyTypeDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();

            details.Add("Energy type", r_FuelType.ToString());
            details.Add("Liters left in tank", m_CurrentEnergyAmount.ToString());

            return details;
        }
    }
}