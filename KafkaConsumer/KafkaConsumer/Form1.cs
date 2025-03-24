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
using Confluent.SchemaRegistry.Serdes;
using Confluent.Kafka.SyncOverAsync;
using System.Text;
using Newtonsoft.Json;
using Arbin.Library.DataModel;
using System.Reflection;
using Arbin.Library.DataModel.Common;

namespace KafkaConsumer
{
    public partial class Form1 : Form
    {
        DataTable m_dtServicesArbin = new DataTable();
        private string m_GroupId = "Consumer_Arbin_QA";

        private string m_BootstrapServer = "192.168.1.57:9092";
        private string m_SaslUsername = "";
        private string m_SaslPassword = "";
        private string m_SchemaRegisterServer = "192.168.1.57:8081";
        private string m_BasicAuthUsername = "";
        private string m_BasicAuthPassword = "";
        private bool m_UseSerialNumber = false;
        private bool m_ConsumerMonitor = false;
        private string m_SerialNumber = "TestNumber";
        private bool m_bTestName = false;
        private bool m_bChanel = false;
        private bool m_bTestID = false;
        private bool m_bConfluentCloud = false;
        private string m_TestName = "";
        private string m_Chanel = "";
        private string m_TestID = "";
        private int m_MessageFormat = 0;
        public static double m_ExportFileSize = 100;
        string m_ExportDataPath;
        bool m_RefresherColumn = false;
        //int m_PublicColumnCount;
        List<string> m_PublicColumn = new List<string>();
        CachedSchemaRegistryClient m_CachedSchemaRegistryClient;
        Thread thread_Monitor;
        Thread thread_MonitorDisplay;
        Thread thread_Channel;
        Thread thread_TestInfo;
        Thread thread_Event;
        Thread thread_ChannelDiagnosticEventData;
        Thread thread_ScheduleFile;
        Thread thread_ConfigFile;
        string m_LogPath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Log.txt";
        ConsumerConfig configComsume_Earliest;
        ConsumerConfig configComsume_Lasest_MonitorDisplay;
        ConsumerConfig configComsume_Lasest;
        ConsumerJson<ChannelTestInfoData> m_ConsumerJson_ChannelTestInfoData;
        ConsumerJson<ChannelEventData> m_ConsumerJson_ChannelEventData;
        ConsumerJson<ChannelDiagnosticEventData> m_ConsumerJson_ChannelDiagnosticEventData;
        ConsumerJson<ChannelData> m_ConsumerJson_ChannelData;
        ConsumerJson<SubChannelData> m_ConsumerJson_SubChannelData;
        ConsumerJson<ChannelMonitorData> m_ConsumerJson_ChannelMonitorData;
        ConsumerJson<ScheduleFileInfo> m_ConsumerJson_ScheduleFile;
        ConsumerJson<ConfigFileInfo> m_ConsumerJson_ConfigFile;

        ConsumerAvro<ChannelTestInfoData> m_ConsumerAvro_ChannelTestInfoData;
        ConsumerAvro<ChannelEventData> m_ConsumerAvro_ChannelEventData;
        ConsumerAvro<ChannelDiagnosticEventData> m_ConsumerAvro_ChannelDiagnosticEventData;
        ConsumerAvro<ChannelData> m_ConsumerAvro_ChannelData;
        ConsumerAvro<SubChannelData> m_ConsumerAvro_SubChannelData;
        ConsumerAvro<ChannelMonitorData> m_ConsumerAvro_ChannelMonitorData;
        ConsumerAvro<ScheduleFileInfo> m_ConsumerAvro_ScheduleFile;
        ConsumerAvro<ConfigFileInfo> m_ConsumerAvro_ConfigFile;
        public Form1()
        {
            InitializeComponent();
            InitConfig();
            InitControl();
            m_ExportDataPath = $"{ Environment.CurrentDirectory}\\ExportData\\{txtGroupId.Text}";
        }
        private void InitControl()
        {
            try
            {
                txtBootstrapServer.Text = m_BootstrapServer;
                txtSaslUsername.Text = m_SaslUsername;
                txtSaslPassword.Text = m_SaslPassword;
                txtSchemaRegisterServer.Text = m_SchemaRegisterServer;
                txtBasicAuthUsername.Text = m_BasicAuthUsername;
                txtBasicAuthPassword.Text = m_BasicAuthPassword;
                chkSerialNumber.Checked = m_UseSerialNumber;
                txtSerialNumber.Text = m_SerialNumber;
                txtGroupId.Text = Guid.NewGuid().ToString();
                //txtGroupId.Text = m_GroupId;
                txtSerialNumber.Enabled = chkSerialNumber.Checked;
                chkConsumerMonitor.Checked = m_ConsumerMonitor;
                chkTestName.Checked = m_bTestName;
                chkTestID.Checked = m_bTestID;
                chkConfluentCloud.Checked = m_bConfluentCloud;
                chkChannel.Checked = m_bChanel;
                txtTestName.Enabled = chkTestName.Checked;
                txtChannel.Enabled = chkChannel.Checked;
                txtTestID.Enabled = chkTestID.Checked;
                txtTestName.Text = m_TestName;
                txtChannel.Text = m_Chanel;
                txtTestID.Text = m_TestID;
                cmbMessageFormat.SelectedIndex = Convert.ToInt32(m_MessageFormat);
                txtFileSize.Text = m_ExportFileSize.ToString();
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
                if (chkConfluentCloud.Checked)
                {
                    schemaRegistryConfig.BasicAuthUserInfo = $"{m_BasicAuthUsername}:{m_BasicAuthPassword}";
                }
                m_CachedSchemaRegistryClient = new CachedSchemaRegistryClient(schemaRegistryConfig);
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
                        sw.WriteLine($"SaslUsername:[{m_SaslUsername}]");
                        sw.WriteLine($"SaslPassword:[{m_SaslPassword}]");
                        sw.WriteLine($"SchemaRegisterServer:[{m_SchemaRegisterServer}]");
                        sw.WriteLine($"BasicAuthUsername:[{m_BasicAuthUsername}]");
                        sw.WriteLine($"BasicAuthPassword:[{m_BasicAuthPassword}]");
                        sw.WriteLine($"Use SerialNumber:[{m_UseSerialNumber}]");
                        sw.WriteLine($"SerialNumber:[{m_SerialNumber}]");
                        sw.WriteLine($"GroupId:[{m_GroupId}]");
                        sw.WriteLine($"ConsumerMonitor:[{m_ConsumerMonitor}]");
                        sw.WriteLine($"bTestName:[{m_bTestName}]");
                        sw.WriteLine($"bChanel:[{m_bChanel}]");
                        sw.WriteLine($"bTestID:[{m_bTestID}]");
                        sw.WriteLine($"bConfluentCloud:[{m_bConfluentCloud}]");
                        sw.WriteLine($"TestName:[{m_TestName}]");
                        sw.WriteLine($"Chanel:[{m_Chanel}]");
                        sw.WriteLine($"TestID:[{m_TestID}]");
                        sw.WriteLine($"MessageFormat:[{m_MessageFormat}]");
                        sw.WriteLine($"ExportFileSize:[{m_ExportFileSize}]");
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
                            sw.WriteLine($"SaslUsername:[{m_SaslUsername}]");
                            sw.WriteLine($"SaslPassword:[{m_SaslPassword}]");
                            sw.WriteLine($"SchemaRegisterServer:[{m_SchemaRegisterServer}]");
                            sw.WriteLine($"BasicAuthUsername:[{m_BasicAuthUsername}]");
                            sw.WriteLine($"BasicAuthPassword:[{m_BasicAuthPassword}]");
                            sw.WriteLine($"Use SerialNumber:[{m_UseSerialNumber}]");
                            sw.WriteLine($"SerialNumber:[{m_SerialNumber}]");
                            sw.WriteLine($"GroupId:[{m_GroupId}]");
                            sw.WriteLine($"ConsumerMonitor:[{m_ConsumerMonitor}]");
                            sw.WriteLine($"bTestName:[{m_bTestName}]");
                            sw.WriteLine($"bChanel:[{m_bChanel}]");
                            sw.WriteLine($"bTestID:[{m_bTestID}]");
                            sw.WriteLine($"bConfluentCloud:[{m_bConfluentCloud}]");
                            sw.WriteLine($"TestName:[{m_TestName}]");
                            sw.WriteLine($"Chanel:[{m_Chanel}]");
                            sw.WriteLine($"TestID:[{m_TestID}]");
                            sw.WriteLine($"MessageFormat:[{m_MessageFormat}]");
                            sw.WriteLine($"ExportFileSize:[{m_ExportFileSize}]");
                        }
                    }
                }
                using (StreamReader sr = File.OpenText(TestDbEfficiency_cfgFileName))
                {
                    m_BootstrapServer = SetConfig(sr.ReadLine());
                    m_SaslUsername = SetConfig(sr.ReadLine());
                    m_SaslPassword = SetConfig(sr.ReadLine());
                    m_SchemaRegisterServer = SetConfig(sr.ReadLine());
                    m_BasicAuthUsername = SetConfig(sr.ReadLine());
                    m_BasicAuthPassword = SetConfig(sr.ReadLine());
                    m_UseSerialNumber = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_SerialNumber = SetConfig(sr.ReadLine());
                    m_GroupId = SetConfig(sr.ReadLine());
                    m_ConsumerMonitor = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bTestName = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bChanel = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bTestID = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_bConfluentCloud = Convert.ToBoolean(SetConfig(sr.ReadLine()));
                    m_TestName = SetConfig(sr.ReadLine());
                    m_Chanel = SetConfig(sr.ReadLine());
                    m_TestID = SetConfig(sr.ReadLine());
                    m_MessageFormat = Convert.ToInt32(SetConfig(sr.ReadLine()));
                    m_ExportFileSize = Convert.ToDouble(SetConfig(sr.ReadLine()));
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
                thread_MonitorDisplay = new Thread(new ThreadStart(ConsumerMonitorDispaly));
                thread_MonitorDisplay.IsBackground = true;
                thread_Monitor = new Thread(new ThreadStart(ConsumerMonitorTopic));
                thread_Monitor.IsBackground = true;
                thread_Channel = new Thread(new ThreadStart(ConsumerChannelTopic));
                thread_Channel.IsBackground = true;
                thread_TestInfo = new Thread(new ThreadStart(ConsumerTestInfoTopic));
                thread_TestInfo.IsBackground = true;
                thread_Event = new Thread(new ThreadStart(ConsumerEventTopic));
                thread_Event.IsBackground = true;
                thread_ChannelDiagnosticEventData = new Thread(new ThreadStart(ConsumerChannelDiagnosticEventDataTopic));
                thread_ChannelDiagnosticEventData.IsBackground = true;
                thread_ScheduleFile = new Thread(new ThreadStart(ConsumerScheduleFileTopic));
                thread_ScheduleFile.IsBackground = true;
                thread_ConfigFile = new Thread(new ThreadStart(ConsumerConfigFileTopic));
                thread_ConfigFile.IsBackground = true;
                thread_MonitorDisplay.Start();
                if (m_ConsumerMonitor)
                    thread_Monitor.Start();
                thread_Channel.Start();
                thread_TestInfo.Start();
                thread_Event.Start();
                thread_ChannelDiagnosticEventData.Start();
                thread_ScheduleFile.Start();
                thread_ConfigFile.Start();
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void ConsumerMonitorDispaly()
        {
            try
            {
                if (m_MessageFormat == (int)EMessageFormat.AVRO)
                    ConsumerMonitor_Avro();
                else
                    ConsumerMonitor_Json();

            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void ConsumerMonitor_Json()
        {
            try
            {
                EKafkaTopic eKafkaTopic = EKafkaTopic.ChannelMonitorData;
                try
                {
                    IConsumer<string, ChannelMonitorData> consumer = new ConsumerBuilder<string, ChannelMonitorData>(configComsume_Lasest_MonitorDisplay).SetValueDeserializer(new JsonDeserializer<ChannelMonitorData>().AsSyncOverAsync()).Build();
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
                               var consumeResult = consumer.Consume(cancellationTokenSource.Token);
                                if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                    continue;
                                var data = consumeResult.Message.Value;
                                if (m_bChanel)
                                {
                                    PropertyInfo property = data.GetType().GetProperty(nameof(ChannelMonitorData.ChannelID));
                                    if (property != null)
                                    {
                                        if (Convert.ToString(property.GetValue(data)) != m_Chanel)
                                            continue;
                                    }
                                }
                                int chan = data.ChannelID;
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
                                string strField;
                                object value;
                                foreach (PropertyInfo property in data.GetType().GetProperties())
                                {
                                    strField = property.Name;
                                    value = property.GetValue(data);
                                    if (strField == nameof(ChannelMonitorData.TestTime) || strField == nameof(ChannelMonitorData.StepTime))
                                    {
                                        dataRow[strField] = FormatTime(value);
                                    }
                                    else if (strField == nameof(ChannelMonitorData.MVUDs))
                                    {
                                        dataRow[strField] = string.Join("_", (IEnumerable<double>)value);
                                    }
                                    else if (strField == nameof(ChannelMonitorData.Auxs))
                                    {
                                        var aux = (IEnumerable<object>)value;
                                        foreach (AuxData item in (IEnumerable<AuxData>)value)
                                        {
                                            string colnumName = GetAuxColumn(Convert.ToString(item.AliasName), Convert.ToInt32(item.AuxChVirtualID));
                                            if (!m_dtServicesArbin.Columns.Contains(colnumName))
                                            {
                                                m_dtServicesArbin.Columns.Add(colnumName, Type.GetType("System.String"));
                                                m_RefresherColumn = true;
                                            }
                                            dataRow[colnumName] = $"{item.AuxType}^{item.AliasName}^{item.AuxChGlobalID}^{item.AuxChVirtualID}^{item.Value}^{item.dxdt}";
                                        }
                                    }
                                    else if (strField == nameof(ChannelMonitorData.CANs))
                                    {
                                        foreach (CANMonitorInfo item in (IEnumerable<CANMonitorInfo>)value)
                                        {
                                            string colnumName = item.MetaName;
                                            if (!m_dtServicesArbin.Columns.Contains(colnumName))
                                            {
                                                m_dtServicesArbin.Columns.Add(colnumName, Type.GetType("System.String"));
                                                m_RefresherColumn = true;
                                            }
                                            dataRow[colnumName] = $"{item.MetaName}^{item.AliasName}^{item.IsOffline}^{item.DataType}^{item.Value}";
                                        }
                                    }
                                    else if (strField == nameof(ChannelMonitorData.SMBs))
                                    {
                                        foreach (SMBMonitorInfo item in (IEnumerable<SMBMonitorInfo>)value)
                                        {
                                            string colnumName = item.MetaName;
                                            if (!m_dtServicesArbin.Columns.Contains(colnumName))
                                            {
                                                m_dtServicesArbin.Columns.Add(colnumName, Type.GetType("System.String"));
                                                m_RefresherColumn = true;
                                            }
                                            dataRow[colnumName] = $"{item.MetaName}^{item.AliasName}^{item.IsOffline}^{item.DataType}^{item.Value}";
                                        }
                                    }
                                    else if (strField == nameof(ChannelMonitorData.SubChannels))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        dataRow[strField] = value;
                                    }
                                }
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
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void ConsumerMonitor_Avro()
        {
            EKafkaTopic eKafkaTopic = EKafkaTopic.ChannelMonitorData;
            try
            {
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(eKafkaTopic.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                IConsumer<string, GenericRecord> consumer = new ConsumerBuilder<string, GenericRecord>(configComsume_Lasest_MonitorDisplay).SetValueDeserializer(new AvroDeserializer<GenericRecord>(m_CachedSchemaRegistryClient).AsSyncOverAsync()).Build();
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
                            ConsumeResult<string, GenericRecord> consumeResult = consumer.Consume(cancellationTokenSource.Token);
                            if (m_UseSerialNumber && !consumeResult.Key.Contains(m_SerialNumber))
                                continue;
                            GenericRecord record = consumeResult.Message.Value;
                            var chan = Convert.ToInt32(record[nameof(ChannelMonitorData.ChannelID)]);
                            if (m_bChanel && Convert.ToString(chan) != m_Chanel)
                                continue;
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
                            foreach (string col in m_PublicColumn)
                            {
                                if (col == nameof(ChannelMonitorData.TestTime) || col == nameof(ChannelMonitorData.StepTime))
                                {
                                    dataRow[col] = FormatTime(record[col]);
                                }
                                else if (col == nameof(ChannelMonitorData.MVUDs))
                                {
                                    object[] Value = (object[])record[col];
                                    dataRow[col] = string.Join("_", Value.Cast<double>().ToArray());
                                }
                                else
                                {
                                    dataRow[col] = record[col];
                                }
                            }
                            #region AUX
                            object[] aux = (object[])record[nameof(ChannelMonitorData.Auxs)];
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
                            object[] can = (object[])record[nameof(ChannelMonitorData.CANs)];
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
                            object[] smb = (object[])record[nameof(ChannelMonitorData.SMBs)];
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
            catch (Exception ex)
            {
                Log($"[{eKafkaTopic}]:" + ex.Message);
            }
        }
        private void ConsumerMonitorTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ChannelMonitorData.ConsumerTopic<ChannelMonitorData>(EKafkaTopic.ChannelMonitorData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ChannelMonitorData.ConsumerTopic(EKafkaTopic.ChannelMonitorData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerChannelTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ChannelData.ConsumerTopic<ChannelData>(EKafkaTopic.ChannelData, m_UseSerialNumber, m_bTestName, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ChannelData.ConsumerTopic(EKafkaTopic.ChannelData, m_UseSerialNumber, m_bTestName, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerSubChannelTopic()
        {
            m_ConsumerJson_SubChannelData.ConsumerTopic<SubChannelData>(EKafkaTopic.SubChannelData, m_UseSerialNumber, m_bTestName, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerTestInfoTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ChannelTestInfoData.ConsumerTopic<ChannelTestInfoData>(EKafkaTopic.ChannelTestInfoData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ChannelTestInfoData.ConsumerTopic(EKafkaTopic.ChannelTestInfoData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerEventTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ChannelEventData.ConsumerTopic<ChannelEventData>(EKafkaTopic.ChannelEventData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ChannelEventData.ConsumerTopic(EKafkaTopic.ChannelEventData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerChannelDiagnosticEventDataTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ChannelDiagnosticEventData.ConsumerTopic<ChannelDiagnosticEventData>(EKafkaTopic.ChannelDiagnosticEventData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ChannelDiagnosticEventData.ConsumerTopic(EKafkaTopic.ChannelDiagnosticEventData, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerScheduleFileTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ScheduleFile.ConsumerTopic<ScheduleFileInfo>(EKafkaTopic.ScheduleFileInfo, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ScheduleFile.ConsumerTopic(EKafkaTopic.ScheduleFileInfo, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        private void ConsumerConfigFileTopic()
        {
            if (m_MessageFormat == (int)EMessageFormat.JSON)
                m_ConsumerJson_ConfigFile.ConsumerTopic<ConfigFileInfo>(EKafkaTopic.ConfigFileInfo, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
            else
                m_ConsumerAvro_ConfigFile.ConsumerTopic(EKafkaTopic.ConfigFileInfo, m_UseSerialNumber, false, m_bTestID, m_bChanel, m_SerialNumber, m_TestName, m_TestID, m_Chanel);
        }
        #endregion        
        string FormatTime(object timeInSeconds)
        {
            int timeInMilliseconds = (int)(Convert.ToDouble(timeInSeconds) * 1000);
            TimeSpan timeSpan = TimeSpan.FromMilliseconds(timeInMilliseconds);
            return timeSpan.ToString(@"hh\:mm\:ss\.fff");
        }
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
        private void InitDataGridView()
        {
            try
            {
                string schemaJson = m_CachedSchemaRegistryClient.GetLatestSchemaAsync(EKafkaTopic.ChannelMonitorData.ToString()).Result;
                RecordSchema schema = (RecordSchema)Avro.Schema.Parse(schemaJson);
                List<string> lstFields = schema.Fields.Select(p => p.Name).ToList();
                string strField;
                for (int i = 0; i < lstFields.Count; i++)
                {
                    strField = lstFields[i];
                    if (strField == "Auxs" || strField == "CANs" || strField == "SMBs" || strField == "SubChannels")
                        continue;
                    m_dtServicesArbin.Columns.Add(strField, Type.GetType("System.String"));
                    m_PublicColumn.Add(strField);
                }
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
            m_SaslUsername = txtSaslUsername.Text;
            m_SaslPassword = txtSaslPassword.Text;
            m_SchemaRegisterServer = txtSchemaRegisterServer.Text;
            m_BasicAuthUsername = txtBasicAuthUsername.Text;
            m_BasicAuthPassword = txtBasicAuthPassword.Text;
            m_UseSerialNumber = chkSerialNumber.Checked;
            m_SerialNumber = txtSerialNumber.Text;
            m_GroupId = txtGroupId.Text;
            m_ConsumerMonitor = chkConsumerMonitor.Checked;
            m_bChanel = chkChannel.Checked;
            m_bTestID = chkTestID.Checked;
            m_bConfluentCloud = chkConfluentCloud.Checked;
            m_bTestName = chkTestName.Checked;
            m_Chanel = txtChannel.Text;
            m_TestID = txtTestID.Text;
            m_TestName = txtTestName.Text;
            m_MessageFormat = cmbMessageFormat.SelectedIndex;
            m_ExportFileSize = Convert.ToDouble(txtFileSize.Text);
            InitConfig(true);
            InitSchemaRegistryConfig();
            btnStart.Enabled = false;
            InitConsumer();
            Directory.CreateDirectory(m_ExportDataPath);
            File.Copy($"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Config.tde", m_ExportDataPath + "\\Config.tde");
            InitDataGridView();
            CreateThread();
        }
        private void InitConsumer()
        {
            try
            {
                configComsume_Earliest = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };
                configComsume_Lasest = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = m_GroupId,
                    AutoOffsetReset = AutoOffsetReset.Latest,
                    EnableAutoCommit = false
                };
                configComsume_Lasest_MonitorDisplay = new ConsumerConfig
                {
                    BootstrapServers = m_BootstrapServer,
                    GroupId = Guid.NewGuid().ToString(),
                    AutoOffsetReset = AutoOffsetReset.Latest,
                    EnableAutoCommit = false
                };
                if (chkConfluentCloud.Checked)
                {
                    configComsume_Earliest.SecurityProtocol =
                        configComsume_Lasest.SecurityProtocol =
                        configComsume_Lasest_MonitorDisplay.SecurityProtocol = SecurityProtocol.SaslSsl;

                    configComsume_Earliest.SaslMechanism =
                        configComsume_Lasest.SaslMechanism =
                        configComsume_Lasest_MonitorDisplay.SaslMechanism = SaslMechanism.Plain;

                    configComsume_Earliest.SaslUsername =
                        configComsume_Lasest.SaslUsername =
                        configComsume_Lasest_MonitorDisplay.SaslUsername = m_SaslUsername;

                    configComsume_Earliest.SaslPassword =
                        configComsume_Lasest.SaslPassword =
                        configComsume_Lasest_MonitorDisplay.SaslPassword = m_SaslPassword;
                }
                SchemaRegistryConfig schemaRegistryConfig = new SchemaRegistryConfig
                {
                    Url = $"http://{m_SchemaRegisterServer.Replace("http://", "")}" // 指定 Schema Registry 的地址
                };
                if (chkConfluentCloud.Checked)
                {
                    schemaRegistryConfig.Url = $"https://{m_SchemaRegisterServer.Replace("https://", "")}";// 指定 Schema Registry 的地址
                    schemaRegistryConfig.BasicAuthUserInfo = $"{m_BasicAuthUsername}:{m_BasicAuthPassword}";
                }
                if (m_MessageFormat == (int)EMessageFormat.JSON)
                {
                    m_ConsumerJson_ChannelTestInfoData = new ConsumerJson<ChannelTestInfoData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ChannelEventData = new ConsumerJson<ChannelEventData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ChannelDiagnosticEventData = new ConsumerJson<ChannelDiagnosticEventData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ChannelData = new ConsumerJson<ChannelData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_SubChannelData = new ConsumerJson<SubChannelData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ChannelMonitorData = new ConsumerJson<ChannelMonitorData>(m_ExportDataPath, configComsume_Lasest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ScheduleFile = new ConsumerJson<ScheduleFileInfo>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerJson_ConfigFile = new ConsumerJson<ConfigFileInfo>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                }
                else
                {
                    m_ConsumerAvro_ChannelTestInfoData = new ConsumerAvro<ChannelTestInfoData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ChannelEventData = new ConsumerAvro<ChannelEventData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ChannelDiagnosticEventData = new ConsumerAvro<ChannelDiagnosticEventData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ChannelData = new ConsumerAvro<ChannelData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_SubChannelData = new ConsumerAvro<SubChannelData>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ChannelMonitorData = new ConsumerAvro<ChannelMonitorData>(m_ExportDataPath, configComsume_Lasest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ScheduleFile = new ConsumerAvro<ScheduleFileInfo>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);
                    m_ConsumerAvro_ConfigFile = new ConsumerAvro<ConfigFileInfo>(m_ExportDataPath, configComsume_Earliest, m_CachedSchemaRegistryClient, schemaRegistryConfig);

                }
            }
            catch (Exception es)
            {

            }
        }
        private void chkSerialNumber_CheckedChanged(object sender, EventArgs e)
        {
            txtSerialNumber.Enabled = chkSerialNumber.Checked;
            chkTestName.Enabled = chkSerialNumber.Checked;
            chkTestName.Checked = chkTestName.Checked && chkSerialNumber.Checked;
            txtTestName.Enabled = chkTestName.Checked;
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
        private void chkConfluentCloud_CheckedChanged(object sender, EventArgs e)
        {
            txtSaslUsername.Enabled = txtSaslPassword.Enabled = txtBasicAuthUsername.Enabled = txtBasicAuthPassword.Enabled = chkConfluentCloud.Checked;
        }
    }
}
