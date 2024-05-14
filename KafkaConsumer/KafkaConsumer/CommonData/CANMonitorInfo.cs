using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// CAN monitor information
    /// </summary>
    [Serializable]
    public class CANMonitorInfo : CANInfo
    {
        /// <summary>
        /// Offline or not
        /// </summary>
        /// <value>
        /// <c>true</c>: Offline
        /// <c>false</c>: Online
        /// </value>
        public bool IsOffline {get; set;} = false;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public CANMonitorInfo()
        {
        }

    }
}
