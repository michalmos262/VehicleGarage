using System;

namespace Ex03.GarageLogic
{
    internal class VehicleRecordInGarage
    {
        private Vehicle m_Vehicle;
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        public VehicleRecordInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public string LisenceNumber
        {
            get
            {
                return m_Vehicle.LisenceNumber;
            }
        }

        public eVehicleStatus VehicleStatus
        {
            get
            {
                return m_VehicleStatus;
            }
            set
            {
                if (Enum.IsDefined(typeof(eVehicleStatus), value))
                {
                    m_VehicleStatus = value;
                }
                else
                {
                    throw new ArgumentException("Vehicle status is not correct");
                }
            }
        }
    }
}
