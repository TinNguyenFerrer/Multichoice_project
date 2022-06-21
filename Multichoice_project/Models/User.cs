using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Multichoice_project.Models
{
    [Index(nameof(UserName), IsUnique =true)]
    public class User
    {
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity), Key]
        public int Id { get; set; }
        [MaxLength(10)]
        [Required]
        public string UserName { get; set; }
        [MaxLength(100)]
        [Required]
        public string PassWord { get; set; }
        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        public string RoleName { get; set; }
        public ICollection<Test>? Tests { get; set; }
        public ICollection<Result>? Results;
    }
}
