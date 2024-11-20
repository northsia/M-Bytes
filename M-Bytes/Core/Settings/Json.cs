using LiteDB;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace M_Bytes.Core.Settings
{
    internal class Json
    {
        public static void CREATE_VAR(string var1, string var2 = null, string var3 = null, string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                index.logger.Error("Path is required.");
                return;
            }

            string fullPath = Path.Combine(path, var1);

            if (!string.IsNullOrEmpty(var2))
            {
                fullPath = Path.Combine(fullPath, var2);
            }

            if (!string.IsNullOrEmpty(var3))
            {
                fullPath = Path.Combine(fullPath, var3);
            }

            index.logger.Info($"Generated Path: {fullPath}");

            try
            {
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                    index.logger.Info("Directory created successfully.");
                }
                else
                {
                    index.logger.Error("Directory already exists.");
                }
            }
            catch (Exception ex)
            {
                index.logger.Error($"Error creating directory: {ex.Message}");
            }
        }
        public static bool GET_TRUE_F_CONTENT_FROM_JSON_(string json_path, string param)
        {
            try
            {
                // Check if the file exists
                if (!File.Exists(json_path))
                {
                    index.logger.Error($"File not found: {json_path}");
                    return false;
                }

                // Read the content of the JSON file
                string jsonContent = File.ReadAllText(json_path);

                // Parse the JSON into a dynamic object
                var parsedJson = JsonConvert.DeserializeObject<dynamic>(jsonContent);

                // Check if the specified key exists and its value is true
                if (parsedJson[param] != null && parsedJson[param] == true)
                {
                    index.logger.Info($"Found true value for parameter: {param} = {parsedJson[param]}");
                    return true;
                }

                // Return false if the parameter is not found or its value is not true
                index.logger.Info($"Parameter '{param}' not found or its value is not true.");
                return false;
            }
            catch (Exception ex)
            {
                index.logger.Error($"An error occurred while reading the file: {ex.Message}");
                return false; // Return false on error
            }
        }
    }
}
