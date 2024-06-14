using System;

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

        public override void ReEnegrize(float i_AdditionalEnergeyAmount, FuelTank.eFuelType? i_FuelType = null)
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
    }
}