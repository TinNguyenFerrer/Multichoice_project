using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    [Index(nameof(Name), IsUnique = true)]
    public class Subject
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Test>? Test { get; set; }
        [ForeignKey("EducationalField")]
        public int EducationalFieldId { get; set; }
        public EducationalField EducationalField { get; set; }
    }
}
