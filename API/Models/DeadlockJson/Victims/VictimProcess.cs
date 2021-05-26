using System.Xml.Serialization;

namespace API.Models.DeadlockJson
{
    [XmlType("victimProcess")]
    public class VictimProcess
    {
        [XmlAttribute("id")]
        public string Id { get; set; }
    }
}
