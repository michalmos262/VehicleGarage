using System;
using System.Linq;

namespace Ex03.GarageLogic
{
    public class VehicleRecordInGarage
    {
        private Vehicle m_Vehicle;
        private string m_OwnerName;
        private string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public VehicleRecordInGarage(Vehicle i_Vehicle, string i_OwnerName, string i_OwnerPhoneNumber)
        {
            m_Vehicle = i_Vehicle;
            m_OwnerName = i_OwnerName;
            m_OwnerPhoneNumber = i_OwnerPhoneNumber;
            m_VehicleStatus = eVehicleStatus.InRepair;
        }

        public enum eVehicleStatus
        {
            InRepair = 1,
            Repaired,
            Paid
        }

        public Vehicle Vehicle
        {
            get
            {
                return m_Vehicle;
            }
        }

        public string OwnerName
        {
            get
            {
                return m_OwnerName;
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
