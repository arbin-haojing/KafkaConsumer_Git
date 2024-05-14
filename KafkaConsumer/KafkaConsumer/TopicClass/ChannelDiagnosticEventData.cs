using KafkaConsumer.TopicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Diagnostic event data
    /// </summary>
    [Serializable]
    public class ChannelDiagnosticEventData : ArbinDataModelBase
    {
        ///// <summary>
        ///// Can be serial number(Specify the cycler), barcode or whatever
        ///// </summary>
        //public string ID { get; set; } = string.Empty;

        /////// <summary>
        /////// Task ID: The server returns this task ID, which is used to determine if this response is a match for this request command
        /////// </summary>
        /////// <remarks>Applies only to one receive and one send, not to subscription commands</remarks>
        //public long TaskID { get; set; } = 0;
        /// <summary>
        /// IV Global ChannelID, starting from 1
        /// </summary>
        public int ChannelID {get; set;} = -1;

        /// <summary>
        /// Diagnostic messages
        /// </summary>
        public string DiagnosticEventMsg {get; set;} = string.Empty;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public ChannelDiagnosticEventData()
        {
        }

    }
}
