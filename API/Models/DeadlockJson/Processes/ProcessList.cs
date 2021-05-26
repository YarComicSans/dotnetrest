using System.Xml.Serialization;

namespace API.Models.DeadlockJson
{
    [XmlType("process-list")]
    public class ProcessList
    {
        [XmlElement("process")]
        public Process[] Process { get; set; }
    }
}
