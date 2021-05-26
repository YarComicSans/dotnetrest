using System;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Dtos
{
    public class DeadlockDto
    {
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
