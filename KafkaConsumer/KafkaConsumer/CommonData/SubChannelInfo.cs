using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    [Serializable]
    public class SubChannelInfo
    {
        /// <summary>
        /// SubChannelID
        /// </summary>
        public int SubChannelID { get; set; } = 0;
        /// <summary>
        /// Current
        /// </summary>
        public float Current { get; set; } = 0;
        /// <summary>
        /// Voltage
        /// </summary>
        public float Voltage { get; set; } = 0;

        /// <summary>
        /// Base Refer Voltage
        /// </summary>
        public float BRefVoltage { get; set; } = 0;
        /// <summary>
        /// Delta Voltage
        /// </summary>
        public float DeltaVoltage { get; set; } = 0;

        /// <summary>
        /// ChargeCapacity
        /// </summary>
        public double ChargeCapacity { get; set; } = 0;

        /// <summary>
        /// DischargeCapacity
        /// </summary>
        public double DischargeCapacity { get; set; } = 0;

        /// <summary>
        /// ChargeEnergy
        /// </summary>
        public double ChargeEnergy { get; set; } = 0;

        /// <summary>
        /// DischargeEnergy
        /// </summary>
        public double DischargeEnergy { get; set; } = 0;
    }
}
