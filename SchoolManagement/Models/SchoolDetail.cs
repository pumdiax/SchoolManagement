using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class SchoolDetail
    {
        [Key]
        public int Id { get; set; }
        public string SchoolName { get; set; }
        public string SchoolAddress { get; set; }
        public string SchoolEmail { get; set; }
        public string SchoolPhone { get; set; }
        public string SchoolWebsite { get; set; }
        public string PrincipalName { get; set; }
        public string PrincipalEmail { get; set; }
        public string PrincipalPhone { get; set; }


        
    }
}
