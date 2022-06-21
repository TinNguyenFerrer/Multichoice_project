using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    public class Result
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key()]
        public int Id { get; set; }
        [Required]
        public string NumberOfCorrectAnswers { get; set; }
        public DateTime DateTime { get; set; }
        [ForeignKey("Test")]
        public int? TestId { get; set; }
        public Test? Test { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
