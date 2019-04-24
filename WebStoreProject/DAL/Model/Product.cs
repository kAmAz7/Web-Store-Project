using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProject.Enum;

namespace DAL.Model
{
    public class Product
    {
        public long Id { get; set; }
        public long? OwnerId { get; set; }
        public User Owner { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }
        [Required]
        [MaxLength(50)]
        public string Title { get; set; }
        [Required]
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        [Required]
        [MaxLength(4000)]
        public string LongDescription { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Date { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime DateInCart { get; set; }
        public double Price { get; set; }
        public byte[] Picture1 { get; set; }
        public byte[] Picture2 { get; set; }
        public byte[] Picture3 { get; set; }
        [Required]
        public ProductState State { get; set; }

        public Product(long? ownerId, string title, string shortDescription, string longDescription, double price,
                       byte[] pic1, byte[] pic2, byte[] pic3)
        {
            OwnerId = ownerId;
            Title = title;
            ShortDescription = shortDescription;
            LongDescription = longDescription;
            Price = price;
            Picture1 = pic1;
            Picture2 = pic2;
            Picture3 = pic3;
            Date = DateTime.Now;
            State = ProductState.Available;
        }

        public Product()
        {

        }
    }
}
