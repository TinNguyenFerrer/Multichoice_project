using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    public class Test
    {
        [Key]
        public int Id { get; set; } 
        [Required]
        public string Name { get; set; }
        [Required]    
        
        public string Link { get; set; }
        public String Description { get; set; }
        public DateTime CreatedTime { get; set; }
        public long Hits { get; set; }
        public User? User { get; set; }
        [ForeignKey("User")]
        public int? UserId { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Question>? Question { get; set; }
        public ICollection<Result>? Results { get; set; }
        public int Time { get; set; }

    }
}
