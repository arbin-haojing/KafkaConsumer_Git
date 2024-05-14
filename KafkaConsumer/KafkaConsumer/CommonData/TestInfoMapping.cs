using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel.Common
{
    /// <summary>
    /// Details of mapping in test information
    /// </summary>
    public class TestInfoMapping 
    {
        /// <summary>
        /// List of auxiliary mapping information
        /// </summary>
        public List<AuxMapping> Mappings { get; set;} = new List<AuxMapping>();
    }
}
