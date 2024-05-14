using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// CAN data information
    /// </summary>
    [Serializable]
    public class CANInfo
    {
        /// <summary>
        /// Alias Name
        /// </summary>
        public string AliasName {get; set;} = string.Empty;

        /// <summary>
        /// Meta Name: CAN_MV_RX
        /// </summary>
        public string MetaName {get; set;} = string.Empty;

        /// <summary>
        /// Data Type
        /// </summary>
        public int DataType {get; set;} = 0;

        /// <summary>
        /// Value
        /// </summary>
        public string Value {get; set;} = string.Empty;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public CANInfo()
        {
        }

    }
}
