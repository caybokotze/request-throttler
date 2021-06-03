using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace RequestThrottler
{
    public class BlockedIpAddresses
    {
        public const string Filename = "ip-bans.json";
        
        public async Task<Dictionary<string, string>> GetFileEntries()
        {
            try
            {
                var sr = File.OpenText(GetFileDirectory());
                var fileContent = await sr.ReadToEndAsync();
                return JsonConvert.DeserializeObject<Dictionary<string, string>>(fileContent);
            }
            catch
            {
                throw;
            }
        }

        public string SerialiseToJson(Dictionary<string, string> ipAddresses)
        {
            return JsonConvert.SerializeObject(ipAddresses, Formatting.Indented);
        }

        private static string GetSolutionDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public string GetFileDirectory()
        {
            return GetSolutionDirectory() + "\\" + Filename;
        }

        public async Task AppendToFile(
            string ipAddress, 
            DateTime dateAppended)
        {
            try
            {
                var fs = File.Create(GetFileDirectory());
                var bannedIps = await GetFileEntries();
                bannedIps.Add(ipAddress, dateAppended.ToString("G"));
                var json = SerialiseToJson(bannedIps);
                byte[] bytes = new UTF8Encoding(true).GetBytes(json);
                fs.Write(bytes, 0, bytes.Length);
            }
            catch
            {
                throw;
            }
        }
    }
}