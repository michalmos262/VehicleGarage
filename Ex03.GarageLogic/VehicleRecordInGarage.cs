using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Ex03.GarageLogic
{
    internal class VehicleRecordInGarage
    {
        private readonly string m_LisenceNumber;
        private readonly string m_OwnerName;
        private readonly string m_OwnerPhoneNumber;
        private eVehicleStatus m_VehicleStatus;

        public enum eVehicleStatus
        {
            InRepair,
            Repaired,
            Paid
        }

        public string LisenceNumber
        {
            get
            {
                return m_LisenceNumber;
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
                    throw new ArgumentException("Enum does not exits");
                }
            }
        }
    }
}
