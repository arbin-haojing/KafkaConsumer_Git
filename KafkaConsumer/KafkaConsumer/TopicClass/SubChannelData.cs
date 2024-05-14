using KafkaConsumer;
using KafkaConsumer.TopicClass;
using System;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Log detail data for IV channel
    /// </summary>
    [Serializable]
    public class SubChannelData : ArbinDataModelBase
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
        /// Barcode
        /// </summary>
        public string Barcode {get; set;} = string.Empty;

        /// <summary>
        /// Test name
        /// </summary>
        public string TestName {get; set;} = string.Empty;

        /// <summary>
        /// Test ID, starting from 1
        /// </summary>
        public int TestID {get; set;} = -1;

        /// <summary>
        /// Indicates when this data was recorded for this test
        /// </summary>
        public long DateTime {get; set;} = 0;

        /// <summary>
        /// Indicates the position of this data in this test
        /// </summary>
        public long DataPoint {get; set;} = 0;

        /// <summary>
        /// Channel status
        /// </summary>
        /// <see cref="EChannelStatus"/>
        public string Status {get; set;} = EChannelStatus.Idle.ToString();

        /// <summary>
        /// Total time this test has been running at the time this data was recorded
        /// </summary>
        public double TestTime {get; set;} = 0;

        /// <summary>
        /// Total time this step has been running at the time this data was recorded
        /// </summary>
        public double StepTime {get; set;} = 0;

        /// <summary>
        /// Step ID, starting from 1
        /// </summary>
        public int StepID {get; set;} = -1;

        /// <summary>
        /// SubStep ID, starting from 1
        /// </summary>
        public int SubStepID {get; set;} = -1;

        /// <summary>
        /// Cycle ID, starting from 1
        /// </summary>
        public int CycleID {get; set;} = -1;

        /// <summary>
        /// Voltage
        /// </summary>
        public float Voltage {get; set;} = 0;

        /// <summary>
        /// Current
        /// </summary>
        public float Current {get; set;} = 0;

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
        public double ChargeCapacity {get; set;} = 0;

        /// <summary>
        /// DischargeCapacity
        /// </summary>
        public double DischargeCapacity {get; set;} = 0;

        /// <summary>
        /// ChargeEnergy
        /// </summary>
        public double ChargeEnergy {get; set;} = 0;

        /// <summary>
        /// DischargeEnergy
        /// </summary>
        public double DischargeEnergy {get; set;} = 0;

        /// <summary>
        /// Data flags
        /// </summary>
        public int DataFlags {get; set;} = 0;

        /// <summary>
        /// Limit ID
        /// </summary>
        public float LimitID {get; set;} = float.MaxValue;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public SubChannelData()
        {
        }

    }
}
