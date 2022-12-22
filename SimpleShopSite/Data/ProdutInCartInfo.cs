using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SexyWebSite.Data
{
    public class ProdutInCartInfo
    {
        [Key]
        public int Id { get; set; }

        public string UserId { get; set; } = default!;

        public int ProductId { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime AddedDay { get; set; }

        [ForeignKey("UserOwnerID")]
        public virtual ApplicationUser UserProductBuy { get; set; } = default!;
    }
}
