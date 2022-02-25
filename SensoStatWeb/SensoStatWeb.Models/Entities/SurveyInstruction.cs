using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensoStatWeb.Models.Entities
{
    [Table("SurveyInstruction")]
    public class SurveyInstruction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int InstructionId { get; set; }
        [ForeignKey("InstructionId")]
        public Instruction? Instruction { get; set; }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SurveyId { get; set; }
        [ForeignKey("SurveyId")]
        public Survey? Survey { get; set; }
        public int Position { get; set; }
    }
}
