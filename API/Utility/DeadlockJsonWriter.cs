using Newtonsoft.Json;
using System.IO;

namespace API.Services
{
    public class DeadlockJsonWriter : JsonTextWriter
    {
        public DeadlockJsonWriter(TextWriter writer) : base(writer) { }

        public override void WritePropertyName(string name)
        {
            switch(name)
            {
                case "victim-list":
                    base.WritePropertyName("victimList");
                    return;
                case "process-list":
                    base.WritePropertyName("processList");
                    return;
                case "resource-list":
                    base.WritePropertyName("resourceList");
                    return;
                default:
                    break;
            }

            if(name.StartsWith("@") || name.StartsWith("#"))
            {
                base.WritePropertyName(name.Substring(1));
                return;
            }

            base.WritePropertyName(name);
        }
    }
}
