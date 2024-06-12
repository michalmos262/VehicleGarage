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

        public void Refuel(float i_AdditionalFuelAmountInLiters, eFuelType i_FuelType)
        {
            if (r_FuelType == i_FuelType)
            {
                CurrentEnergyAmount += i_AdditionalFuelAmountInLiters;
            }
            // TODO: else, execption
        }
    }
}