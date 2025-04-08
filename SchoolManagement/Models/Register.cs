using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Models
{
    public class Register
    {

        // School Information
        [Required(ErrorMessage = "School Name is required")]
        [StringLength(100)]
        public string SchoolName { get; set; }


       
        [Required(ErrorMessage = "School Address is required")]
        [StringLength(100)]
        public string SchoolAddress { get; set; }



       
        [DataType(DataType.Date)]
        public DateTime EstablishmentDate { get; set; }



        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number")]
        [StringLength(10, ErrorMessage = "Phone Number must be 10 digits.")]
        public string SchoolPhone { get; set; }




        [Key]
        [Required(ErrorMessage = "School Email is required")]
        [EmailAddress]
        public string SchoolEmail { get; set; }



       
        [Required(ErrorMessage = "Administrator Email is required")]
        [EmailAddress]
        public string AdminEmail { get; set; }



        [Required(ErrorMessage = "Full Name is required")]
        [StringLength(100)]
        public string AdminName { get; set; }



       
        [DataType(DataType.Date)]
        public DateTime AdminDob { get; set; }



        
        public string AdminGender { get; set; }



        public string AdminAddress { get; set; }



        [Required(ErrorMessage = "Phone Number is required.")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number")]
        [StringLength(10, ErrorMessage = "Phone Number must be 10 digits.")]
        public string AdminPhone { get; set; }



        [Required(ErrorMessage = "Password is Requider")]
        [DataType(DataType.Password)]
        public string Password { get; set; }




        [Required(ErrorMessage = "Please Re-enter your Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match!")]
        public string ConfirmPassword { get; set; }
    }
}
