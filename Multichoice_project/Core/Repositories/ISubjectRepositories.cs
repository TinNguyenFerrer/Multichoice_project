using Multichoice_project.Models;
using Multichoice_project.Repositories;

namespace Multichoice_project.Core.Repositories
{
    public interface ISubjectRepositories : IRepositories<Subject>
    {
        public IEnumerable<Subject> GetSubjectByEducationFieldId(int idEdu);
        public IEnumerable<Subject> GetSubjectByEducationFieldName(string EduName);
    }
}
