using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatApi.Models
{
    [Table("SurveyState")]
    public class SurveyState
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string? Libelle { get; set; }

        public List<Survey>? Surveys { get; set; }
    }
}
