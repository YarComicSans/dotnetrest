using System;
using Microsoft.Extensions.Configuration;
using System.Xml;
using System.IO;

namespace API.Services
{
    public interface IDeadlockImporter
    {
        public string[] ReadDeadlockXmlsFromFiles();
    }

    public class DeadlockImporter : IDeadlockImporter
    {
        private readonly IConfiguration _configuration;

        public DeadlockImporter(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string[] ReadDeadlockXmlsFromFiles()
        {
            var folderPath = _configuration["DeadlocksFilesFolder"];
            var fullPath = Path.Combine(Environment.CurrentDirectory, folderPath);

            string[] filenames = Directory.GetFiles(fullPath, "*.xml");

            var xmls = new string[filenames.Length];
            XmlDocument doc = new XmlDocument();

            for (int i = 0; i < filenames.Length; i++)
            {
                doc.Load(filenames[i]);
                xmls[i] = doc.InnerXml;
            }

            return xmls;
        }
    }
}
