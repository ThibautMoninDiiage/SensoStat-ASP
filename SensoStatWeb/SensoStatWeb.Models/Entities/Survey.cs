using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("Survey")]
    public class Survey
    {
        public Survey()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string? Name { get; set; }

        public int CreatorId { get; set; }

        [ForeignKey("CreatorId")]
        public Administrator? Administrator { get; set; }

        public int StateId { get; set; }

        [ForeignKey("StateId")]
        public SurveyState? SurveyState { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User? User { get; set; }

        public DateTime CreationDate { get; set; }

        public List<Question>? Questions { get; set; }

        public List<Instruction>? Instructions { get; set; }

        public List<UserProduct>? UserProducts { get; set; }
    }
}
