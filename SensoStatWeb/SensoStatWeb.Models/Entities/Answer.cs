using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("Id")]
        public Question? Question { get; set; }
        public string? UserAnswer { get; set; }
    }
}
