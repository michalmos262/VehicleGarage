using System;
using System.Collections.Generic;

namespace Ex03.GarageLogic
{
    public class FuelTank : EnergyTank
    {
        private readonly eFuelType r_FuelType;

        public enum eFuelType
        {
            Octan95,
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
            // TODO: excpetion ???
        }

        public override void ReEnergize(float i_AdditionalEnergeyAmount, FuelTank.eFuelType? i_FuelType = null)
        {
            if (!i_FuelType.HasValue)
            {
                throw new ArgumentNullException(nameof(i_FuelType));
            }
            else if (i_FuelType != r_FuelType)
            {
                // TODO: execption
            }
            else
            {
                CurrentEnergyAmount += i_AdditionalEnergeyAmount;
            }
        }

        public override Dictionary<string, string> GetSpecificEnergeyTypeDetails()
        {
            Dictionary<string, string> details = new Dictionary<string, string>();
            
            details.Add("Energey type", r_FuelType.ToString());
            details.Add("Liters left in tank", m_CurrentEnergyAmount.ToString());

            return details;
        }
    }
}