using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// Simulation information
    /// </summary>
    public class SimulationInfo 
    {
        /// <summary>
        /// Simulation name
        /// </summary>
        public string SimulationName {get; set;} = string.Empty;

        /// <summary>
        /// Extended information
        /// </summary>
        public string ExInfos { get; set; } = string.Empty;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public SimulationInfo()
        {
        }

    }
}
