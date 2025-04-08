using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        // Navigation property
        public ICollection<UserDetail> UserDetail { get; set; }
    }
}
