using KafkaConsumer.TopicClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arbin.Library.DataModel
{
    /// <summary>
    /// Event data
    /// </summary>
    [Serializable]
    public class ChannelEventData : ArbinDataModelBase
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
        /// ChannelID ID, starting from 1
        /// </summary>
        public int ChannelID {get; set;} = -1;

        /// <summary>
        /// Test ID, starting from 1
        /// </summary>
        public int TestID {get; set;} = -1;

        /// <summary>
        /// Indicates when this data was recorded for this test
        /// </summary>
        public long DateTime {get; set;} = 0;

        /// <summary>
        /// Event Type
        /// </summary>
        /// <see cref="EEventType"/>
        public int EventType {get; set;} = 0;

        /// <summary>
        /// Event description
        /// </summary>
        public string EventDesc {get; set;} = string.Empty;

        /// <summary>
        /// Total time this test has been running at the time this data was recorded
        /// </summary>
        public double TestTime {get; set;} = 0;

        /// <summary>
        /// Total time this step has been running at the time this data was recorded
        /// </summary>
        public double StepTime {get; set;} = 0;

        /// <summary>
        /// Cycle ID, starting from 1
        /// </summary>
        public int CycleID {get; set;} = -1;

        /// <summary>
        /// Step ID, starting from 1
        /// </summary>
        public int StepID {get; set;} = -1;

        /// <summary>
        /// SubStep ID, starting from 1
        /// </summary>
        public int SubStepID {get; set;} = -1;

        /// <summary>
        /// Limit ID, starting from 1
        /// </summary>
        public int LimitID {get; set;} = -1;

        /// <summary>
        /// Universal Counter 1
        /// </summary>
        public int TCCounter1 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 2
        /// </summary>
        public int TCCounter2 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 3
        /// </summary>
        public int TCCounter3 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 4
        /// </summary>
        public int TCCounter4 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 5
        /// </summary>
        public int TCCounter5 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 6
        /// </summary>
        public int TCCounter6 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 7
        /// </summary>
        public int TCCounter7 {get; set;} = 0;

        /// <summary>
        /// Universal Counter 8
        /// </summary>
        public int TCCounter8 {get; set;} = 0;

        /// <summary>
        /// TC_Time1, TC_Time2, TC_Time3, and TC_Time4 are time counters. 
        /// Time counters can be used to count the total test time of a group of steps. 
        /// Further, the time counters can be used as the step termination limit or logging data limit.
        /// </summary>
        public double TCTimer1 {get; set;} = 0;

        /// <summary>
        /// TC_Time1, TC_Time2, TC_Time3, and TC_Time4 are time counters. 
        /// Time counters can be used to count the total test time of a group of steps. 
        /// Further, the time counters can be used as the step termination limit or logging data limit.
        /// </summary>
        public double TCTimer2 {get; set;} = 0;

        /// <summary>
        /// TC_Time1, TC_Time2, TC_Time3, and TC_Time4 are time counters. 
        /// Time counters can be used to count the total test time of a group of steps. 
        /// Further, the time counters can be used as the step termination limit or logging data limit.
        /// </summary>
        public double TCTimer3 {get; set;} = 0;

        /// <summary>
        /// TC_Time1, TC_Time2, TC_Time3, and TC_Time4 are time counters. 
        /// Time counters can be used to count the total test time of a group of steps. 
        /// Further, the time counters can be used as the step termination limit or logging data limit.
        /// </summary>
        public double TCTimer4 {get; set;} = 0;

        /// <summary>
        /// Default Constructors
        /// </summary>
        public ChannelEventData()
        {
        }

    }
}
