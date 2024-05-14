using Arbin.Library.DataModel;
using Avro.Generic;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class ConsumerJson<T>
    {
        ConsumerConfig configComsume;
        CachedSchemaRegistryClient m_CachedSchemaRegistryClient;
        SchemaRegistryConfig m_SchemaRegistryConfig;
        string m_LogPath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Log.txt";
        CommonHelper commonHelper;
        string m_ExportDataPath;
        public ConsumerJson(string _ExportDataPath, ConsumerConfig _configComsume, CachedSchemaRegistryClient _CachedSchemaRegistryClient, SchemaRegistryConfig _schemaRegistryConfig)
        {
            m_ExportDataPath = _ExportDataPath;
            configComsume = _configComsume;
            m_CachedSchemaRegistryClient = _CachedSchemaRegistryClient;
            m_SchemaRegistryConfig = _schemaRegistryConfig;
            commonHelper = new CommonHelper(m_ExportDataPath, m_CachedSchemaRegistryClient);
        }
        public void ConsumerTopic(EKafkaTopic eKafkaTopic, bool m_UseSerialNumber, bool m_bTestName, bool m_bTestID, bool m_bChanel, string m_SerialNumber, string m_TestName, string m_TestID, string m_Chanel)
        {
            string strPathFile = $@"{m_ExportDataPath}\{typeof(T).Name}\";
            Directory.CreateDirectory(strPathFile);
            string strPath;
            try
            {
               IConsumer<string, string> consumer = new ConsumerBuilder<string, string>(configComsume).SetValueDeserializer(Deserializers.Utf8).Build();
                consumer.Subscribe(eKafkaTopic.ToString());
                try
                {
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) => {
                        e.Cancel = true;
                        cancellationTokenSource.Cancel();
                    }; 
                    bool bSelectKey = false;
                    string strSelect = "";
                    if (m_UseSerialNumber)
                        strSelect += m_SerialNumber;
                    if (m_bTestName)
                        strSelect += "_" + m_TestName;
                    bSelectKey = !string.IsNullOrEmpty(strSelect);
                    while (true)
                    {
                        try
                        {
                            strPath = strPathFile;
                            var consumeResult = consumer.Consume(cancellationTokenSource.Token);
                            if (bSelectKey)
                            {
                                if (m_bTestName && !consumeResult.Key.ToLower().Equals(strSelect.ToLower()))
                                    continue;
                                if (!m_bTestName && !consumeResult.Key.ToLower().Contains(strSelect.ToLower()))
                                    continue;
                            };
                            var data  = JsonConvert.DeserializeObject<T>(consumeResult.Message.Value.Replace("\0","").Replace("\b", ""));
                            if (m_bTestID)
                            {
                                PropertyInfo property = data.GetType().GetProperty("TestID");
                                if (property != null)
                                {
                                    if (Convert.ToString(property.GetValue(data)) != m_TestID)
                                        continue;
                                }
                            }
                            if (m_bChanel)
                            {
                                PropertyInfo property = data.GetType().GetProperty("ChannelID");
                                if (property != null)
                                {
                                    if (Convert.ToString(property.GetValue(data)) != m_Chanel)
                                        continue;
                                }
                            }
                            List<string> lstLines = new List<string>();
                            List<string> lstColumn = new List<string>();
                            object value;
                            string strColumnValue;
                            bool bOk = true;
                            string strField;
                            foreach (PropertyInfo property in data.GetType().GetProperties())
                            {
                                strField = property.Name;
                                value = property.GetValue(data);
                                if (property.PropertyType.Name == "List`1")
                                {
                                    strColumnValue = "";
                                    if (strField == $"MVUDs")
                                    {
                                        strColumnValue = string.Join("_", (IEnumerable<double>)value);
                                    }
                                    else if (strField == $"TCCounters")
                                    {
                                        strColumnValue = string.Join("_", (IEnumerable<int>)value);
                                    }
                                    else if (strField == $"SimulationFileNames")
                                    {
                                        strColumnValue = string.Join("_", (IEnumerable<string>)value);
                                    }
                                    else
                                    {
                                        foreach (var item in (IEnumerable<object>)value)
                                        {
                                            List<string> lstInner = new List<string>();
                                            foreach (PropertyInfo itemProperty in item.GetType().GetProperties())
                                            {
                                                lstInner.Add(Convert.ToString(itemProperty.GetValue(item)));
                                            }
                                            strColumnValue += $"[{string.Join("_", lstInner)}]";
                                        }
                                    }
                                }
                                else
                                {
                                    
                                    strColumnValue = Convert.ToString(value);
                                    if (strField == "ID"||
                                        strField == "TestName" ||
                                        strField == "TestID" ||
                                        strField == "ChannelID" ||
                                        strField == "SubChannelID")
                                    {
                                        strPath += $"[{strColumnValue}] ";
                                    }
                                }
                                lstColumn.Add(strColumnValue.Replace(",",""));
                            }
                            lstLines.Add(string.Join(",", lstColumn));
                            bOk = commonHelper.WriteFile_DifferenPath<T>(eKafkaTopic, strPath, ".csv", lstLines, out string newPath, EMessageFormat.JSON);
                            if (!bOk)
                            {
                                Log($"[{eKafkaTopic}]:" + string.Join(",", lstColumn));
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            Log($"[{eKafkaTopic}]:" + ex.Message);
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Log($"[{eKafkaTopic}]:Consumer Close;" + ex.Message);
                    // 用户取消操作，关闭消费者
                    consumer.Close();
                }
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void Log(string mes)
        {
            try
            {
                File.AppendAllLines(m_LogPath, new List<string>() { $"[{DateTime.Now}:]" + mes });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
