using Multichoice_project.Models;
using Multichoice_project.GenericRepositories;

namespace Multichoice_project.Core.Repositories
{
    public interface ISubjectRepositories : IRepositories<Subject>
    {
        public IEnumerable<Subject> GetSubjectByEducationFieldId(int idEdu);
        public IEnumerable<Subject> GetSubjectByEducationFieldName(string EduName);
    }
}
