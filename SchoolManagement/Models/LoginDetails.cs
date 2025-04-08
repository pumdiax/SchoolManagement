using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class LoginDetails
    {
        [Required]
        public string SchoolName { get; set; }
        [Required]
        public string UserEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

    }
}
