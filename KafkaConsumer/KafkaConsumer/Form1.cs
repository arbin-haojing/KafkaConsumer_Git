using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Confluent.Kafka;
using System.Threading;
using Avro.IO;
using Avro.Generic;
using Avro;
using System.IO;
using System.Text.RegularExpressions;
using Confluent.SchemaRegistry;

namespace KafkaConsumer
{
    public partial class Form1 : Form
    {
        DataTable m_dtServicesArbin = new DataTable();
        private string BasePath = Environment.CurrentDirectory;
        private string m_GroupId = "Consumer_Arbin_QA";
        private string m_BootstrapServer = "192.168.1.57:9092";
        private string m_SchemaRegisterServer = "192.168.1.57:8081";
        private bool m_UseSerialNumber = false;
        private bool m_ConsumerMonitor = false;
        private string m_SerialNumber = "TestNumber";
        private bool m_bTestName = false;
        private bool m_bChanel = false;
        private bool m_bTestID = false;
        private string m_TestName = "";
        private string m_Chanel = "";
        private string m_TestID = "";
        object objLockFIle = new object();
        string m_ExportDataPath;
        bool m_RefresherColumn = false;
        object objLock = new object();
        int m_PublicColumnCount;
        CachedSchemaRegistryClient m_CachedSchemaRegistryClient;
        Thread thread_Monitor;
        Thread thread_Channel;
        Thread thread_TestInfo;
        Thread thread_Event;
        Thread thread_DiagnosticEvent;
        string m_LogPath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Log.txt";
        Dictionary<EKafkaTopic, int> m_TopicFilesCount = new Dictionary<EKafkaTopic, int>();
        Dictionary<IConsumer<string, byte[]>, Dictionary<int, int>> m_Offset = new Dictionary<IConsumer<string, byte[]>, Dictionary<int, int>>();
        Dictionary<string, int> m_NameMapIndex = new Dictionary<string, int>();
        public Form1()
        {
            InitializeComponent();
            InitConfig();
            BasePath = Environment.CurrentDirectory + "\\";
            InitControl();
            m_ExportDataPath = BasePath + $"\\ExportData\\{txtGroupId.Text}";
            CreateThread();
        }
        private void InitControl()
        {
            try
            {
                txtBootstrapServer.Text = m_BootstrapServer;
                txtSchemaRegisterServer.Text = m_SchemaRegisterServer;
                chkSerialNumber.Checked = m_UseSerialNumber;
                txtSerialNumber.Text = m_SerialNumber;
                txtGroupId.Text = Guid.NewGuid().ToString();
                //txtGroupId.Text = m_GroupId;
                txtSerialNumber.Enabled = chkSerialNumber.Checked;
                chkConsumerMonitor.Checked = m_ConsumerMonitor;
                chkTestName.Checked = m_bTestName;
                chkTestID.Checked = m_bTestID;
                chkChannel.Checked = m_bChanel;
                txtTestName.Enabled = chkTestName.Checked;
                txtChannel.Enabled = chkChannel.Checked;
                txtTestID.Enabled = chkTestID.Checked;
                txtTestName.Text = m_TestName;
                txtChannel.Text = m_Chanel;
                txtTestID.Text = m_TestID;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void InitSchemaRegistryConfig()
        {
            try
            {
                SchemaRegistryConfig schemaRegistryConfig = new SchemaRegistryConfig
                {
                    Url = m_SchemaRegisterServer
                };
                m_CachedSchemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
                InitExportPath();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void InitConfig(bool delete = false)
        {
            string TestDbEfficiency_cfgFileName = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Config.tde";
            try
            {
                if (!File.Exists(TestDbEfficiency_cfgFileName))
                {
                    using (StreamWriter sw = File.CreateText(TestDbEfficiency_cfgFileName))
                    {
                        sw.WriteLine($"BootstrapServers:[{m_BootstrapServer}]");
                        sw.WriteLine($"SchemaRegisterServer:[{m_SchemaRegisterServer}]");
                        sw.WriteLine($"Use SerialNumber:[{m_UseSerialNumber}]");
                        sw.WriteLine($"SerialNumber:[{m_SerialNumber}]");
                        sw.WriteLine($"GroupId:[{m_GroupId}]");
                        sw.WriteLine($"ConsumerMonitor:[{m_ConsumerMonitor}]");
                        sw.WriteLine($"bTestName:[{m_bTestName}]");
                        sw.WriteLine($"bChanel:[{m_bChanel}]");
                        sw.WriteLine($"bTestID:[{m_bTestID}]");
                        sw.WriteLine($"TestName:[{m_TestName}]");
                        sw.WriteLine($"Chanel:[{m_Chanel}]");
                        sw.WriteLine($"TestID:[{m_TestID}]");
                    }
                }
                else
                {
                    if (delete)
                    {
                        File.Delete(TestDbEfficiency_cfgFileName);
                        using (StreamWriter sw = File.CreateText(TestDbEfficiency_cfgFileName))
                        {
                            sw.WriteLine($"BootstrapServers:[{m_BootstrapServer}]");
                            sw.WriteLine($"SchemaRegisterServer:[{m_SchemaRegisterServer}]");
                            sw.WriteLine($"Use SerialNumber:[{m_UseSerialNumber}]");
                            sw.WriteLine($"SerialNumber:[{m_SerialNumber}]");
                            sw.WriteLine($"GroupId:[{m_GroupId}]");
                            sw.WriteLine($"ConsumerMonitor:[{m_ConsumerMonitor}]");
                            sw.WriteLine($"bTestName:[{m_bTestName}]");
                            sw.WriteLine($"bChanel:[{m_bChanel}]");
                            sw.WriteLine($"bTestID:[{m_bTestID}]");
                            sw.WriteLine($"TestName:[{m_TestName}]");
                            sw.WriteLine($"Chanel:[{m_Chanel}]");
                            sw.WriteLine($"TestID:[{m_TestID}]");
                        }
                    }
                }
                using (StreamReader sr = File.OpenText(TestDbEfficiency_cfgFileName))
                {
                    m_BootstrapServer = SetConfig(sr.ReadLine());
                    m_SchemaRegisterServer = SetConfig(sr.ReadLine());
                    m_UseSerialNumber = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_SerialNumber = SetConfig(sr.ReadLine());
                    m_GroupId = SetConfig(sr.ReadLine());
                    m_ConsumerMonitor = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bTestName = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bChanel = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bTestID = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_TestName = SetConfig(sr.ReadLine());
                    m_Chanel = SetConfig(sr.ReadLine());
                    m_TestID = SetConfig(sr.ReadLine());
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
                File.Delete(TestDbEfficiency_cfgFileName);
            }
        }
        private string SetConfig(string input)
        {
            string strValue = "";
            try
            {
                string pattern = @"\[(.*?)\]"; // 正则表达式模式，匹配方括号中间的任意字符
                MatchCollection matches = Regex.Matches(input, pattern);
                foreach (Match match in matches)
                {
                    strValue = match.Groups[1].Value;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }

            return strValue;
        }
        #region 消费主题
        private void CreateThread()
        {
            try
            {
                thread_Monitor = new Thread(new ThreadStart(ConsumerMonitor));
                thread_Monitor.IsBackground = true;
                thread_Channel = new Thread(new ThreadStart(ConsumerChannelTopic));
                thread_Channel.IsBackground = true;
                thread_TestInfo = new Thread(new ThreadStart(ConsumerTestInfoTopic));
                thread_TestInfo.IsBackground = true;
                thread_Event = new Thread(new ThreadStart(ConsumerEventTopic));
                thread_Event.IsBackground = true;
                thread_DiagnosticEvent = new Thread(new ThreadStart(ConsumerDiagnosticEventTopic));
                thread_DiagnosticEvent.IsBackground = true;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void StartThread()
        {
            try
            {
                if (m_ConsumerMonitor)
                {
                    InitDataGridView(); 
                    thread_Monitor.Start();
                }
                thread_Channel.Start();
                thread_TestInfo.Start();
                thread_Event.Start();
                thread_DiagnosticEvent.Start();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void ConsumerMonitor()
        {
            EKafkaTopic eKafkaTopic = EKafkaTopic.Monitor;
            try
            {

                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Latest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };

                using (var consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build())
                {
                    consumer.Subscribe(eKafkaTopic.ToString());

                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = false; // 防止应用程序关闭
                        cancellationTokenSource.Cancel();
                    };

                    try
                    {
                        while (true)
                        {
                            try
                            {
                                m_RefresherColumn = false;
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                {
                                    //consumer.Commit(consumeResult);
                                    continue;
                                }
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    //lock (objLock)
                                    {
                                        while (memoryStream.Position < memoryStream.Length)
                                        {
                                            GenericRecord record = avroReader.Read(null, reader);
                                            int chanPos = (int)EMonitorFieldPos.Channel;
                                            var chan = Convert.ToInt32(record.GetValue(chanPos));
                                            int needAddCount = chan - m_dtServicesArbin.Rows.Count;
                                            if (needAddCount > 0)
                                            {
                                                int colNum = m_dtServicesArbin.Columns.Count;
                                                for (int i = 0; i < needAddCount; i++)
                                                {
                                                    DataRow newRow = m_dtServicesArbin.NewRow();
                                                    for (int col = 0; col < colNum; col++)
                                                    {
                                                        newRow[col] = "";
                                                    }
                                                    m_dtServicesArbin.Rows.Add(newRow);
                                                }
                                            }
                                            DataRow dataRow = m_dtServicesArbin.Rows[chan - 1];
                                            for (int i = 0; i < m_PublicColumnCount; i++)
                                            {
                                                if (i == m_NameMapIndex[$"{eKafkaTopic}_TestTime"] || (i == m_NameMapIndex[$"{eKafkaTopic}_StepTime"]))
                                                {
                                                    dataRow[i] = FormatTime(record.GetValue(i));
                                                }
                                                else if (i == m_NameMapIndex[$"{eKafkaTopic}_MVUDs"])
                                                {
                                                    object[] Value =(object[])record.GetValue(i);
                                                    dataRow[i] = string.Join("_",Value.Cast<double>().ToArray());
                                                }
                                                else
                                                {
                                                    dataRow[i] = record.GetValue(i);
                                                }
                                            }
                                            #region AUX
                                            object[] aux = (object[])record.GetValue(m_NameMapIndex[$"{eKafkaTopic}_Auxs"]);
                                            for (int i = 0; i < aux.Count(); i++)
                                            {
                                                GenericRecord _record = aux[i] as GenericRecord;
                                                object Aux_AuxType = _record.GetValue((int)EMonitorFieldPos.Aux_AuxType);
                                                object Aux_AliasName = _record.GetValue((int)EMonitorFieldPos.Aux_AliasName);
                                                object Aux_AuxChGlobalID = _record.GetValue((int)EMonitorFieldPos.Aux_AuxChGlobalID);
                                                object Aux_AuxChVirtualID = _record.GetValue((int)EMonitorFieldPos.Aux_AuxChVirtualID);
                                                object Aux_Value = _record.GetValue((int)EMonitorFieldPos.Aux_Value);
                                                object Aux_dxdt = _record.GetValue((int)EMonitorFieldPos.Aux_dxdt);
                                                string colnumName = GetAuxColumn(Convert.ToString(Aux_AliasName), Convert.ToInt32(Aux_AuxChVirtualID));
                                                Console.WriteLine(colnumName);
                                                if (!m_dtServicesArbin.Columns.Contains(colnumName))
                                                {
                                                    m_dtServicesArbin.Columns.Add(colnumName, Type.GetType("System.String"));
                                                    m_RefresherColumn = true;
                                                }
                                                dataRow[colnumName] = $"{Aux_AuxType}^{Aux_AliasName}^{Aux_AuxChGlobalID}^{Aux_AuxChVirtualID}^{Aux_Value}^{Aux_dxdt}";
                                            }
                                            #endregion
                                            #region CAN
                                            object[] can = (object[])record.GetValue(m_NameMapIndex[$"{eKafkaTopic}_CANBMSs"]);
                                            for (int i = 0; i < can.Count(); i++)
                                            {
                                                GenericRecord _record = can[i] as GenericRecord;
                                                string MetaName = Convert.ToString(_record.GetValue((int)EMonitorFieldPos.CANBMS_MetaName));
                                                object AliasName = _record.GetValue((int)EMonitorFieldPos.CANBMS_AliasName);
                                                object IsOffline = _record.GetValue((int)EMonitorFieldPos.CANBMS_IsOffline);
                                                object DataType = _record.GetValue((int)EMonitorFieldPos.CANBMS_DataType);
                                                object Value = _record.GetValue((int)EMonitorFieldPos.CANBMS_Value);
                                                if (!m_dtServicesArbin.Columns.Contains(MetaName))
                                                {
                                                    m_dtServicesArbin.Columns.Add(MetaName, Type.GetType("System.String"));
                                                    m_RefresherColumn = true;
                                                }
                                                dataRow[MetaName] = $"{MetaName}^{AliasName}^{IsOffline}^{DataType}^{Value}";
                                            }
                                            #endregion
                                            #region SMB
                                            object[] smb = (object[])record.GetValue(m_NameMapIndex[$"{eKafkaTopic}_SMBs"]);
                                            for (int i = 0; i < smb.Count(); i++)
                                            {
                                                GenericRecord _record = smb[i] as GenericRecord;
                                                string MetaName = Convert.ToString(_record.GetValue((int)EMonitorFieldPos.SMB_MetaName));
                                                object AliasName = _record.GetValue((int)EMonitorFieldPos.SMB_AliasName);
                                                object IsOffline = _record.GetValue((int)EMonitorFieldPos.SMB_IsOffline);
                                                object DataType = _record.GetValue((int)EMonitorFieldPos.SMB_DataType);
                                                object Value = _record.GetValue((int)EMonitorFieldPos.SMB_Value);
                                                if (!m_dtServicesArbin.Columns.Contains(MetaName))
                                                {
                                                    m_dtServicesArbin.Columns.Add(MetaName, Type.GetType("System.String"));
                                                    m_RefresherColumn = true;
                                                }
                                                dataRow[MetaName] = $"{MetaName}^{AliasName}^{IsOffline}^{DataType}^{Value}";
                                            }
                                            #endregion
                                            if (m_RefresherColumn)
                                            {
                                                if (dataGridView1.InvokeRequired)
                                                {
                                                    dataGridView1.Invoke(new Action(() =>
                                                    {
                                                        dataGridView1.DataSource = null;
                                                        dataGridView1.DataSource = m_dtServicesArbin;
                                                    }));
                                                    Thread.Sleep(100);
                                                }
                                                else
                                                {
                                                    dataGridView1.DataSource = null;
                                                    dataGridView1.DataSource = m_dtServicesArbin;
                                                }
                                            }

                                        }
                                    }
                                }
                                //consumer.Commit(consumeResult);
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
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerChannelTopic()
        {
            string strPathFile = $@"{m_ExportDataPath}\Channel\";
            Directory.CreateDirectory(strPathFile);
            //string strPathFile = $@"{m_ExportDataPath}\Channel.csv";
            EKafkaTopic eKafkaTopic = EKafkaTopic.Channel;
            try
            {
                
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };
                IConsumer<string, byte[]> consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build();
                m_Offset.Add(consumer,new Dictionary<int, int>());
                {
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
                        int intFilesCount = m_TopicFilesCount[eKafkaTopic];
                        int intCount = 0;
                        string strPath;
                        while (true)
                        {
                            try
                            {
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                intCount++;
                                if (bSelectKey)
                                {
                                    if (m_UseSerialNumber && m_bTestName && !consumeResult.Key.ToLower().Equals(strSelect.ToLower()))
                                        continue;
                                    if (!m_bTestName && !consumeResult.Key.ToLower().Contains(strSelect.ToLower()))
                                        continue;
                                };
                                bool bOk = true;
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    while (memoryStream.Position < memoryStream.Length)
                                    {
                                        GenericRecord record = avroReader.Read(null, reader);
                                        if (m_bTestID && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_TestID)) != m_TestID)
                                            continue;
                                        if (m_bChanel && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_Chan)) != m_Chanel)
                                            continue;
                                        strPath = strPathFile;
                                        List<string> lstLine = new List<string>();
                                        List<string> lstCol = new List<string>();
                                        string strValue;
                                        for (int i = 0; i < intFilesCount; i++)
                                        {
                                            if (i == m_NameMapIndex[$"{eKafkaTopic}_SerialNumber"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_TestName"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_TestID"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_ChannelID"])
                                            {
                                                strValue = Convert.ToString(record.GetValue(i));
                                                strPath += $"[{strValue}] ";
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_Auxs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {
                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxType))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxChGlobalID))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxChVirtualID))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_Value))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_dxdt))}]";
                                                    lstCol.Add(strValue);
                                                }
                                                //lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_CANBMSs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {
                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_CANBMS_MetaName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_CANBMS_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_CANBMS_DataType))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_CANBMS_Value))}]";
                                                    lstCol.Add(strValue);
                                                }
                                                //lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_SMBs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {

                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_SMB_MetaName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_SMB_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_SMB_DataType))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Channel_SMB_Value))}]";
                                                    lstCol.Add(strValue);
                                                }
                                                //lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_MVUDs"])
                                            {
                                                object[] Value = (object[])record.GetValue(i);
                                                strValue = string.Join("_", Value.Cast<double>().ToArray());
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_TCCounters"])
                                            {
                                                object[] Value = (object[])record.GetValue(i);
                                                strValue = string.Join("_", Value.Cast<int>().ToArray());
                                                lstCol.Add(strValue);
                                            }
                                            else
                                            {
                                                strValue = Convert.ToString(record.GetValue(i));
                                                lstCol.Add(strValue);
                                            }
                                        }
                                        lstLine.Add(string.Join(",", lstCol));
                                        strPath += ".csv";
                                        if (!File.Exists(strPath))
                                            CreateFile(eKafkaTopic, strPath);
                                        bOk = WriteFile_DifferenPath_Channel(eKafkaTopic, strPath, ".csv", lstLine);
                                        if (!bOk)
                                        {
                                            Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                            continue;
                                        }
                                    }
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
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerSubChannelTopic()
        {
            string strPathFile = $@"{m_ExportDataPath}\Channel\";
            Directory.CreateDirectory(strPathFile);
            //string strPathFile = $@"{m_ExportDataPath}\Channel.csv";
            EKafkaTopic eKafkaTopic = EKafkaTopic.Channel;
            try
            {

                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };
                IConsumer<string, byte[]> consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build();
                m_Offset.Add(consumer, new Dictionary<int, int>());
                {
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
                        int intFilesCount = m_TopicFilesCount[eKafkaTopic];
                        int intCount = 0;
                        string strPath;
                        while (true)
                        {
                            try
                            {
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                intCount++;
                                if (bSelectKey)
                                {
                                    if (m_UseSerialNumber && m_bTestName && !consumeResult.Key.Equals(strSelect))
                                        continue;
                                    if (!m_bTestName && !consumeResult.Key.Contains(strSelect))
                                        continue;
                                };
                                bool bOk = true;
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    while (memoryStream.Position < memoryStream.Length)
                                    {
                                        GenericRecord record = avroReader.Read(null, reader);
                                        if (m_bTestID && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_TestID)) != m_TestID)
                                            continue;
                                        if (m_bChanel && Convert.ToString(record.GetValue((int)EMonitorFieldPos.Channel_Chan)) != m_Chanel)
                                            continue;
                                        strPath = strPathFile;
                                        List<string> lstLine = new List<string>();
                                        List<string> lstCol = new List<string>();
                                        string strValue;
                                        for (int i = 0; i < intFilesCount; i++)
                                        {
                                            if (i == m_NameMapIndex[$"{eKafkaTopic}_SerialNumber"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_TestName"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_TestID"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_ChannelID"] ||
                                                i == m_NameMapIndex[$"{eKafkaTopic}_SubChannelID"])
                                            {
                                                strValue = Convert.ToString(record.GetValue(i));
                                                strPath += $"[{strValue}] ";
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_Auxs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {
                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxType))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxChGlobalID))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_AuxChVirtualID))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_Value))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.Aux_dxdt))}]";
                                                }
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_CANBMSs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {
                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.CANBMS_MetaName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.CANBMS_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.CANBMS_IsOffline))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.CANBMS_DataType))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.CANBMS_Value))}]";
                                                }
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_SMBs"])
                                            {
                                                strValue = "";
                                                foreach (var item in (object[])record.GetValue(i))
                                                {

                                                    strValue = "";
                                                    GenericRecord record_tem = item as GenericRecord;
                                                    strValue += $"[{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.SMB_MetaName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.SMB_AliasName))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.SMB_IsOffline))}";
                                                    strValue += $"^{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.SMB_DataType))}";
                                                    strValue += $"{Convert.ToString(record_tem.GetValue((int)EMonitorFieldPos.SMB_Value))}]";
                                                }
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_MVUDs"])
                                            {
                                                object[] Value = (object[])record.GetValue(i);
                                                strValue = string.Join("_", Value.Cast<double>().ToArray());
                                                lstCol.Add(strValue);
                                            }
                                            else if (i == m_NameMapIndex[$"{eKafkaTopic}_TCCounters"])
                                            {
                                                object[] Value = (object[])record.GetValue(i);
                                                strValue = string.Join("_", Value.Cast<int>().ToArray());
                                                lstCol.Add(strValue);
                                            }
                                            else
                                            {
                                                strValue = Convert.ToString(record.GetValue(i));
                                                lstCol.Add(strValue);
                                            }
                                        }
                                        lstLine.Add(string.Join(",", lstCol));
                                        strPath += ".csv";
                                        if (!File.Exists(strPath))
                                            CreateFile(eKafkaTopic, strPath);
                                        bOk = WriteFile_DifferenPath_Channel(eKafkaTopic, strPath, ".csv", lstLine);
                                        if (!bOk)
                                        {
                                            Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                            continue;
                                        }
                                    }
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
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerTestInfoTopic()
        {
            EKafkaTopic eKafkaTopic = EKafkaTopic.TestInfo;
            try
            {
                string strPathFile = $@"{m_ExportDataPath}\TestInfo.csv";

                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };
                using (var consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build())
                {
                    consumer.Subscribe(eKafkaTopic.ToString());
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = false; // 防止应用程序关闭
                        cancellationTokenSource.Cancel();
                    };
                    try
                    {
                        while (true)
                        {
                            try
                            {
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                //if (m_Offset[consumer].TryGetValue(consumeResult.Partition,out int offset))
                                //{
                                //    offset++;
                                //}
                                //else
                                //{
                                //    m_Offset[consumer].Add(consumeResult.Partition, 1);
                                //}
                                if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                {
                                    //consumer.Commit(consumeResult);
                                    continue;
                                };
                                bool bOk = true;
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    while (memoryStream.Position < memoryStream.Length)
                                    {
                                        GenericRecord record = avroReader.Read(null, reader);
                                        Dictionary<string, object> recordDict = record.Schema.Fields.ToDictionary(
                                            field => field.Name,
                                            field => record.GetValue(field.Pos)
                                        );
                                        List<string> lstLine = new List<string>();
                                        List<string> lstCol = new List<string>();
                                        foreach (var keyValue in recordDict)
                                        {
                                            if (keyValue.Key == "Mappings")
                                            {
                                                string strValue = "";
                                                foreach (var item in (object[])keyValue.Value)
                                                {
                                                    record = item as GenericRecord;
                                                    strValue += "[";
                                                    strValue += Convert.ToString(record.GetValue((int)EMonitorFieldPos.Mapping_AuxType)) + "^";
                                                    strValue += Convert.ToString(record.GetValue((int)EMonitorFieldPos.Mapping_AuxChGlobalID)) + "^";
                                                    strValue += Convert.ToString(record.GetValue((int)EMonitorFieldPos.Mapping_AliasName)) + "^";
                                                    strValue += Convert.ToString(record.GetValue((int)EMonitorFieldPos.Mapping_Unit));
                                                    strValue += "]";
                                                }
                                                if (strValue.EndsWith(";"))
                                                    strValue = strValue.Remove(strValue.Length - 1);
                                                lstCol.Add(strValue);
                                            }
                                            else if (keyValue.Key == "SimulationFileNames")
                                            {
                                                string strValue = "";
                                                foreach (var item in (object[])keyValue.Value)
                                                {
                                                    //record = item as GenericRecord;
                                                    strValue += $"[{item}]";
                                                }
                                                if (strValue.EndsWith(";"))
                                                    strValue = strValue.Remove(strValue.Length - 1);
                                                lstCol.Add(strValue);
                                            }
                                            else
                                                lstCol.Add(Convert.ToString(keyValue.Value));
                                        }
                                        lstLine.Add(string.Join(",", lstCol));
                                        bOk = WriteFile_DifferenPath(eKafkaTopic, strPathFile, ".csv", lstLine, out string newPath);
                                        if (strPathFile != newPath)
                                            strPathFile = newPath;
                                        if (!bOk)
                                        {
                                            Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                            continue;
                                        }
                                    }
                                }
                                //if (bOk)
                                //    consumer.Commit(consumeResult);
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
                        consumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerEventTopic()
        {
            EKafkaTopic eKafkaTopic = EKafkaTopic.Event;
            try
            {
                string strPathFile = $@"{m_ExportDataPath}\Event.csv";

                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };
                using (var consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build())
                {
                    consumer.Subscribe(eKafkaTopic.ToString());
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = false; // 防止应用程序关闭
                        cancellationTokenSource.Cancel();
                    };
                    try
                    {
                        while (true)
                        {
                            try
                            {
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                {
                                    //consumer.Commit(consumeResult);
                                    continue;
                                };
                                bool bOk = true;
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    while (memoryStream.Position < memoryStream.Length)
                                    {
                                        GenericRecord record = avroReader.Read(null, reader);
                                        Dictionary<string, object> recordDict = record.Schema.Fields.ToDictionary(
                                            field => field.Name,
                                            field => record.GetValue(field.Pos)
                                        );
                                        List<string> lstLine = new List<string>();
                                        List<string> lstCol = new List<string>();
                                        foreach (var keyValue in recordDict)
                                        {
                                            lstCol.Add(Convert.ToString(keyValue.Value));
                                        }
                                        lstLine.Add(string.Join(",", lstCol));
                                        bOk = WriteFile_DifferenPath(eKafkaTopic, strPathFile, ".csv", lstLine, out string newPath);
                                        if (strPathFile != newPath)
                                            strPathFile = newPath;
                                        if (!bOk)
                                        {
                                            Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                            continue;
                                        }
                                    }
                                }
                                //if (bOk)
                                //    consumer.Commit(consumeResult);
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
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerDiagnosticEventTopic()
        {
            string strPathFile = $@"{m_ExportDataPath}\DiagnosticEvent.csv";
            EKafkaTopic eKafkaTopic = EKafkaTopic.DiagnosticEvent;
            try
            {
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                ConsumerConfig configComsume = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest, // 从最早的偏移量开始消费消息
                    EnableAutoCommit = false // 启用自动提交偏移量
                };
                using (var consumer = new ConsumerBuilder<string, byte[]>(configComsume).Build())
                {
                    consumer.Subscribe(eKafkaTopic.ToString());
                    CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
                    Console.CancelKeyPress += (_, e) =>
                    {
                        e.Cancel = false; // 防止应用程序关闭
                        cancellationTokenSource.Cancel();
                    };
                    try
                    {
                        while (true)
                        {
                            try
                            {
                                ConsumeResult<string, byte[]> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                {
                                    //consumer.Commit(consumeResult);
                                    continue;
                                };
                                bool bOk = true;
                                var serializedRecords = consumeResult.Message.Value;
                                using (var memoryStream = new System.IO.MemoryStream(serializedRecords))
                                {
                                    var reader = new BinaryDecoder(memoryStream);
                                    var avroReader = new GenericDatumReader<GenericRecord>(schema,
                                    schema);
                                    while (memoryStream.Position < memoryStream.Length)
                                    {
                                        GenericRecord record = avroReader.Read(null, reader);
                                        Dictionary<string, object> recordDict = record.Schema.Fields.ToDictionary(
                                            field => field.Name,
                                            field => record.GetValue(field.Pos)
                                        );
                                        List<string> lstLine = new List<string>();
                                        List<string> lstCol = new List<string>();
                                        foreach (var keyValue in recordDict)
                                        {
                                            lstCol.Add(Convert.ToString(keyValue.Value));
                                        }
                                        lstLine.Add(string.Join(",", lstCol));
                                        bOk = WriteFile_DifferenPath(eKafkaTopic, strPathFile, ".csv", lstLine, out string newPath);
                                        if (strPathFile != newPath)
                                            strPathFile = newPath;
                                        if (!bOk)
                                        {
                                            Log($"[{eKafkaTopic}]:" + string.Join(",", lstCol));
                                            continue;
                                        }
                                    }
                                }
                                //if (bOk)
                                //    consumer.Commit(consumeResult);
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
                        // 用户取消操作，关闭消费者
                        Log($"[{eKafkaTopic}]:Consumer Close;" + ex.Message);
                        consumer.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        #endregion
        private string GetAuxColumn(string Aux_AliasName, int Aux_AuxChVirtualID)
        {
            string colnumName = "";
            try
            {
                foreach (var item in Enum.GetNames(typeof(EAuxColumn)))
                {
                    if (Convert.ToString(Aux_AliasName).Contains(item))
                    {
                        colnumName = $"{item}_{(Convert.ToInt32(Aux_AuxChVirtualID) + 1)}";
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
            return colnumName;
        }
        private bool WriteFile_DifferenPath(EKafkaTopic eKafkaTopic, string strPathFile, string Extension, List<string> lstLine, out string newStrPathFile)
        {
            lock (objLockFIle)
            {
                bool bOk = true;
                newStrPathFile = strPathFile;
                try
                {
                    if (new FileInfo(strPathFile).Length >= 1024 * 1024 * 100) //100M
                    {
                        var array = Path.GetFileName(strPathFile).Replace(Extension, "").Split('_');
                        int index = 1;
                        if (array.Length > 1)
                        {
                            index = Convert.ToInt32(array[1]);
                            index += 1;
                            strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            CreateFile(eKafkaTopic, strPathFile);
                        }
                        else
                        {
                            strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            while (File.Exists(strPathFile))
                            {
                                index++;
                                strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            }
                            if (File.Exists(Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + (index - 1) + Extension) &&  new FileInfo(Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + (index - 1) + Extension).Length < 1024 * 1024 * 100)
                            {
                                index--;
                                strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            }
                            else
                            {
                                CreateFile(eKafkaTopic, strPathFile);
                            }
                        }
                    }
                    File.AppendAllLines(strPathFile, lstLine);
                    newStrPathFile = strPathFile;
                }
                catch (Exception ex)
                {
                    bOk = false;
                    Log($"WriteFile:{strPathFile};" + ex.Message);
                }
                return bOk;
            }
        }
        private bool WriteFile_DifferenPath_Channel(EKafkaTopic eKafkaTopic, string strPathFile, string Extension, List<string> lstLine)
        {
            lock (objLockFIle)
            {
                bool bOk = true;
                try
                {
                    if (new FileInfo(strPathFile).Length >= 1024 * 1024 * 100) //100M
                    {
                        string DirectoryName = Path.GetDirectoryName(strPathFile) + "\\";
                        string strName = Path.GetFileName(strPathFile).Replace(Extension, "");
                        int index = 1;
                        strPathFile = DirectoryName + strName + "_" + index + Extension;
                        while (File.Exists(strPathFile))
                        {
                            index++;
                            strPathFile = DirectoryName + strName + "_" + index + Extension;
                        }
                        if (File.Exists(DirectoryName + strName + "_" + (index - 1) + Extension) && new FileInfo(DirectoryName + strName + "_" + (index - 1) + Extension).Length < 1024 * 1024 * 100)
                        {
                            index--;
                            strPathFile = DirectoryName + strName + "_" + index + Extension;
                        }
                        else
                        {
                            CreateFile(eKafkaTopic, strPathFile);
                        }
                    }
                    File.AppendAllLines(strPathFile, lstLine);
                }
                catch (Exception ex)
                {
                    bOk = false;
                    Log($"WriteFile:{strPathFile};" + ex.Message);
                }
                return bOk;
            }
        }
        private void Log(string mes)
        {
            try
            {
                File.AppendAllLines(m_LogPath,  new List<string>() { $"[{DateTime.Now}:]"+ mes });
            }
            catch (Exception ex)
            {

            }
        }
        string FormatTime(object timeInSeconds)
        {
            int timeInMilliseconds = (int)(Convert.ToDouble(timeInSeconds) * 1000);
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInMilliseconds);
            return timeSpan.ToString(@"hh\:mm\:ss\.fff");
        }
        private void AddNameMapIndex(string field , int index)
        {
            if (!m_NameMapIndex.ContainsKey(field))
                m_NameMapIndex.Add(field, index);
        }
        private void CreateFile(EKafkaTopic eKafkaTopic, string strPathFile,bool Create = true)
        {
            try
            {
                if (!Directory.Exists(m_ExportDataPath))
                    Directory.CreateDirectory(m_ExportDataPath);
                List<string> lstLine = new List<string>();
                
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                if (!m_TopicFilesCount.Keys.Contains(eKafkaTopic))
                    m_TopicFilesCount.Add(eKafkaTopic, schema.Fields.Count);
                List<string> lstFields = schema.Fields.Select(p => p.Name).ToList();
                string strField;
                for (int i = 0; i < lstFields.Count; i++)
                {
                    strField = lstFields[i];
                    AddNameMapIndex($"{eKafkaTopic}_{strField}",i);
                }
                if (Create && !File.Exists(strPathFile))
                {
                    using (var fileStream = File.Create(strPathFile))
                    {
                        fileStream.Close(); // 关闭文件句柄
                    }
                    string strColumn = "";
                    foreach (var item in schema.Fields)
                    {
                        if (item.Name == "Auxs" || item.Name == "CANBMSs" || item.Name == "SMBs")
                        {
                            continue;
                        }
                        strColumn += item.Name + ",";
                    }
                    if (strColumn.EndsWith(","))
                        strColumn = strColumn.Remove(strColumn.Length - 1);
                    lstLine.Add(strColumn);
                    File.AppendAllLines(strPathFile, lstLine);
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void InitExportPath()
        {
            try
            {
                m_TopicFilesCount.Clear();
                CreateFile(EKafkaTopic.TestInfo, $@"{m_ExportDataPath}\TestInfo.csv");
                CreateFile(EKafkaTopic.Channel, $@"{m_ExportDataPath}\Channel.csv",false);
                CreateFile(EKafkaTopic.Event, $@"{m_ExportDataPath}\Event.csv");
                CreateFile(EKafkaTopic.DiagnosticEvent, $@"{m_ExportDataPath}\DiagnosticEvent.csv");
                CreateFile(EKafkaTopic.SubChannel, $@"{m_ExportDataPath}\SubChannel.csv");
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        
        private void InitDataGridView()
        {
            try
            {
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(EKafkaTopic.Monitor.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                List<string> lstFields = schema.Fields.Select(p => p.Name).ToList();
                string strField;
                for (int i = 0; i < lstFields.Count; i++)
                {
                    strField = lstFields[i];
                    AddNameMapIndex($"{EKafkaTopic.Monitor}_{strField}", i);
                    if (strField == "Auxs" || strField == "CANBMSs" || strField == "SMBs" || strField == "SubChannels")
                        continue;
                    m_dtServicesArbin.Columns.Add(strField, Type.GetType("System.String"));
                }
                m_PublicColumnCount = m_dtServicesArbin.Columns.Count;
                //for (int i = 0; i < 8; i++)
                //{
                //    DataRow newRow = m_dtServicesArbin.NewRow();
                //    for (int col = 0; col < 14; col++)
                //    {
                //        newRow[col] = "";
                //    }
                //    m_dtServicesArbin.Rows.Add(newRow);
                //}
                dataGridView1.DataSource = m_dtServicesArbin;
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            m_BootstrapServer = txtBootstrapServer.Text;
            m_SchemaRegisterServer = txtSchemaRegisterServer.Text;
            m_UseSerialNumber = chkSerialNumber.Checked;
            m_SerialNumber = txtSerialNumber.Text;
            m_GroupId = txtGroupId.Text;
            m_ConsumerMonitor = chkConsumerMonitor.Checked;
            m_bChanel = chkChannel.Checked;
            m_bTestID = chkTestID.Checked;
            m_bTestName = chkTestName.Checked;
            m_Chanel = txtChannel.Text;
            m_TestID = txtTestID.Text;
            m_TestName = txtTestName.Text;
            InitConfig(true);
            InitSchemaRegistryConfig();
            btnStart.Enabled = false;
            //m_ExportDataPath
            File.Copy($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Config.tde", m_ExportDataPath + "\\Config.tde");
            StartThread();
        }
        private void chkSerialNumber_CheckedChanged(object sender, EventArgs e)
        {
            txtSerialNumber.Enabled = chkSerialNumber.Checked;
        }

        private void chkTestName_CheckedChanged(object sender, EventArgs e)
        {
            txtTestName.Enabled = chkTestName.Checked;
        }

        private void chkChannel_CheckedChanged(object sender, EventArgs e)
        {
            txtChannel.Enabled = chkChannel.Checked;
        }

        private void chkTestID_CheckedChanged(object sender, EventArgs e)
        {
            txtTestID.Enabled = chkTestID.Checked;
        }
    }
}
