using API.Models.DeadlockJson;
using System.Xml.Serialization;

namespace API.Models
{
    [XmlRoot("deadlock")]
    public class JsonDeadlock
    {
        [XmlElement("victim-list")]
        public VictimList VictimList { get; set; }

        [XmlElement("process-list")]
        public ProcessList ProcessList { get; set; }
    }
}
