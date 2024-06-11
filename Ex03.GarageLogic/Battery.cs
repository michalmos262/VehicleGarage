namespace Ex03.GarageLogic
{
    public class Battery : EnergyTank
    {
        public Battery(float i_MaximumTimeBatteryCanLast) : base(i_MaximumTimeBatteryCanLast)
        {

        }

        public void Recharge(float i_AdditionalBatteryTimeInHours)
        {
            CurrentEnergyAmount += i_AdditionalBatteryTimeInHours;
        }
    }
}
