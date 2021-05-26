using System.Xml.Serialization;

namespace API.Models.DeadlockJson
{
    [XmlType("victim-list")]
    public class VictimList
    {
        [XmlElement("victimProcess")]
        public VictimProcess VictimProcess { get; set; }
    }
}
