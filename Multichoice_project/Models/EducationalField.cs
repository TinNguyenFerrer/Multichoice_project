using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    public class EducationalField
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Subject> Subjects { get; set; }
    }
}
