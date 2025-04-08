using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagement.Models
{
    public class UserDetail

    {
        [Key]
        [Required]
        public string UserId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string UserEmail { get; set; }
        public string UserPhone { get; set; }

        public string UserAddress { get; set; }

        public string UserGender { get; set; }

        public string UserDOB { get; set; }

        [Required]
        public string UserPassword { get; set; }

        public string UserSchoolName { get; set; }

        // Foreign key for Role
        public int RoleId { get; set; }

        // Navigation property
        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
