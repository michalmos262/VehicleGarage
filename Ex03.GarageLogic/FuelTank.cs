using System;

namespace Ex03.GarageLogic
{
    internal class FuelTank : EnergyTank
    {
        private readonly eFuelType r_FuelType;
        private readonly eFuelUnit r_FuelUnit;

        public enum eFuelType
        {
            Octan95,
            Octan96,
            Octan98,
            Soler
        }

        public enum eFuelUnit
        {
            Litres = 1,
            Gallons = (int)0.264172f
        }

        public FuelTank(float i_MaxFuelAmount, eFuelType i_FuelType, eFuelUnit i_FuelUnit) : base(i_MaxFuelAmount)
        {
            if (Enum.IsDefined(typeof(eFuelType), i_FuelType) && Enum.IsDefined(typeof(eFuelUnit), i_FuelUnit))
            {
                r_FuelType = i_FuelType;
                r_FuelUnit = i_FuelUnit;
            }
            // TODO: excpetion ???
        }

        public void Refuel(float i_AdditionalFuelAmountInLiters, eFuelType i_FuelType)
        {
            if (r_FuelType == i_FuelType)
            {
                CurrentEnergyAmount += (i_AdditionalFuelAmountInLiters * (float)r_FuelUnit);
            }
            // TODO: else, execption
        }
    }
}
