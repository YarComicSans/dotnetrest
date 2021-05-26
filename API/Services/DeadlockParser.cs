using System;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using API.Models;
using Newtonsoft.Json;

namespace API.Services
{
    public interface IDeadlockParser
    {
        public Deadlock FromXml(string xml);
    }

    public class DeadlockParser : IDeadlockParser
    {
        public Deadlock FromXml(string xml)
        {
            try 
            {
                XmlSerializer serializer = new XmlSerializer(typeof(JsonDeadlock));
                StringReader reader = new StringReader(xml);
                var deadlockJson = (JsonDeadlock)serializer.Deserialize(reader);

                if(deadlockJson == null) return null;

                var victimProcessId = deadlockJson.VictimList?.VictimProcess?.Id;
                if(victimProcessId == null || victimProcessId == "") return null;

                DateTime? date = deadlockJson.ProcessList?.Process[0]?.Lasttranstarted;
                if(date != null && date == DateTime.MinValue) date = null;

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);

                string jsonString = JsonConvert.SerializeXmlNode(doc);

                var deadlock = new Deadlock(null, victimProcessId, date, xml, jsonString);
                return deadlock;
            }
            catch(Exception)
            {
                return null;
            }
        }
    }
}
