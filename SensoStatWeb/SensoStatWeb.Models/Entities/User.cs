using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SensoStatWeb.Models.Entities
{
    [Table("User")]
    public class User
    {
        [Key]
        public string? Id { get; set; }
        public int SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        [JsonIgnore]
        public Survey? Survey { get; set; }
        public string? Code { get; set; }
        public string? Link { get; set; }
        [JsonIgnore]
        public List<Answer>? Answers { get; set; }
        [JsonIgnore]
        public List<UserProduct>? UserProducts { get; set; }
    }
}
