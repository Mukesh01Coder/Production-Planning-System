using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Production_Planning_System.Models
{
    public class Production
    {
        [Key]
        public int Product_Id { get; set; }


        [Required(ErrorMessage = "Enter the Product Name...")]
        [DataType(DataType.Text)]
        [StringLength(30)]
        [Display(Name = "Product Name")]
        public string Product_Name { get; set; }

        [Required(ErrorMessage = "Enter the Product Shape...")]
        [DataType(DataType.Text)]
        [Display(Name = "Product Shape")]
        [StringLength(20)]
        public string Product_Shape { get; set; }

        [Required(ErrorMessage = "Enter the Product Color...")]
        [DataType(DataType.Text)]
        [StringLength(20)]
        [Display(Name = "Product Color")]
        public string Product_Color { get; set; }

        [Required(ErrorMessage = "Enter the Product Quantity...")]
        [Display(Name = "Product Quantity")]
        public int Product_Quantity { get; set; }

        [Required(ErrorMessage = "Enter the Product Price...")]
        [DataType(DataType.Currency)]
        [Display(Name = "Product Price")]
        long Product_Price { get; set; }


        [Display(Name = "ImageName")]
        public string? ImageName { get; set; }

        [Display(Name = "Profile Picture")]
        public string? ProfilePicture { get; set; }
       
    }
}
