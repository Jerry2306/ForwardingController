using System;
using System.Collections.Generic;
using System.IO;

namespace CustomModuleUtils
{
    public class CustomModuleConfigurationSplitter
    {
        public IDictionary<string, string> GetConfiguration(FileInfo configFile)
        {
            var result = new Dictionary<string, string>();
            var fileLines = File.ReadAllLines(configFile.FullName);

            for (var i = 0; i < fileLines.Length; i++)
            {
                var lineResult = fileLines[i].Split('=');

                if (lineResult.Length != 2)
                    throw new Exception($"Unreadable line in config file '{configFile.FullName}' in line '{i + 1}'!");

                if (string.IsNullOrEmpty(lineResult[0]))
                    throw new Exception($"Invalid key in config file '{configFile.FullName}' in line '{i + 1}'. Key can't be empty!");

                if (result.ContainsKey(lineResult[0]))
                    throw new Exception($"Invalid key in config file '{configFile.FullName}' in line '{i + 1}'. Key already exists!");

                result.Add(lineResult[0], lineResult[1]);
            }

            return result;
        }
    }
}