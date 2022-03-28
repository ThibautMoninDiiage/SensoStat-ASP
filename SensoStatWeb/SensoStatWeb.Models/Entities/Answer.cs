using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SensoStatWeb.Models.Entities
{
    [Table("Answer")]
    public class Answer
    {
        [Key]
        public string? UserId { get; set; }
        public User? User { get; set; }

        [Key]
        public int QuestionId { get; set; }
        public Question? Question { get; set; }

        public string? UserAnswer { get; set; }

        [Key]
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}