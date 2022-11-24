using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Production_Planning_System.Models
{
    public class Sales
    {
        [Key]
        public int sales_Id { get; set; }

        [Required(ErrorMessage = "Enter The Order Number..")]
        [Display(Name = "Sales Order Number")]
       
        public int order_num { get; set; }

        [Required(ErrorMessage = "Enter The Product Demand (High/Low/Medium)..")]
        [DataType(DataType.Text)]
        [Display(Name = "Demand In Market")]
        [StringLength(20)]
        public string Demand { get; set; }


        [Required(ErrorMessage = "Enter The Order Type..")]
        [DataType(DataType.Text)]
        [Display(Name = "Order Type")]
        [StringLength(30)]
        public string Oder_Type { get; set; }


        [Display(Name = "ImageName")]
        public string? ImageName { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }
       
    }
}
