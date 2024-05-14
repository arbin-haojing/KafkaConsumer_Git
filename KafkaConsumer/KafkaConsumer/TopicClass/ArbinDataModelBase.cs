using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer.TopicClass
{
    [Serializable]
    public class ArbinDataModelBase
    {
        /// <summary>
        /// Can be serial number(Specify the cycler), barcode or whatever
        /// </summary>
        public string ID { get; set; } = string.Empty;

        /// <summary>
        /// Task ID: The server returns this task ID, which is used to determine if this response is a match for this request command
        /// </summary>
        /// <remarks>Applies only to one receive and one send, not to subscription commands</remarks>
        public long TaskID { get; set; } = 0;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public ArbinDataModelBase()
        {
        }
    }
}
