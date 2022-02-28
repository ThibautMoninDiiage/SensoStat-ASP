using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string? Id { get; set; }
        public Survey? Survey { get; set; }
        public string? Code { get; set; }
        public string? Link { get; set; }
    }
}
