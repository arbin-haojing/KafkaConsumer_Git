using Arbin.Library.DataModel.Common;
using KafkaConsumer;
using KafkaConsumer.TopicClass;
using System;
using System.Collections.Generic;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Log detail data for IV channel
    /// </summary>
    [Serializable]
    public class ChannelData : ArbinDataModelBase
    {
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
        /// Power
        /// </summary>
        public float Power {get; set;} = 0;

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
        /// InternalResistance
        /// </summary>
        public float InternalResistance {get; set;} = 0;

        /// <summary>
        /// dVdt
        /// </summary>
        public float dVdt {get; set;} = 0;

        /// <summary>
        /// dQdV
        /// </summary>
        public float dQdV {get; set;} = 0;

        /// <summary>
        /// dVdQ
        /// </summary>
        public float dVdQ {get; set;} = 0;

        /// <summary>
        /// Data flags
        /// </summary>
        public int DataFlags {get; set;} = 0;

        /// <summary>
        /// Limit ID
        /// </summary>
        public float LimitID {get; set;} = float.MaxValue;

        /// <summary>
        /// List of MVUD
        /// </summary>
        public List<double> MVUDs {get; set;} = new List<double>();

        /// <summary>
        /// List of  TC_Counter
        /// </summary>
        public List<int> TCCounters {get; set;} = new List<int>();

        /// <summary>
        /// List of CAN
        /// </summary>
        public List<CANInfo> CANs {get; set;} = new List<CANInfo>();

        /// <summary>
        /// List of SMB
        /// </summary>
        public List<SMBInfo> SMBs {get; set;} = new List<SMBInfo>();

        /// <summary>
        /// List of Auxiliary
        /// </summary>
        public List<AuxData> Auxs {get; set;} = new List<AuxData>();
        /// <summary>
        /// DNLC
        /// </summary>
        public float DNLC { get; set; } = 0;
        /// <summary>
        /// DNC
        /// </summary>
        public float DNC { get; set; } = 0;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public ChannelData()
        {
        }

    }
}
