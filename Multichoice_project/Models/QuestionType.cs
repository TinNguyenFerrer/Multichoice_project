using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    public class QuestionType
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]

        public int Id { get; set; }
        [Required]
        public string Type { get; set; }
        public ICollection<Question>? Questions { get; set; }
    }
}
