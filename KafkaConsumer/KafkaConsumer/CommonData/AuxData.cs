using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// Details of Auxiliary data
    /// </summary>
    [Serializable]
    public class AuxData 
    {
        /// <summary>
        /// Auxiliary type
        /// </summary>
        public string AuxType {get; set;} = string.Empty;

        /// <summary>
        /// Alias Name
        /// </summary>
        public string AliasName {get; set;} = string.Empty;

        /// <summary>
        /// Auxiliary's global channel ID, starting from 1
        /// </summary>
        public int AuxChGlobalID {get; set;} = -1;

        /// <summary>
        /// Auxiliary's virtual channel ID, starting from 1
        /// </summary>
        public int AuxChVirtualID {get; set;} = -1;

        /// <summary>
        /// Value
        /// </summary>
        public float Value {get; set;} = 0;

        /// <summary>
        /// First-order derivatives of auxiliary value
        /// </summary>
        public float dxdt {get; set;} = 0;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public AuxData()
        {
        }

        
    }
}
