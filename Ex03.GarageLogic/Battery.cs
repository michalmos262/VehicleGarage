namespace Ex03.GarageLogic
{
    public class Battery : EnergyTank
    {
        public Battery(float i_MaximumTimeBatteryCanLast) : base(i_MaximumTimeBatteryCanLast)
        {

        }

        public override void ReEnegrize(float i_AdditionalEnergeyAmount, FuelTank.eFuelType? i_FuelType = null)
        {
            if (i_FuelType.HasValue)
            {
                // TODO: cant fuel a battery
            }
            CurrentEnergyAmount += i_AdditionalEnergeyAmount;
        }
    }
}