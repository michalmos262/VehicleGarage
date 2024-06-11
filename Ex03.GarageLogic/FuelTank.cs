﻿using System;

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
            Litres,
            Gallons
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

        public void ReFuel(float i_AdditionalFuelAmount, eFuelType i_FuelType)
        {
            if (r_FuelType == i_FuelType)
            {
                CurrentEnergyAmount += i_AdditionalFuelAmount;
            }
            // TODO: else, execption
        }
    }
}
