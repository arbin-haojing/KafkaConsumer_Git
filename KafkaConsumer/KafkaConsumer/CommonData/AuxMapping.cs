using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// Details of auxiliary mapping
    /// </summary>
    public class AuxMapping 
    {/// <summary>
     /// Mapping information in TestInfo
     /// </summary>
        public string AuxType {get; set;} = string.Empty;

        /// <summary>
        /// Auxiliary's global channel ID, starting from 1
        /// </summary>
        public int AuxChGlobalID {get; set;} = -1;

        /// <summary>
        /// Auxiliary's virtual channel ID, starting from 1
        /// </summary>
        public int AuxChVirtualID {get; set;} = -1;

        /// <summary>
        /// Alias Name
        /// </summary>
        public string AliasName {get; set;} = string.Empty;

        /// <summary>
        /// Unit
        /// </summary>
        public string Unit {get; set;} = string.Empty;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public AuxMapping()
        {
        }

    }
}
