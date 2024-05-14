using Avro;
using Confluent.SchemaRegistry;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KafkaConsumer
{
    public class CommonHelper
    {
        object objLockFIle = new object(); 
        string m_LogPath = $"{AppDomain.CurrentDomain.SetupInformation.ApplicationBase}Log.txt";
        string m_ExportDataPath;
        CachedSchemaRegistryClient m_CachedSchemaRegistryClient;
        Dictionary<EKafkaTopic, int> m_TopicFilesCount = new Dictionary<EKafkaTopic, int>(); 
        Dictionary<string, int> m_NameMapIndex = new Dictionary<string, int>();
        public CommonHelper(string _ExportDataPath, CachedSchemaRegistryClient _CachedSchemaRegistryClient)
        {
            m_ExportDataPath = _ExportDataPath;
            m_CachedSchemaRegistryClient = _CachedSchemaRegistryClient;
        }
        public bool WriteFile_DifferenPath<T>(EKafkaTopic eKafkaTopic, string strPathFile, string Extension, List<string> lstLine, out string newStrPathFile, EMessageFormat eMessageFormat)
        {
            lock (objLockFIle)
            {
                strPathFile += Extension;
                if (!File.Exists(strPathFile))
                    CreateFile<T>(eKafkaTopic, strPathFile, eMessageFormat);
                bool bOk = true;
                newStrPathFile = strPathFile;
                try
                {
                    if (new FileInfo(strPathFile).Length >= 1024 * 1024 * Form1.m_ExportFileSize) //100M
                    {
                        var array = Path.GetFileName(strPathFile).Replace(Extension, "").Split('_');
                        int index = 1;
                        if (array.Length > 1)
                        {
                            index = Convert.ToInt32(array[1]);
                            index += 1;
                            strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            CreateFile<T>(eKafkaTopic, strPathFile, eMessageFormat);
                        }
                        else
                        {
                            strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            while (File.Exists(strPathFile))
                            {
                                index++;
                                strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            }
                            if (File.Exists(Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + (index - 1) + Extension) && new FileInfo(Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + (index - 1) + Extension).Length < 1024 * 1024 * Form1.m_ExportFileSize)
                            {
                                index--;
                                strPathFile = Path.GetDirectoryName(strPathFile) + "\\" + array[0] + "_" + index + Extension;
                            }
                            else
                            {
                                CreateFile<T>(eKafkaTopic, strPathFile, eMessageFormat);
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
        private void CreateFile<T>(EKafkaTopic eKafkaTopic, string strPathFile,EMessageFormat eMessageFormat, bool Create = true)
        {
            try
            {
                switch (eMessageFormat)
                {
                    case EMessageFormat.AVRO:
                        CreateFile_Avro(eKafkaTopic, strPathFile, Create);
                        break;
                    case EMessageFormat.JSON:
                        CreateFile_Json<T>(eKafkaTopic, strPathFile, Create);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log(ex.Message);
            }
        }
        private void CreateFile_Avro(EKafkaTopic eKafkaTopic, string strPathFile, bool Create = true)
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
                if (Create && !File.Exists(strPathFile))
                {
                    using (var fileStream = File.Create(strPathFile))
                    {
                        fileStream.Close(); // 关闭文件句柄
                    }
                    string strColumn = "";
                    foreach (var item in schema.Fields)
                    {
                        //if (item.Name == "Auxs" || item.Name == "CANBMSs" || item.Name == "SMBs")
                        //{
                        //    continue;
                        //}
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
        private void CreateFile_Json<T>(EKafkaTopic eKafkaTopic, string strPathFile, bool Create = true)
        {
            try
            {
                if (!Directory.Exists(m_ExportDataPath))
                    Directory.CreateDirectory(m_ExportDataPath);
                List<string> lstLine = new List<string>();
                if (Create && !File.Exists(strPathFile))
                {
                    using (var fileStream = File.Create(strPathFile))
                        fileStream.Close(); // 关闭文件句柄
                    string strColumn = "";
                    foreach (PropertyInfo property in typeof(T).GetProperties())
                    {
                        strColumn += property.Name + ",";
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
