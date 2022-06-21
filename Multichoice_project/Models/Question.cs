using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    public class Question
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        public int Mark { get; set; }
        [Required]
        [ForeignKey("Test")]
        public int TestId { get; set; }
        public Test Test { get; set; }
        [ForeignKey("QuestionType")]
        public int QuestionTypeID { get; set; }
        [Required]
        public QuestionType Type { get; set; }
        public ICollection<Answer>? Answers { get; set; }
    }
}
