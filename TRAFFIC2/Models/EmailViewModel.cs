using System.ComponentModel.DataAnnotations;

namespace TRAFFIC2.Models
{
    public class EmailViewModel
    {
        [Required]
        [EmailAddress]
        public string ToEmail { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }
    }
}
