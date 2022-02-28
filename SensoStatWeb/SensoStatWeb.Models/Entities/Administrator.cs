using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace SensoStatWeb.Models.Entities
{
    [Table("Administrator")]
    public class Administrator
    {
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string? UserName { get; set; }
        [Required]
        public string? Password { get; set; }
        [JsonIgnore]
        public new List<Survey>? Surveys { get; set;}
    }
}
