using Arbin.Library.DataModel;
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
    public class CFileTopicInfo : ArbinDataModelBase
    {
        public string FileName { get; set; }
        public string MD5 { get; set; }
        public string Content { get; set; }
        public string Creator { get; set; }
        public string CreatedTime { get; set; }
        public string LastModifyTime { get; set; }
        public string LastModifier { get; set; }
        public string Version { get; set; }
    }
}
