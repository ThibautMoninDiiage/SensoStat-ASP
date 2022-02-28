using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("UserProduct")]
    public class UserProduct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        public User? User { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product? Product { get; set; } 
        public int Position { get; set; }
    }
}
