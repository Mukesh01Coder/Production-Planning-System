using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Production_Planning_System.Models
{
    public class Administration
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter first name")]
        [Display(Name = "First Name")]
        [StringLength(20)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter last name")]
        [Display(Name = "Last Name")]
        [StringLength(20)]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        [StringLength(50)]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please enter age")]      
        public int Age { get; set; }

        [Required(ErrorMessage = "Please choose gender")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Please enter position")]
        [StringLength(20)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Please enter office")]
        [StringLength(20)]
        public string Office { get; set; }

        [Required(ErrorMessage = "Please enter salary")]
        public int Salary { get; set; }

        [Display(Name = "ImageName")]
        public string? ImageName { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }

        public Administration()
        {
            FullName = FirstName + " " + LastName;
        }
    }
}
