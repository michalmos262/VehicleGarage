using System;

namespace Ex03.GarageLogic
{
    internal class Battery : EnergyTank
    {
        private readonly BatteryTimeUnitHours r_BatteryTimeUnitHours;

        public enum BatteryTimeUnitHours
        {
            Day = 24,
            Hour = 1,
            Minute = 1/60,
        }

        public Battery(float i_MaximumTimeBatteryCanLast, BatteryTimeUnitHours i_BatteryTimeUnit) : base(i_MaximumTimeBatteryCanLast)
        {
            if (Enum.IsDefined(typeof(BatteryTimeUnitHours), i_BatteryTimeUnit))
            {
                r_BatteryTimeUnitHours = i_BatteryTimeUnit;
            }
            // TODO: excpetion ???
        }

        public void Recharge(float i_AdditionalBatteryTimeInHours)
        {
            CurrentEnergyAmount += i_AdditionalBatteryTimeInHours * (1 / (float)i_AdditionalBatteryTimeInHours);
        }
    }
}
