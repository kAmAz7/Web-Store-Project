using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using WebProject.Enum;

namespace WebProject.ModelDTO
{
    public class ProductDTO 
    {
        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public UserDTO Owner { get; set; }
        public long? UserId { get; set; }
        [Required]
        [MaxLength(50,ErrorMessage = "Title must be between 1-50 characters")]
        public string Title { get; set; }
        [Required]
        [MaxLength(500,ErrorMessage = "Short Description must be between 1-500 characters")]
        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(4000, ErrorMessage = "Long Description must be between 1-4000 characters")]
        [DisplayName("Long Description")]
        [DataType(DataType.MultilineText)]
        public string LongDescription { get; set; }
        [Required]
        public DateTime Date { get; set; }
        [DataType(DataType.Currency)]
        [Range(0,100000,ErrorMessage ="Price must be between 0 - 100,000 $")]
        public double Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
        [Required]
        public ProductState State { get; set; }
    }
}
