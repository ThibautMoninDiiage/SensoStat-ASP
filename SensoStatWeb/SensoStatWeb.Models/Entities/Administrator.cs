using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SensoStatWeb.Models.DTOs.Up;

namespace SensoStatWeb.Models.Entities
{
    [Table("Administrator")]
    public class Administrator
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Salt { get; set; }
        [Required]
        public string? Password { get; set; }
        [JsonIgnore]
        public List<Survey>? Surveys { get; set;}

        public Administrator()
        {

        }

        public Administrator(AdministratorDTOUp administrator)
        {
            FirstName = administrator.FirstName;
            LastName = administrator.LastName;
            UserName = administrator.UserName;
            Password = administrator.Password;
        }
    }
}
