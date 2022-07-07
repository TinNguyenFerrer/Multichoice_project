using Microsoft.EntityFrameworkCore;
using Multichoice_project.Core.Repositories;
using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Persistence.Repositories
{
    public class SubjectRepositories : Repositories<Subject>, ISubjectRepositories
    {
        public SubjectRepositories(Multichoise_DBContext dbContext) : base(dbContext)
        {

        }

        public IEnumerable<Subject> GetSubjectByEducationFieldId(int idEdu)
        {
            return (from subject in  this.GetAll() 
                    where subject.EducationalFieldId==idEdu
                    select subject);
        }
        public IEnumerable<Subject> GetSubjectByEducationFieldName(string EduName)
        {
            return (from subject in this.GetAll()
                    join educationalfield in this.DbContext.EducationalFields 
                    on subject.EducationalField.Name equals educationalfield.Name
                    where educationalfield.Name == EduName
                    select subject);
        }
    }
}
