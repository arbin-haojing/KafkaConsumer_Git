using Avro;
using Avro.Generic;
using Avro.IO;
using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Confluent.SchemaRegistry.Serdes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class ConsumerAvro<T>
    {
        ConsumerConfig configComsume;
        CachedSchemaRegistryClient m_CachedSchemaRegistryClient;
        SchemaRegistryConfig m_SchemaRegistryConfig;
        string m_LogPath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Log.txt";
        CommonHelper commonHelper;
        string m_ExportDataPath;
        public ConsumerAvro(string _ExportDataPath, ConsumerConfig _configComsume, CachedSchemaRegistryClient _CachedSchemaRegistryClient, SchemaRegistryConfig _schemaRegistryConfig)
        {
            m_ExportDataPath = _ExportDataPath;
            configComsume = _configComsume;
            m_CachedSchemaRegistryClient = _CachedSchemaRegistryClient;
            m_SchemaRegistryConfig = _schemaRegistryConfig;
            commonHelper = new CommonHelper(m_ExportDataPath, m_CachedSchemaRegistryClient);
        }
        public void ConsumerTopic(EKafkaTopic eKafkaTopic, bool m_UseSerialNumber, bool m_bTestName, bool m_bTestID, bool m_bChanel, string m_SerialNumber,string m_TestName,string m_TestID,string m_Chanel)
        {
            string strPathFile = $@"{m_ExportDataPath}\{typeof(T).Name}\";
            Directory.CreateDirectory(strPathFile);
            try
            {
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                IConsumer<string, GenericRecord> consumer = new ConsumerBuilder<string, GenericRecord>(configComsume).SetValueDeserializer(new AvroDeserializer<GenericRecord>(m_CachedSchemaRegistryClient).AsSyncOverAsync()).Build();
                consumer.Subscribe(eKafkaTopic.ToString());
                CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) =>
                {
                    e.Cancel = false; // 防止应用程序关闭
                    cancellationTokenSource.Cancel();
                };
                try
                {
                    bool bSelectKey = false;
                    string strSelect = "";
                    if (m_UseSerialNumber)
                        strSelect += m_SerialNumber;
                    if (m_bTestName)
                        strSelect += "_" + m_TestName;
                    bSelectKey = !string.IsNullOrEmpty(strSelect);
                    int intFilesCount = schema.Fields.Count;
                    List<string> lstFields = schema.Fields.Select(p => p.Name).ToList();
                    string strPath;
                    while (true)
                    {
                        try
                        {
                            ConsumeResult<string, GenericRecord> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                            if (bSelectKey)
                            {
                                if (m_bTestName && !consumeResult.Key.ToLower().Equals(strSelect.ToLower()))
                                    continue;
                                if (!m_bTestName && !consumeResult.Key.ToLower().Contains(strSelect.ToLower()))
                                    continue;
                                //if (!consumeResult.Key.ToLower().Equals(strSelect.ToLower()))
                                //    continue;
                            };
                            bool bOk = true;
                            GenericRecord record = consumeResult.Message.Value;
                            if (m_bTestID && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_TestID)) != m_TestID)
                                continue;
                            if (m_bChanel && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_Chan)) != m_Chanel)
                                continue;
                            strPath = strPathFile;
                            List<string> lstLine = new List<string>();
                            List<string> lstCol = new List<string>();
                            string strValue;
                            string strField;
                            for (int i = 0; i < intFilesCount; i++)
                            {
                                strField = lstFields[i];
                                if (strField == $"ID" ||
                                    strField == $"TestName" ||
                                    strField == $"TestID" ||
                                    strField == $"ChannelID" ||
                                    strField == $"SubChannelID")
                                {
                                    strValue = Convert.ToString(record.GetValue(i));
                                    strPath += $"[{strValue}] ";
                                }
                                else if (strField == $"Auxs" || strField == $"CANs" || strField == $"SMBs" || strField == "Mappings")
                                {
                                    strValue = "";
                                    foreach (var item in (object[])record.GetValue(i))
                                    {
                                        List<string> lstInner = new List<string>();
                                        GenericRecord record_tem = item as GenericRecord;
                                        for (int inner = 0; inner < record_tem.Schema.Fields.Count; inner++)
                                        {
                                            lstInner.Add($"{Convert.ToString(record_tem.GetValue(inner))}");
                                        }
                                        strValue += $"[{string.Join("^", lstInner)}]";
                                        
                                    }
                                }
                                else if (strField == $"MVUDs")
                                {
                                    object[] Value = (object[])record.GetValue(i);
                                    strValue = string.Join("_", Value.Cast<double>().ToArray());
                                }
                                else if (strField == $"TCCounters")
                                {
                                    object[] Value = (object[])record.GetValue(i);
                                    strValue = string.Join("_", Value.Cast<int>().ToArray());
                                }
                                else if (strField == "SimulationFileNames")
                                {
                                    object[] Value = (object[])record.GetValue(i);
                                    strValue = string.Join("_", Value.Cast<string>().ToArray());
                                }
                                else
                                {
                                    strValue = Convert.ToString(record.GetValue(i));
                                }
                                lstCol.Add(strValue.Replace(","," "));
                            }
                            lstLine.Add(string.Join(",", lstCol));
                            bOk = commonHelper.WriteFile_DifferenPath<T>(eKafkaTopic, strPath, ".csv", lstLine, out string newPath, EMessageFormat.AVRO);
                            if (!bOk)
                            {
                                Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                continue;
                            }
                        }
                        catch (Exception ex)
                        {
                            //Log($"[{eKafkaTopic}]:" + ex.Message);
                            //continue;
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
