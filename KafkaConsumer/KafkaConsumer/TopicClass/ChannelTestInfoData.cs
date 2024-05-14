using Arbin.Library.DataModel.Common;
using System.Collections.Generic;
using System;
using KafkaConsumer.TopicClass;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Test information data
    /// </summary>
    [Serializable]
    public class ChannelTestInfoData : ArbinDataModelBase
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
        /// From UTC now to arbin format long time
        /// </summary>
        public long StartFileTime { get; set; } = 0;

        /// <summary>
        /// Indicates the start time of this test (UTC time)
        /// </summary>
        public string StartDateTime {get; set;} = string.Empty;

        /// <summary>
        /// Creator
        /// </summary>
        public string Creator {get; set;} = string.Empty;

        /// <summary>
        /// Comment
        /// </summary>
        public string Comment {get; set;} = string.Empty;

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
        /// CANBMS File Content
        /// </summary>
        public string CANBMSFileContent {get; set;} = string.Empty;

        /// <summary>
        /// SMB name
        /// </summary>
        public string SMBName {get; set;} = string.Empty;

        /// <summary>
        /// SMB File Content
        /// </summary>
        public string SMBFileContent {get; set;} = string.Empty;

        /// <summary>
        /// Capacity
        /// </summary>
        public float Capacity {get; set;} = 0;

        /// <summary>
        /// Specific capacity
        /// </summary>
        public float SpecificCapacity {get; set;} = 0;

        /// <summary>
        /// Specific mass
        /// </summary>
        public float SpecificMass {get; set;} = 0;

        /// <summary>
        /// Software version
        /// </summary>
        public string SoftwareVersion {get; set;} = string.Empty;

        /// <summary>
        /// Firmware version
        /// </summary>
        public string FirmwareVersion {get; set;} = string.Empty;

        /// <summary>
        /// List of simulation file name
        /// </summary>
        public List<string> SimulationFileNames {get; set;} = new List<string>();

        /// <summary>
        /// Mapping information
        /// </summary>
        public List<AuxMapping> Mappings { get; set;} = new List<AuxMapping>();

        /// <summary>
        /// Default Constructors
        /// </summary>
        public ChannelTestInfoData()
        {
        }

    }
}
