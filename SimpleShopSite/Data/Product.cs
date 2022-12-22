using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SexyWebSite.Data
{
    public class Product
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int ProductId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public List<Image> Images { get; set; } = new();

        public int MainImageIndex { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey("UserOwnerID")]
        public virtual ApplicationUser UserOwner { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}
