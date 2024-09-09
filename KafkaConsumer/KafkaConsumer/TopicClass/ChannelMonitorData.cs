using Arbin.Library.DataModel.Common;
using KafkaConsumer;
using KafkaConsumer.TopicClass;
using System;
using System.Collections.Generic;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Channel monitor data
    /// </summary>
    [Serializable]
    public class ChannelMonitorData : ArbinDataModelBase
    {
        /// <summary>
        /// Can be serial number(Specify the cycler), barcode or whatever
        /// </summary>
        //public string ID { get; set; } = string.Empty;

        ///// <summary>
        ///// Task ID: The server returns this task ID, which is used to determine if this response is a match for this request command
        ///// </summary>
        ///// <remarks>Applies only to one receive and one send, not to subscription commands</remarks>
        //public long TaskID { get; set; } = 0;
        /// <summary>
        /// ChannelID ID, starting from 1
        /// </summary>
        public int ChannelID {get; set;} = -1;

        /// <summary>
        /// Channel status
        /// </summary>
        /// <see cref="EChannelStatus"/>
        public string Status {get; set;} = EChannelStatus.Idle.ToString();

        /// <summary>
        /// Whether the communication has failed
        /// </summary>
        /// <value>
        /// <c>true</c>: Communication failure with Firmware<br></br>
        /// <c>false</c>:Successful communication with Firmware 
        /// </value>
        public bool CommunicationFailure {get; set;} = true;

        /// <summary>
        /// Schedule name
        /// </summary>
        public string ScheduleName {get; set;} = string.Empty;

        /// <summary>
        /// TestObject name
        /// </summary>
        public string TestObjectName {get; set;} = string.Empty;

        /// <summary>
        /// CANBMS name
        /// </summary>
        public string CANBMSName {get; set;} = string.Empty;

        /// <summary>
        /// SMB name
        /// </summary>
        public string SMBName {get; set;} = string.Empty;

        /// <summary>
        /// Chart name
        /// </summary>
        public string ChartName {get; set;} = string.Empty;

        /// <summary>
        /// Test name
        /// </summary>
        public string TestName {get; set;} = string.Empty;

        /// <summary>
        /// Exit condition
        /// </summary>
        public string ExitCondition {get; set;} = string.Empty;

        /// <summary>
        /// StepID and cycleID
        /// </summary>
        public string StepAndCycle {get; set;} = string.Empty;

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
        /// Barcode
        /// </summary>
        public string Barcode {get; set;} = string.Empty;

        /// <summary>
        /// Master Channel ID, ChannelID is not equal to MasterChannelID , which means that this channel is a parallel subchannel
        /// </summary>
        public int MasterChannelID {get; set;} = -1;

        /// <summary>
        /// Total time this test has been running at the time this data was recorded
        /// </summary>
        public double TestTime {get; set;} = 0;

        /// <summary>
        /// Total time this step has been running at the time this data was recorded
        /// </summary>
        public double StepTime {get; set;} = 0;

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
        /// Nominal capacity of the testobject
        /// </summary>
        public double NominalCapacityOfTestObject {get; set;} = 0;

        /// <summary>
        /// The current maximum value of the testobject
        /// </summary>
        public double CurrentMaxOfTestObject {get; set;} = 0;

        /// <summary>
        /// The voltage maximum value of the testobject
        /// </summary>
        public double VoltageMaxOfTestObject {get; set;} = 0;

        /// <summary>
        /// The voltage minimum value of the testobject
        /// </summary>
        public double VoltageMinOfTestObject {get; set;} = 0;

        /// <summary>
        /// Nominal voltage of the testobject
        /// </summary>
        public double NominalVoltageOfTestObject {get; set;} = 0;

        /// <summary>
        /// Nominal capacitance of the testobject
        /// </summary>
        public double NominalCapacitanceOfTestObject {get; set;} = 0;

        /// <summary>
        /// Nominal IR of the testobject
        /// </summary>
        public double NominalIROfTestObject {get; set;} = 0;

        /// <summary>
        /// Specific capacity of the testobject
        /// </summary>
        public double SpecificCapacityOfTestObject {get; set;} = 0;

        /// <summary>
        /// Is auto calculate of the testobject
        /// </summary>
        public bool IsAutoCalculateOfTestObject {get; set;} = false;

        /// <summary>
        /// Mass of the testobject
        /// </summary>
        public double MassOfTestObject {get; set;} = 0;

        /// <summary>
        /// Charge capacity
        /// </summary>
        public double ChargeCapacity {get; set;} = 0;

        /// <summary>
        /// Discharge capacity
        /// </summary>
        public double DischargeCapacity {get; set;} = 0;

        /// <summary>
        /// Charge energy
        /// </summary>
        public double ChargeEnergy {get; set;} = 0;

        /// <summary>
        /// Discharge energy
        /// </summary>
        public double DischargeEnergy {get; set;} = 0;

        /// <summary>
        /// Internal resistance
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
        /// ACR
        /// </summary>
        public float ACR {get; set;} = 0;

        /// <summary>
        /// ACI
        /// </summary>
        public float ACI {get; set;} = 0;

        /// <summary>
        /// AC IPhase
        /// </summary>
        public float ACIPhase {get; set;} = 0;

        /// <summary>
        /// List of MVUD
        /// </summary>
        public List<double> MVUDs {get; set;} = new List<double>();

        /// <summary>
        /// List of CAN
        /// </summary>
        public List<CANMonitorInfo> CANs {get; set;} = new List<CANMonitorInfo>();

        /// <summary>
        /// List of SMB
        /// </summary>
        public List<SMBMonitorInfo> SMBs {get; set;} = new List<SMBMonitorInfo>();

        /// <summary>
        /// List of Auxiliary
        /// </summary>
        public List<AuxData> Auxs {get; set;} = new List<AuxData>();
        public List<SubChannelInfo> SubChannels { get; set;} = new List<SubChannelInfo>();
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
        public ChannelMonitorData()
        {
        }

    }
}
