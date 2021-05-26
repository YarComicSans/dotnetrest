using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Deadlock
    {
        public Deadlock() { }
        public Deadlock(string id, string processId, DateTime? date, string xml, string json)
        {
            Id = id;
            ProcessId = processId;
            CreatedDate = date;
            XmlData = xml;
            JsonData = json;
        }
        [Key]
        [Required]
        public string Id { get; set; }
        [Required]
        public string ProcessId { get; set; }
        [Required]
        public DateTime? CreatedDate { get; set; }
        [Required]
        public string XmlData { get; set; }
        [Required]
        public string JsonData { get; set; }
    }
}
