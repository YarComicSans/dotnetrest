using System;
using System.Xml.Serialization;

namespace API.Models.DeadlockJson
{
    [XmlType("process")]
    public class Process
    {
        [XmlIgnore]
        public DateTime? Lasttranstarted { get; private set; }

        [XmlAttribute("lasttranstarted")]
        public string LasttranstartedString
        {
            get { return this.Lasttranstarted.ToString(); }
            set
            {
                if(!string.IsNullOrEmpty(value))
                    this.Lasttranstarted = DateTime.Parse(value);
                else
                    this.Lasttranstarted = null;
            }
        }
    }
}
