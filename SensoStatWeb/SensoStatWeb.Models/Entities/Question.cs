using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("Question")]
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public string? Libelle { get; set; }
        public int Position { get; set; }
        public int SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public Survey? Survey { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}