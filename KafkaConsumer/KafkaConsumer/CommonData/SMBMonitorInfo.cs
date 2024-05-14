using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// SMB monitor information
    /// </summary>
    [Serializable]
    public class SMBMonitorInfo : SMBInfo
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
        public SMBMonitorInfo()
        {
        }

    }
}
