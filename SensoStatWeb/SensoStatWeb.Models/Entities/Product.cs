using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SensoStatWeb.Models.Entities
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string? Code { get; set; }
        public int SurveyId { get; set; }
        [JsonIgnore]
        [ForeignKey("SurveyId")]
        public Survey? Survey { get; set;}
        [JsonIgnore]
        public List<UserProduct>? UserProducts { get; set; }
    }
}
