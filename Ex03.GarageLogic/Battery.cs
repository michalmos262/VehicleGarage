using System;

namespace Ex03.GarageLogic
{
    internal class Battery : EnergyTank
    {
        private readonly BatteryTimeUnit r_BatteryTimeUnit;

        public enum BatteryTimeUnit
        {
            Days,
            Hours,
            Mintues,
        }

        public Battery(float i_MaxFuelAmount, BatteryTimeUnit i_BatteryTimeUnit) : base(i_MaxFuelAmount)
        {
            if (Enum.IsDefined(typeof(BatteryTimeUnit), i_BatteryTimeUnit))
            {
                r_BatteryTimeUnit = i_BatteryTimeUnit;
            }
            // TODO: excpetion ???
        }

    }
}
