﻿using System.IO;

using Share.Logs;
using Share.Json;

namespace Share.Config
{
    public class DataServerConfig
    {
        public string RedisIPAddr;
        public int RedisPort;
        public string MySqlConnection;
    }

    public class ConfigManager : Singleton<ConfigManager>
    {
        private const string CONFIG_JSON_FILE = "DataServerConfig.json";

        private DataServerConfig m_Config;



        public ConfigManager()
        {
            m_Config = null;
        }

        public void Initialize()
        {
            string json_file = Path.Combine(Folder.GetCurrentDir(), CONFIG_JSON_FILE);

            if (!File.Exists(json_file))
            {
                LogManager.Error("Data server config file " + json_file.ToString() + " not exist!");
                return;
            }

            FileStream file_stream = new FileStream(json_file, FileMode.Open);
            StreamReader stream_reader = new StreamReader(file_stream);
            stream_reader.BaseStream.Seek(0, SeekOrigin.Begin);

            string tmp_content = stream_reader.ReadToEnd();
            m_Config = JsonHelper.DeserializeJsonToObject<DataServerConfig>(tmp_content);

            stream_reader.Close();
            file_stream.Close();
        }
    }
}
